using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnAmountPerPlayer = 1;
    public float spawnCooldown = 10.0f;
    public float timePlayed = 0.0f;

    public GameObject EnemyPrefab;
    public Transform EnemyHolder;

    public Sprite redMonster;
    public Sprite blueMonster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePlayed += Time.deltaTime;
        spawnCooldown -= Time.deltaTime;
        if(spawnCooldown < 0.0f)
        {
            spawnCooldown = 10.0f;
            SpawnNewEnemies();
        }

    }

    void SpawnNewEnemies()
    {
        for(int i = 0; i < spawnAmountPerPlayer; i++)
        {
            //Red
            GameObject redEnemy = Instantiate(EnemyPrefab, new Vector3(Mathf.Cos(Random.Range(0, 360)), 0, Mathf.Sin(Random.Range(0, 360))), Quaternion.identity, EnemyHolder);
            FollowBehaviour redEnemyFollow = redEnemy.GetComponent<FollowBehaviour>();
            if(redEnemyFollow != null)
            {
                redEnemyFollow.FollowNewTarget(FollowBehaviour.TargetColor.Red);
            }
            SpriteRenderer redEnemyRenderer = redEnemy.transform.GetChild(0).GetComponent<SpriteRenderer>();
            if(redEnemyRenderer != null)
            {
                redEnemyRenderer.sprite = blueMonster;
            }

            //Blue
            GameObject blueEnemy = Instantiate(EnemyPrefab, new Vector3(Mathf.Cos(Random.Range(0, 360)), 0, Mathf.Sin(Random.Range(0, 360))), Quaternion.identity, EnemyHolder);
            FollowBehaviour blueEnemyFollow = blueEnemy.GetComponent<FollowBehaviour>();
            if (blueEnemyFollow != null)
            {
                blueEnemyFollow.FollowNewTarget(FollowBehaviour.TargetColor.Blue);
            }
            SpriteRenderer blueEnemyRenderer = blueEnemy.transform.GetChild(0).GetComponent<SpriteRenderer>();
            if (blueEnemyRenderer != null)
            {
                blueEnemyRenderer.sprite = redMonster;
            }
        }
    }
}
