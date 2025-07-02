using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    [SerializeField] Vector3 spawnAreaMin;
    [SerializeField] Vector3 spawnAreaMax;

    public float spawnHeight = 2f; // Y 높이 (음식이 뜨는 위치)
    public LayerMask obstacleMask;  // 충돌 방지용 레이어

    float eatingTime = 8f;
    float eatingTimer = 0.0f;

    //[SerializeField] TextMeshProUGUI[] timerText;
    //float timer = 90.0f;

    public SnakeController[] players;
    
    private void Start()
    {
        eatingTime = 1.0f;
        //SetResolution(); // 초기에 게임 해상도 고정
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
        Debug.Log("게임종료");
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
        int setWidth = 1920; // 사용자 설정 너비
        int setHeight = 1080; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }
}
