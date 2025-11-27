using System.Collections;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] GameObject bossSpawnEffect;
    [SerializeField] Transform playerTarget;
    [SerializeField] float riseHeight = 10f;
    [SerializeField] float riseDuration = 3f;
    Vector3 initialPosition;
    bool isSpawned = false;

    void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isSpawned)
        {
            this.transform.LookAt(playerTarget);
        }
    }


    public void StartBossRise()
    {
        StartCoroutine(RiseBoss());
    }

    IEnumerator RiseBoss()
    {
        isSpawned = false;
        transform.position = initialPosition;

        Vector3 startPos = transform.position;
        Vector3 targetPos = initialPosition + Vector3.up * riseHeight;

        float elapsed = 0f;

        while (elapsed < riseDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / riseDuration);
            bossSpawnEffect.SetActive(true);
            elapsed += Time.deltaTime;
            yield return null;
        }
        isSpawned = true;
    }



}
