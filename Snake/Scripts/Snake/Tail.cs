using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameObject originFoodPrefab; // 이 꼬리가 어떤 음식에서 생겼는가

    public float safeTime = 0.5f;
    private float timer;

    void Start()
    {
        timer = safeTime;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    public bool IsSafe => timer > 0;
}
