using MoreMountains.Feedbacks;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public UnityEvent OnCreate; //ī�� ���� ��
    public UnityEvent OnSelect; //ī�� ���� ��

    private void OnMouseDown()
    {
        if (GameManager.Instance.isMatch) //ī�� ��Ī ���� ��쿡�� ��ġ�� �� �ǵ���
            return;

        OnSelect?.Invoke();
    }
}
