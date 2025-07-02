using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameObject originFoodPrefab; // �� ������ � ���Ŀ��� ����°�

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
