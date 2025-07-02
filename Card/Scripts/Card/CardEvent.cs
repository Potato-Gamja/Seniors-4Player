using UnityEngine;
using DG.Tweening;
using System;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;

public class CardEvent : MonoBehaviour
{
    public int id; //카드의 넘버: 카드의 짝을 맞추기 위한 넘버
    [SerializeField] PlaySFX playSFX;
    public SpriteRenderer cardRenderer; //카드의 스프라이트 렌더러
    public BoxCollider2D boxCol; //카드의 박스 콜라이더

    public Sprite front; //앞면
    public Sprite back; //뒷면

    private bool isFlipped = true;
    public MMF_Player feedbackPlayer;

    //카드 선택 <- 유니티 이벤트에서 호출
    public void SelectCard()
    {
        FlipCardImage(); //카드 뒤집기
        SetCard(); //첫번째, 두번째 카드 설정
    }

    //카드 뒤집기
    public void FlipCardImage()
    {
        Vector3 baseScale = transform.localScale; //기존 스케일값
        Vector3 targetScale = new Vector3(0f, baseScale.y, baseScale.z); //목표 스케일값

        //DG.Tweening을 사용한 카드 크기 변경(축소 애니메이션)
        //0.2초 동안 targetScale 값으로 변경 -> 카드가 뒤집히는 효과를 연출
        transform.DOScale(targetScale, 0.2f).OnComplete(() => //축소 애니메이션이 끝난 후 실행되는 콜백함수
        {
            isFlipped = !isFlipped; //뒤집힘 상태를 반전 -> 앞면인지 뒷면인지 체크
            
            if (isFlipped) //뒤집힘
            {
                cardRenderer.sprite = front; //카드렌더러의 스프라이트를 앞면 스프라이트로 변경.
            }
            else //원래대로
            {
                cardRenderer.sprite = back; //카드렌더러의 스프라이트를 뒷면 스프라이트로 변경.
            }

            transform.DOScale(baseScale, 0.2f); //카드를 원래 크기대로 0.2초 동안 복구
        });
    }

    public void SetCard()
    {
        if (GameManager.Instance.firstCard == null) //선택한 첫 번째 카드가 없을 경우
        {
            GameManager.Instance.firstCard = this; //첫 번째 카드로 설정
        }
        else if (GameManager.Instance.firstCard != null && GameManager.Instance.secondCard == null) //선택한 첫 번째 카드가 있으며, 두 번째 카드가 없을 경우
        {
            GameManager.Instance.secondCard = this; //두 번째 카드로 설정
            GameManager.Instance.Matched(); //두 카드를 비교
        }

        BoxCol(); //박스 콜라이더 반전
    }

    //1초 후 카드 파괴
    public void DestroyCard()
    {
        Invoke(nameof(Feel_Scale), 0.5f);
        Invoke(nameof(DestroyCardInvoke), 1.0f);
    }

    private void Feel_Scale()
    {
        //feedbackPlayer.PlayFeedbacks();
        
    }

    //카드 파괴
    public void DestroyCardInvoke()
    {
        playSFX.GetSoundPlay();

        GameManager.Instance.isMatch = false; //카드 매칭 여부 거짓
        GameManager.Instance.firstCard = null; //첫번째 카드 초기화
        GameManager.Instance.secondCard = null; //두번째 카드 초기화

        Destroy(gameObject);
    }

    //1초 후 카드 닫기
    public void CloseCard()
    {
        Invoke(nameof(CloseCardInvoke), 1.0f);
    }

    //카드 닫기
    public void CloseCardInvoke()
    {
        FlipCardImage();
        playSFX.CancelSoundPlay();

        GameManager.Instance.isMatch = false; //카드 매칭 여부 거짓
        GameManager.Instance.firstCard = null; //첫번째 카드 초기화
        GameManager.Instance.secondCard = null; //두번째 카드 초기화

        Invoke(nameof(BoxCol), 0.5f);
    }

    //박스 콜라이더 반전 > 연속 터치 방지
    public void BoxCol()
    {
        boxCol.enabled = !boxCol.enabled; //박스 콜라이더 활성화 상태 반전
    }
}
