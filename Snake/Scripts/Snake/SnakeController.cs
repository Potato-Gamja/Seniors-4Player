using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private float baseSpeed = 2f;
    public float rotationSpeed = 100f; // 초당 회전 속도
    private float baseRotspeed = 100f;
    private int rotationDirection = 0; // -1(왼쪽), 1(오른쪽), 0(중립)

    public float segmentSpacing = 0.5f;

    public int score = 0;

    public GameObject bodySegmentPrefab;
    public GameObject tailGroup;

    public Vector3 spawnVec;
    public Vector3 spawnRot;

    bool isDead = false;

    WaitForSeconds moveDelay = new(1.0f);
    WaitForSeconds fadeDelay = new(1.5f);

    Collider col;

    [SerializeField] ParticleSystem boostParticle;

    private List<Transform> bodySegments = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    private Renderer[] renderers;
    private Material[] materials;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        col  = GetComponent<Collider>(); 

        List<Material> mats = new();

        foreach (Renderer r in renderers)
            mats.AddRange(r.materials); // 각 렌더러의 머티리얼들

        materials = mats.ToArray();
    }

    void Start()
    {
        Set();
    }

    void Update()
    {
        if (isDead)
            return;

        Rotate();      // ← 회전 먼저 적용
        MoveForward();
        MoveSegments();

        // 테스트용 입력
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
            TurnLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            TurnRight();
    }

    void Rotate()
    {
        if (rotationDirection != 0)
        {
            float angle = rotationSpeed * rotationDirection * Time.deltaTime;
            transform.Rotate(Vector3.up, angle);
        }
    }

    void MoveForward()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, positions[0]) > segmentSpacing)
        {
            positions.Insert(0, transform.position);
            rotations.Insert(0, transform.rotation);
        }
    }

    void MoveSegments()
    {
        for (int i = 1; i < bodySegments.Count; i++)
        {
            int targetIndex = Mathf.Min((i + 1) * Mathf.RoundToInt(segmentSpacing * 10), positions.Count - 1);

            Vector3 targetPos = positions[targetIndex];
            Quaternion targetRot = rotations[targetIndex];

            bodySegments[i].position = Vector3.MoveTowards(bodySegments[i].position, targetPos, moveSpeed * Time.deltaTime);
            bodySegments[i].rotation = Quaternion.Slerp(bodySegments[i].rotation, targetRot, 0.5f);
        }
    }

    public void AddSegment(GameObject foodObj)
    {
        if (foodObj == null) 
            return;

        Food food = foodObj.GetComponent<Food>();

        if (food == null || food.tailPrefab == null) 
            return;

        string tag = food.tailPrefab.GetComponent<PooledObject>()?.poolTag;

        Transform lastSegment = bodySegments[bodySegments.Count - 1];

        Vector3 spawnPos;
        Quaternion spawnRot;

        if (bodySegments.Count == 1) // 머리만 있을 경우 = 첫 번째 꼬리
        {
            spawnRot = transform.rotation;
            spawnPos = transform.position - transform.forward * segmentSpacing; // 정확한 간격 사용
        }
        else
        {
            spawnRot = lastSegment.rotation;
            spawnPos = lastSegment.position - lastSegment.forward * segmentSpacing;
        }

        GameObject newSegment = ObjectPool.Instance.SpawnFromPool(tag, spawnPos, spawnRot, tailGroup.transform);
        bodySegments.Add(newSegment.transform);

        // 꼬리에 원본 음식 정보 저장
        Tail tail = newSegment.GetComponent<Tail>();
        if (tail != null)
        {
            tail.originFoodPrefab = foodObj;
        }

        score++;
    }

    public void TurnLeft()
    {
        if (isDead)
            return;
        rotationDirection = -1;
    }

    public void TurnRight()
    {
        if (isDead) 
            return;
        rotationDirection = 1;
    }

    public void StopTurning()
    {
        rotationDirection = 0;
    }

    void Die()
    {
        // 꼬리에서 원래 음식대로 생성
        for (int i = 1; i < bodySegments.Count; i++)
        {
            Transform segment = bodySegments[i];
            if (segment != null)
            {
                Tail tail = segment.GetComponent<Tail>();
                Quaternion rot = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));

                if (tail != null && tail.originFoodPrefab != null)
                {
                    string tag = tail.originFoodPrefab.GetComponent<PooledObject>()?.poolTag;
                    if (!string.IsNullOrEmpty(tag))
                    {
                        ObjectPool.Instance.SpawnFromPool(tag, segment.position, rot);
                    }
                }

                segment.gameObject.SetActive(false);
            }
        }

        // 리스트 초기화
        bodySegments.Clear();
        positions.Clear();
        rotations.Clear();

        Set();
        isDead = true;
        col.enabled = false;
        transform.position = spawnVec;
        transform.rotation = Quaternion.Euler(spawnRot);

        StartCoroutine(nameof(Respawn));
    }

    IEnumerator Respawn()
    {
        StartCoroutine(InvincibilityRoutine(1.0f));
        yield return moveDelay;
        isDead = false;
        StartCoroutine(FadeInRoutine(1.0f));
    }

    IEnumerator BlinkRoutine(float duration, float blinkInterval = 0.2f)
    {
        float timer = 0f;
        bool visible = true;

        while (timer < duration)
        {
            visible = !visible;
            foreach (Renderer r in renderers)
            {
                if (r != null)
                    r.enabled = visible;
            }

            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        // 깜빡임 종료 후 무조건 표시
        foreach (Renderer r in renderers)
        {
            if (r != null)
                r.enabled = true;
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (var mat in materials)
        {
            if (mat.HasProperty("_Color"))
            {
                Color c = mat.color;
                c.a = alpha;
                mat.color = c;
            }
        }
    }

    IEnumerator InvincibilityRoutine(float duration)
    {
        SetAlpha(0.2f); // 반투명 시작

        col.enabled = false;
        yield return new WaitForSeconds(duration);
        col.enabled = true;

        SetAlpha(1.0f); // 불투명 복원
    }

    IEnumerator FadeInRoutine(float duration)
    {
        float time = 0f;
        float startAlpha = 0.2f;
        float endAlpha = 1.0f;

        col.enabled = false;
        SetAlpha(startAlpha);

        while (time < duration)
        {
            float t = time / duration;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
            SetAlpha(currentAlpha);

            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(endAlpha);
        col.enabled = true;
    }

    void Set()
    {
        moveSpeed = baseSpeed;
        rotationSpeed = baseRotspeed;
        score = 0;
        bodySegments.Add(transform);
        boostParticle.Stop();

        for (int i = 0; i < 20; i++) // 20개의 위치 미리 추가 (간격 유지)
        {
            Vector3 pos = transform.position - transform.forward * segmentSpacing * i;
            positions.Add(pos);
            rotations.Add(transform.rotation);
        }
    }

    public void BoostSpeed(float boostAmount, float duration)
    {
        if (isDead)
            return;
        StartCoroutine(SpeedBoostRoutine(boostAmount, duration));
        
    }

    private IEnumerator SpeedBoostRoutine(float boostAmount, float duration)
    {
        WaitForSeconds delay = new(duration);

        boostParticle.Play();
        moveSpeed = baseSpeed * boostAmount;
        rotationSpeed = baseRotspeed * boostAmount;
        yield return delay;
        moveSpeed = baseSpeed;
        rotationSpeed = baseRotspeed;
        boostParticle.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead)
            return;

        if (other.CompareTag("Food"))
        {
            AddSegment(other.gameObject);
            other.gameObject.SetActive(false);
        }

        if (bodySegments.Contains(other.transform) && other.transform != transform)
        {
            Tail tail = other.GetComponent<Tail>();
            if (tail != null && tail.IsSafe)
            {
                return; // 무시
            }
            Die();
        }

        if (other.CompareTag("Wall") || other.CompareTag("Player") || other.CompareTag("SnakeTail"))
        {
            Die();
        }

       
    }
}
