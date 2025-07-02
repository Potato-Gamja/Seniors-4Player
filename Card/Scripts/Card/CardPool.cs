using System.Collections.Generic;
using UnityEngine;

public class CardPool : MonoBehaviour
{
    public int stageNum = 0; //�������� ���� �ѹ�
    int id = 0; //ī���� ¦ ���߱�� ���̵�

    public GameObject[] cardPrefabs; //ī�� ������ �迭
    public Sprite[] sprites; //ī�� ��������Ʈ �迭

    public List<Sprite> spriteList = new List<Sprite>(); //ī���̺�Ʈ ��ũ��Ʈ ����Ʈ

    public Transform spawnTrans; //ī�� ���� ���� ��ġ

    [SerializeField]
    float posX = 0; //ī���� x ��ǥ
    [SerializeField] 
    float posY = 0; //ī���� y ��ǥ

    [SerializeField] 
    float addX; //ī�� ��ġ�� ���� x��ǥ��
    [SerializeField] 
    float addY; //ī�� ��ġ�� ���� y��ǥ��

    [SerializeField] 
    float maxX; //x�� �ִ밪
    [SerializeField] 
    float maxY; //y�� �ִ밪

    private void Awake()
    {
    
        for(int i = 0; i < sprites.Length; i++)
        {
            spriteList.Add(sprites[i]);
        }
        ShuffleList(spriteList);

        switch (stageNum) //�������� �� ī�� ����
        {
            case 0:
                for (int i = 0; i < cardPrefabs.Length; i++)
                {
                    int ran = Random.Range(0, 4);

                    for (int j = 0; j < 2; j++)
                    {
                        GameObject card = Instantiate(cardPrefabs[i]); //ī�� ����
                        card.transform.parent = transform; //������ ī���� �θ� ����
                        CardEvent cardEvent = card.GetComponent<CardEvent>(); //ī���� ī�� �̺�Ʈ ��ũ��Ʈ ������Ʈ ��������
                        GameManager.Instance.cardList.Add(cardEvent); //ī�帮��Ʈ�� ī�� �̺�Ʈ ��ũ��Ʈ �߰�
                        cardEvent.id = id; //ī�� �̺�Ʈ ��ũ��Ʈ�� ���̵� �Ҵ�
                        cardEvent.front = spriteList[id]; //ī���� �ո� ��������Ʈ ����
                        SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>(); //��������Ʈ ������ ������Ʈ ��������
                        spriteRenderer.sprite = spriteList[id]; //ī���� ��������Ʈ ����

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

                    id++; //���̵� �� ����
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
        GameManager.Instance.ShuffleList(GameManager.Instance.cardList); //ī�� ����Ʈ �ڼ���
        RandomTransform(); //������ ��ġ�� �̵�
        
        GameManager.Instance.SetGame();

    }

    //ī�� ������ ��ġ
    public void RandomTransform()
    {
        for (int i = 0; i < GameManager.Instance.cardList.Count; i++)
        {
            //������ �ٲ� ī�� ����Ʈ�� �ε��� 0���� ��ġ ����
            GameManager.Instance.cardList[i].gameObject.transform.position =
                                    new Vector3(spawnTrans.position.x + posX, 
                                                spawnTrans.position.y + posY, 
                                                spawnTrans.position.z);

            posX += addX; //x�� ���ϱ� > ������ �������� ī�尡 ���������� �̵�

            if(posX >= maxX) //x�� ���� ������ ��� > �ٽ� ���ʿ������� ��ġ�� ����ǰ� �ϱ� ����
            {
                posX = 0; //x�� �ʱ�ȭ > �ٽ� ó�� ���� ��ġ���� �����ϱ� ����
                posY -= addY; //y�� ���� > ī�尡 �Ʒ� �࿡�� ��ġ�ǰ� �ϱ� ����
            }
        }
    }
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
