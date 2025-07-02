using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //���ӸŴ��� �̱���
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

    public List<CardEvent> cardList = new List<CardEvent>(); //ī���̺�Ʈ ��ũ��Ʈ ����Ʈ

    SoundManager soundManager;
    [SerializeField] AudioSource flip_AudioSource;

    public GameObject clearPanel; //Ŭ���� �ǳ�
    public float timer = 0.0f; //���� ����
    private int touchCount = 0; //�����⸦ �õ��� Ƚ��

    public CardEvent firstCard; //ù��°�� ������ ī��
    public CardEvent secondCard; //�ι�°�� ������ ī��

    public bool isMatch = true; //���ӽ��� �� ��ġ ����
    public bool isClear = false;

    public int count = 0; //���߱� ������ ī��Ʈ
    public int clearCount; //���߾�� �ϴ� ī��Ʈ

    [SerializeField] GameObject[] playerGroup = new GameObject[4]; //�÷��̾� �׷� ������Ʈ �迭
    [SerializeField] Image[] playerImage = new Image[4]; //�÷��̾� �̹��� ������Ʈ �迭
    public Text[] playerText = new Text[4]; //�÷��̾� �ؽ�Ʈ �迭
    public int[] playerScore = new int[] { 0, 0, 0, 0 }; //�÷��̾� ���� �迭
    public Text[] winnerText = new Text[4];

    public int playerID; //�÷��̾� ���� ���̵� 0~3

    private void Awake()
    {

        //���ӸŴ��� �̱���
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
            StopAllCoroutines(); //�ڷ�ƾ ����
        }
    }

    //���Ӽ���
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

        playerID = Random.Range(0, 4); //��������
        PlayerChanged(); //�������� ����

        //ī���̺�Ʈ ��ũ��Ʈ ����Ʈ �߰�
        //cardList.AddRange(FindObjectsByType<CardEvent>(FindObjectsInactive.Include, //��Ȱ��ȭ�� ������Ʈ ���� ����
        //  

        clearCount = cardList.Count / 2; //Ŭ���� ī��Ʈ üũ
        Invoke(nameof(GameStart), 5f); //���ӽ�ŸƮ �Լ� 5�� �ڿ� ����
        Invoke(nameof(CardBoxCol), 6f);
    }

    //���ӽ���
    private void GameStart()
    {
        foreach (CardEvent card in cardList) //ī�� �̺�Ʈ�� ���� ����Ʈ �ݺ� ȣ��
        {
            card.FlipCardImage(); //ī�� ������ > �ո鿡�� �޸����� ������
        }
        flip_AudioSource.Play();
        isMatch = false; //��ġ �����ϰ� ��ġ���� ����
        StartCoroutine(TimerCoroution()); //Ÿ�̸� ����
    }

    private void CardBoxCol()
    {
        foreach (CardEvent card in cardList) //ī�� �̺�Ʈ�� ���� ����Ʈ �ݺ� ȣ��
        {
            card.BoxCol(); //ī�� �ڽ� �ݶ��̴� Ȱ��ȭ
        }
    }

    //���� Ŭ����
    private void GameClear()
    {
        ScoreCompare();
        clearPanel.SetActive(true); //Ŭ���� �ǳ� Ȱ��ȭ
        soundManager.VictorySFX_Play();
    }

    //Ÿ�̸� �ڷ�ƾ
    private IEnumerator TimerCoroution()
    {
        var wait = new WaitForSeconds(1f);

        timer += 1; //�ð� 1�� ����
        yield return wait; //1�� ������
        StartCoroutine(TimerCoroution()); //�ڷ�ƾ ����
    }

    //ī�� ���߱�
    public void Matched()
    {
        isMatch = true; //ī�� ��Ī ���� ��
        touchCount++; //������ ī��Ʈ ����
        if (firstCard.id == secondCard.id) //���� ���� ī���� ���
        {
            firstCard.DestroyCard(); //ī�� ����
            secondCard.DestroyCard(); //ī�� ����
            count++; //����Ŭ���� ī��Ʈ ����

            playerScore[playerID]++; //�÷��̾� ���� ����
            playerText[playerID].text = playerScore[playerID].ToString(); //�÷��̾� ���� �ؽ�Ʈ �� ����

            //���� Ŭ���� ī��Ʈ ���� ��
            if (count == clearCount)
            {
                Invoke(nameof(GameClear), 1.0f); //���� Ŭ����
            }
        }
        else //���� �ٸ� ī���� ���
        {
            firstCard.CloseCard(); //������
            secondCard.CloseCard(); //������

            playerID++; //�÷��̾� ���̵� ����

            if (playerID >= 4) //�÷��̾� ���̵� �ʱ�ȭ
            {
                playerID = 0; //ó�� ������
            }

            PlayerChanged(); //�÷��̾� ���� ���� ȿ��
        }
    }

    //�÷��̾� ��ü
    public void PlayerChanged()
    {
        int i = playerID - 1;

        if (i < 0)
            i = 3;

        //�÷��̾� ���̵� ����ġ
        switch (playerID)
        {
            case 0: //ù ��° �÷��̾�
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                //�� ���� �÷��̾� �̹��� ���� �� ����
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);
                //�̹� ���� �÷��̾� �̹��� ���� �� ����
                break;

            case 1: //�� ��° �÷��̾�
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);

                break;

            case 2: //�� ��° �÷��̾�
                playerImage[i].color = new Color(playerImage[i].color.r, playerImage[i].color.g, playerImage[i].color.b, 0.05f);
                playerImage[playerID].color = new Color(playerImage[playerID].color.r, playerImage[playerID].color.g, playerImage[playerID].color.b, 1.0f);

                break;

            case 3: //�� ��° �÷��̾�
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

        // ������ �������� �������� ����
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
            winnerText[i].text = ranks[i].ToString() + "��";
        }
    }

    //ī�� ����Ʈ ����
    public List<T> ShuffleList<T>(List<T> _list)
    {
        for (int i = _list.Count - 1; i > 0; i--) //������ �ε������� ù��° �ε������� �Ųٷ� �ݺ�
        {
            int ran = UnityEngine.Random.Range(0, i); //������ �ε��� ����
            T temp = _list[i]; //���� ����Ʈ�� �ӽ÷� ����
            _list[i] = _list[ran];//������ �ε��� ���� ��ġ�� ��ü
            _list[ran] = temp; //�ӽ÷� ������ ���� ����Ʈ�� ����
        }

        return _list; //����Ʈ ��ȯ
    }
}
