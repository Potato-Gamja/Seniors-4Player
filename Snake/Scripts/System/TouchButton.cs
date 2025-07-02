using UnityEngine;
using UnityEngine.EventSystems;

public class TurnButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Boost }
    public ButtonType turnDirection;

    float boostAmount = 2.0f;
    float duration = 2.0f;
    float boostCooldown = 12.0f;
    float boostTimer = 0.0f;
    bool canBoost = true;

    public SnakeController snake; // Inspector에서 연결

    void Update()
    {
        boostTimer += Time.deltaTime;
        if (boostTimer >= boostCooldown)
            canBoost = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (turnDirection == ButtonType.Left)
        {
            snake.TurnLeft();
        }
        else if (turnDirection == ButtonType.Right)
        {
            snake.TurnRight();
        }
        else if(turnDirection == ButtonType.Boost && canBoost)
        {
            canBoost = false;
            boostTimer = 0.0f;
            snake.BoostSpeed(boostAmount, duration);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        snake.StopTurning(); // 손 떼면 회전 중지
    }
}
