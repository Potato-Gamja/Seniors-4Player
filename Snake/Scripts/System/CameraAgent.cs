using UnityEngine;

public class CameraAgent : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;

        cam.transform.position = new Vector3(0f, 5, 0f); //ī�޶� �̵�
        cam.transform.rotation = Quaternion.Euler(90f, 0f, 0f); //����
    }
}
