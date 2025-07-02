using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public int stageNum = 0; //스테이지 구분 넘버
    int id = 0; //카드의 짝 맞추기용 아이디

    public GameObject[] cardPrefabs; //카드 프리팹 배열
    public Sprite[] sprites; //카드 스프라이트 배열

    public List<Sprite> spriteList = new List<Sprite>(); //카드이벤트 스크립트 리스트

    public Transform spawnTrans; //카드 스폰 시작 위치

    [SerializeField]
    float posX = 0; //카드의 x 좌표
    [SerializeField] 
    float posY = 0; //카드의 y 좌표

    [SerializeField] 
    float addX; //카드 위치에 더할 x좌표값
    [SerializeField] 
    float addY; //카드 위치에 더할 y좌표값

    [SerializeField] 
    float maxX; //x의 최대값
    [SerializeField] 
    float maxY; //y의 최대값

    private void Awake()
    {
    
        for(int i = 0; i < sprites.Length; i++)
        {
            spriteList.Add(sprites[i]);
        }
        ShuffleList(spriteList);

        switch (stageNum) //스테이지 별 카드 생성
        {
            case 0:
                for (int i = 0; i < cardPrefabs.Length; i++)
                {
                    int ran = Random.Range(0, 4);

                    for (int j = 0; j < 2; j++)
                    {
                        GameObject card = Instantiate(cardPrefabs[i]); //카드 생성
                        card.transform.parent = transform; //생성된 카드의 부모 설정
                        CardEvent cardEvent = card.GetComponent<CardEvent>(); //카드의 카드 이벤트 스크립트 컴포넌트 가져오기
                        GameManager.Instance.cardList.Add(cardEvent); //카드리스트에 카드 이벤트 스크립트 추가
                        cardEvent.id = id; //카드 이벤트 스크립트의 아이디 할당
                        cardEvent.front = spriteList[id]; //카드의 앞면 스프라이트 변경
                        SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>(); //스프라이트 렌더러 컴포넌트 가져오기
                        spriteRenderer.sprite = spriteList[id]; //카드의 스프라이트 변경

                        switch (ran)
                        {
                            case 0:
                                card.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;

                            case 1:
                                card.transform.rotation = Quaternion.Euler(0, 0, 90);
                                break;

                            case 2:
                                card.transform.rotation = Quaternion.Euler(0, 0, 180);
                                break;

                            case 3:
                                card.transform.rotation = Quaternion.Euler(0, 0, -90);
                                break;
                        }
                    }

                    id++; //아이디 값 증가
                }
                break;
                /*
            case 1:
                for (int i = 0; i < cardPrefabs.Length; i++)
                {
                    int ran = Random.Range(0, 4);

                    for (int j = 0; j < 2; j++)
                    {
                        GameObject card = Instantiate(cardPrefabs[i]);
                        card.transform.parent = transform;
                        CardEvent cardEvent = card.GetComponent<CardEvent>();
                        GameManager.Instance.cardList.Add(cardEvent);
                        cardEvent.id = id;
                        cardEvent.front = spriteList[id];
                        SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
                        spriteRenderer.sprite = spriteList[id];

                        switch (ran)
                        {
                            case 0:
                                card.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;

                            case 1:
                                card.transform.rotation = Quaternion.Euler(0, 0, 90);
                                break;

                            case 2:
                                card.transform.rotation = Quaternion.Euler(0, 0, 180);
                                break;

                            case 3:
                                card.transform.rotation = Quaternion.Euler(0, 0, -90);
                                break;
                        }
                    }
                    id++;
                }
                break;

            case 2:
                for (int i = 0; i < cardPrefabs.Length; i++)
                {
                    int ran = Random.Range(0, 4);

                    for (int j = 0; j < 2; j++)
                    {
                        GameObject card = Instantiate(cardPrefabs[i]);
                        card.transform.parent = transform;
                        CardEvent cardEvent = card.GetComponent<CardEvent>();
                        GameManager.Instance.cardList.Add(cardEvent);
                        cardEvent.id = id;
                        cardEvent.front = spriteList[id];
                        SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
                        spriteRenderer.sprite = spriteList[id];

                        switch (ran)
                        {
                            case 0:
                                card.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;

                            case 1:
                                card.transform.rotation = Quaternion.Euler(0, 0, 90);
                                break;

                            case 2:
                                card.transform.rotation = Quaternion.Euler(0, 0, 180);
                                break;

                            case 3:
                                card.transform.rotation = Quaternion.Euler(0, 0, -90);
                                break;
                        }
                    }
                    id++;
                }
                break;

            case 3:
                for (int i = 0; i < cardPrefabs.Length; i++)
                {
                    int ran = Random.Range(0, 4);

                    for (int j = 0; j < 2; j++)
                    {
                        GameObject card = Instantiate(cardPrefabs[i]);
                        card.transform.parent = transform;
                        CardEvent cardEvent = card.GetComponent<CardEvent>();
                        GameManager.Instance.cardList.Add(cardEvent);
                        cardEvent.id = id;
                        cardEvent.front = spriteList[id];
                        SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
                        spriteRenderer.sprite = spriteList[id];

                        switch (ran)
                        {
                            case 0:
                                card.transform.rotation = Quaternion.Euler(0, 0, 0);
                                break;

                            case 1:
                                card.transform.rotation = Quaternion.Euler(0, 0, 90);
                                break;

                            case 2:
                                card.transform.rotation = Quaternion.Euler(0, 0, 180);
                                break;

                            case 3:
                                card.transform.rotation = Quaternion.Euler(0, 0, -90);
                                break;
                        }
                    }
                    id++;
                }
                break;
                */

        }
        GameManager.Instance.ShuffleList(GameManager.Instance.cardList); //카드 리스트 뒤섞기
        RandomTransform(); //랜덤한 위치로 이동
        
        GameManager.Instance.SetGame();

    }

    //카드 랜덤한 위치
    public void RandomTransform()
    {
        for (int i = 0; i < GameManager.Instance.cardList.Count; i++)
        {
            //순서가 바뀐 카드 리스트의 인덱스 0부터 위치 변경
            GameManager.Instance.cardList[i].gameObject.transform.position =
                                    new Vector3(spawnTrans.position.x + posX, 
                                                spawnTrans.position.y + posY, 
                                                spawnTrans.position.z);

            posX += addX; //x값 더하기 > 오른쪽 방향으로 카드가 순차적으로 이동

            if(posX >= maxX) //x값 끝에 달했을 경우 > 다시 왼쪽에서부터 위치가 변경되게 하기 위함
            {
                posX = 0; //x값 초기화 > 다시 처음 왼쪽 위치부터 설정하기 위함
                posY -= addY; //y값 빼기 > 카드가 아래 행에서 위치되게 하기 위함
            }
        }
    }
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
