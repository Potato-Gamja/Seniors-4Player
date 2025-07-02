using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //게임매니저 싱글톤
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                instance = obj.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public List<CardEvent> cardList = new List<CardEvent>(); //카드이벤트 스크립트 리스트

    SoundManager soundManager;
    [SerializeField] AudioSource flip_AudioSource;

    public GameObject clearPanel; //클리어 판넬
    public float timer = 0.0f; //게임 시작
    private int touchCount = 0; //뒤집기를 시도한 횟수

    public CardEvent firstCard; //첫번째로 선택한 카드
    public CardEvent secondCard; //두번째로 선택한 카드

    public bool isMatch = true; //게임시작 전 터치 방지
    public bool isClear = false;

    public int count = 0; //맞추기 성공한 카운트
    public int clearCount; //맞추어야 하는 카운트

    [SerializeField] GameObject[] playerGroup = new GameObject[4]; //플레이어 그룹 오브젝트 배열
    [SerializeField] Image[] playerImage = new Image[4]; //플레이어 이미지 오브젝트 배열
    public Text[] playerText = new Text[4]; //플레이어 텍스트 배열
    public int[] playerScore = new int[] { 0, 0, 0, 0 }; //플레이어 점수 배열
    public Text[] winnerText = new Text[4];

    public int playerID; //플레이어 구분 아이디 0~3

    private void Awake()
    {

        //게임매니저 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (isClear)
        {
            StopAllCoroutines(); //코루틴 정지
        }
    }

    //게임설정
    public void SetGame()
    {
        clearPanel = GameObject.Find("---Canvas---").transform.GetChild(0).gameObject;
        flip_AudioSource = GameObject.Find("SFX Manager").GetComponent<AudioSource>();
        soundManager = GameObject.Find("Sound Manager").GetComponent<SoundManager>();

        for (int i = 0; i < 4; i++)
        {
            playerGroup[i] = GameObject.Find("Player Group").transform.GetChild(i).gameObject;
            playerImage[i] = playerGroup[i].GetComponent<Image>();
            playerText[i] = playerGroup[i].transform.GetChild(0).gameObject.GetComponent<Text>();
            winnerText[i] = clearPanel.transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.GetComponent<Text>();
        }

        playerID = Random.Range(0, 4); //랜덤차례
        PlayerChanged(); //랜덤차례 적용

        //카드이벤트 스크립트 리스트 추가
        //cardList.AddRange(FindObjectsByType<CardEvent>(FindObjectsInactive.Include, //비활성화된 오브젝트 포함 여부
        //  

        clearCount = cardList.Count / 2; //클리어 카운트 체크
        Invoke(nameof(GameStart), 5f); //게임스타트 함수 5초 뒤에 실행
        Invoke(nameof(CardBoxCol), 6f);
    }

    //게임시작
    private void GameStart()
    {
        foreach (CardEvent card in cardList) //카드 이벤트를 가진 리스트 반복 호출
        {
            card.FlipCardImage(); //카드 뒤집기 > 앞면에서 뒷면으로 뒤집기
        }
        flip_AudioSource.Play();
        isMatch = false; //터치 가능하게 매치여부 거짓
        StartCoroutine(TimerCoroution()); //타이머 실행
    }

    private void CardBoxCol()
    {
        foreach (CardEvent card in cardList) //카드 이벤트를 가진 리스트 반복 호출
        {
            card.BoxCol(); //카드 박스 콜라이더 활성화
        }
    }

    //게임 클리어
    private void GameClear()
    {
        ScoreCompare();
        clearPanel.SetActive(true); //클리어 판넬 활성화
        soundManager.VictorySFX_Play();
    }

    //타이머 코루틴
    private IEnumerator TimerCoroution()
    {
        var wait = new WaitForSeconds(1f);

        timer += 1; //시간 1초 증가
        yield return wait; //1초 딜레이
        StartCoroutine(TimerCoroution()); //코루틴 실행
    }

    //카드 맞추기
    public void Matched()
    {
        isMatch = true; //카드 매칭 여부 참
        touchCount++; //뒵지기 카운트 증가
        if (firstCard.id == secondCard.id) //서로 같은 카드일 경우
        {
            firstCard.DestroyCard(); //카드 제거
            secondCard.DestroyCard(); //카드 제거
            count++; //게임클리어 카운트 증가

            playerScore[playerID]++; //플레이어 점수 증가
            playerText[playerID].text = playerScore[playerID].ToString(); //플레이어 점수 텍스트 값 변경

            //게임 클리어 카운트 충족 시
            if (count == clearCount)
            {
                Invoke(nameof(GameClear), 1.0f); //게임 클리어
            }
        }
        else //서로 다른 카드일 경우
        {
            firstCard.CloseCard(); //뒤집기
            secondCard.CloseCard(); //뒤집기

            playerID++; //플레이어 아이디 증가

            if (playerID >= 4) //플레이어 아이디 초기화
            {
                playerID = 0; //처음 순서로
            }

            PlayerChanged(); //플레이어 차례 변경 효과
        }
    }

    //플레이어 교체
    public void PlayerChanged()
    {
        int i = playerID - 1;

        if (i < 0)
            i = 3;

        //플레이어 아이디 스위치
        switch (playerID)
        {
            case 0: //첫 번째 플레이어
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                //전 차례 플레이어 이미지 알파 값 변경
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);
                //이번 차례 플레이어 이미지 알파 값 변경
                break;

            case 1: //두 번째 플레이어
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);

                break;

            case 2: //세 번째 플레이어
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);

                break;

            case 3: //네 번째 플레이어
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);

                break;
        }
    }

    private void ScoreCompare()
    {
        int[] ranks = new int[playerScore.Length];
        List<(int index, int score)> scoreList = new List<(int, int)>();

        for (int i = 0; i < playerScore.Length; i++)
        {
            scoreList.Add((i, playerScore[i]));
        }

        // 점수를 기준으로 내림차순 정렬
        scoreList = scoreList.OrderByDescending(x => x.score).ToList();

        int currentRank = 1; 
        for (int i = 0; i < scoreList.Count; i++)
        {
            if (i > 0 && scoreList[i].score != scoreList[i - 1].score)
            {
                currentRank = i + 1;
            }

            ranks[scoreList[i].index] = currentRank;
        }

        for (int i = 0; i < playerScore.Length; i++)
        {
            winnerText[i].text = ranks[i].ToString() + "등";
        }
    }

    //카드 리스트 셔플
    public List<T> ShuffleList<T>(List<T> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--) //마지막 인덱스부터 첫번째 인덱스까지 거꾸로 반복
        {
            int ran = UnityEngine.Random.Range(0, i); //랜덤한 인덱스 선택
            T temp = _list[i]; //기존 리스트를 임시로 저장
            _list[i] = _list[ran];//선택한 인덱스 값의 위치를 교체
            _list[ran] = temp; //임시로 저장한 값을 리스트에 저장
        }

        return _list; //리스트 반환
    }
}
