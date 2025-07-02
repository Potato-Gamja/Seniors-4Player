using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    //���� �ٽ��ϱ�
    public void Retry()
    {
        GameManager.Instance.cardList.Clear();
        GameManager.Instance.playerScore = new int[] { 0, 0, 0, 0 };
        GameManager.Instance.count = 0;
        GameManager.Instance.timer = 0;

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //�κ�� ����
    public void GoRoby()
    {
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene("RobyScene");
    }
}
