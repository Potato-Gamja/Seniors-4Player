using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject tailPrefab;

    float destroyTime = 20.0f;
	float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= destroyTime)
        {
            timer = 0.0f;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}
