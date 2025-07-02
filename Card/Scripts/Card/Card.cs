using MoreMountains.Feedbacks;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public UnityEvent OnCreate; //카드 생성 시
    public UnityEvent OnSelect; //카드 선택 시

    private void OnMouseDown()
    {
        if (GameManager.Instance.isMatch) //카드 매칭 중일 경우에는 터치가 안 되도록
            return;

        OnSelect?.Invoke();
    }
}
