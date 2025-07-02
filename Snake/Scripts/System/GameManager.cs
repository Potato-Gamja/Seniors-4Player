using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    [SerializeField] Vector3 spawnAreaMin;
    [SerializeField] Vector3 spawnAreaMax;

    public float spawnHeight = 2f; // Y ���� (������ �ߴ� ��ġ)
    public LayerMask obstacleMask;  // �浹 ������ ���̾�

    float eatingTime = 8f;
    float eatingTimer = 0.0f;

    //[SerializeField] TextMeshProUGUI[] timerText;
    //float timer = 90.0f;

    public SnakeController[] players;
    
    private void Start()
    {
        eatingTime = 1.0f;
        //SetResolution(); // �ʱ⿡ ���� �ػ� ����
        //for (int i = 0; i < timerText.Length; i++)
        //{
        //    timerText[i].text = timer.ToString();
        //}
    }

    private void Update()
    {
        //if (timer <= 0)
        //    return;

        //timer -= Time.deltaTime;
        //for (int i = 0; i < timerText.Length; i++)
        //{
        //    timerText[i].text = ((int)timer).ToString();
        //}

        //if(timer <= 0)
        //{
        //    GameOver();
        //}

        eatingTimer += Time.deltaTime;
        if (eatingTimer >= eatingTime)
        {
            FoodSpawn();
            eatingTimer = 0.0f;
            eatingTime = Random.Range(8.0f, 15.0f);
        }
    }

    void GameOver()
    {
        Debug.Log("��������");
    }

    void FoodSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float z = Random.Range(spawnAreaMin.z, spawnAreaMax.z);
            Vector3 spawnPos = new Vector3(x, spawnHeight, z);

            int index = Random.Range(0, foodPrefabs.Length);

            if (!Physics.CheckSphere(spawnPos, 0.5f, obstacleMask))
            {
                Quaternion randomRot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                Instantiate(foodPrefabs[index], spawnPos, randomRot);
            }
        }
    }

    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
}
