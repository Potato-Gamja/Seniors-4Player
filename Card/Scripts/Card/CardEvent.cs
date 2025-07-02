using UnityEngine;
using DG.Tweening;
using System;
using MoreMountains.Feedbacks;
using MoreMountains.FeedbacksForThirdParty;

public class CardEvent : MonoBehaviour
{
    public int id; //ī���� �ѹ�: ī���� ¦�� ���߱� ���� �ѹ�
    [SerializeField] PlaySFX playSFX;
    public SpriteRenderer cardRenderer; //ī���� ��������Ʈ ������
    public BoxCollider2D boxCol; //ī���� �ڽ� �ݶ��̴�

    public Sprite front; //�ո�
    public Sprite back; //�޸�

    private bool isFlipped = true;
    public MMF_Player feedbackPlayer;

    //ī�� ���� <- ����Ƽ �̺�Ʈ���� ȣ��
    public void SelectCard()
    {
        FlipCardImage(); //ī�� ������
        SetCard(); //ù��°, �ι�° ī�� ����
    }

    //ī�� ������
    public void FlipCardImage()
    {
        Vector3 baseScale = transform.localScale; //���� �����ϰ�
        Vector3 targetScale = new Vector3(0f, baseScale.y, baseScale.z); //��ǥ �����ϰ�

        //DG.Tweening�� ����� ī�� ũ�� ����(��� �ִϸ��̼�)
        //0.2�� ���� targetScale ������ ���� -> ī�尡 �������� ȿ���� ����
        transform.DOScale(targetScale, 0.2f).OnComplete(() => //��� �ִϸ��̼��� ���� �� ����Ǵ� �ݹ��Լ�
        {
            isFlipped = !isFlipped; //������ ���¸� ���� -> �ո����� �޸����� üũ
            
            if (isFlipped) //������
            {
                cardRenderer.sprite = front; //ī�巻������ ��������Ʈ�� �ո� ��������Ʈ�� ����.
            }
            else //�������
            {
                cardRenderer.sprite = back; //ī�巻������ ��������Ʈ�� �޸� ��������Ʈ�� ����.
            }

            transform.DOScale(baseScale, 0.2f); //ī�带 ���� ũ���� 0.2�� ���� ����
        });
    }

    public void SetCard()
    {
        if (GameManager.Instance.firstCard == null) //������ ù ��° ī�尡 ���� ���
        {
            GameManager.Instance.firstCard = this; //ù ��° ī��� ����
        }
        else if (GameManager.Instance.firstCard != null && GameManager.Instance.secondCard == null) //������ ù ��° ī�尡 ������, �� ��° ī�尡 ���� ���
        {
            GameManager.Instance.secondCard = this; //�� ��° ī��� ����
            GameManager.Instance.Matched(); //�� ī�带 ��
        }

        BoxCol(); //�ڽ� �ݶ��̴� ����
    }

    //1�� �� ī�� �ı�
    public void DestroyCard()
    {
        Invoke(nameof(Feel_Scale), 0.5f);
        Invoke(nameof(DestroyCardInvoke), 1.0f);
    }

    private void Feel_Scale()
    {
        //feedbackPlayer.PlayFeedbacks();
        
    }

    //ī�� �ı�
    public void DestroyCardInvoke()
    {
        playSFX.GetSoundPlay();

        GameManager.Instance.isMatch = false; //ī�� ��Ī ���� ����
        GameManager.Instance.firstCard = null; //ù��° ī�� �ʱ�ȭ
        GameManager.Instance.secondCard = null; //�ι�° ī�� �ʱ�ȭ

        Destroy(gameObject);
    }

    //1�� �� ī�� �ݱ�
    public void CloseCard()
    {
        Invoke(nameof(CloseCardInvoke), 1.0f);
    }

    //ī�� �ݱ�
    public void CloseCardInvoke()
    {
        FlipCardImage();
        playSFX.CancelSoundPlay();

        GameManager.Instance.isMatch = false; //ī�� ��Ī ���� ����
        GameManager.Instance.firstCard = null; //ù��° ī�� �ʱ�ȭ
        GameManager.Instance.secondCard = null; //�ι�° ī�� �ʱ�ȭ

        Invoke(nameof(BoxCol), 0.5f);
    }

    //�ڽ� �ݶ��̴� ���� > ���� ��ġ ����
    public void BoxCol()
    {
        boxCol.enabled = !boxCol.enabled; //�ڽ� �ݶ��̴� Ȱ��ȭ ���� ����
    }
}
