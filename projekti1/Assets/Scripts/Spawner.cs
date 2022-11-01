using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject
{
    public GameObject enemy;
    public bool enemyActive;
    public int enemyStartBatch;
    public float enemyInterval;
    public int enemyAmount;

    public EnemyObject(GameObject enemy, bool enemyActive, int enemyStartBatch, float enemyInterval, int enemyAmount)
    {
        this.enemy = enemy;
        this.enemyActive = enemyActive;
        this.enemyStartBatch = enemyStartBatch;
        this.enemyInterval = enemyInterval;
        this.enemyAmount = enemyAmount;
    }
}

public class Spawner : MonoBehaviour
{
    [Header("Enemy1")]
    [SerializeField] GameObject enemy1;
    [SerializeField] bool enemy1Active;
    [SerializeField] int enemy1StartBatch;
    [SerializeField] float enemy1Interval = 5f;
    [SerializeField] int enemy1Amount;

    [Header("Enemy2")]
    [SerializeField] GameObject enemy2;
    [SerializeField] bool enemy2Active;
    [SerializeField] int enemy2StartBatch;
    [SerializeField] float enemy2Interval = 5f;
    [SerializeField] int enemy2Amount;

    [Header("General")]
    [SerializeField] bool startingBatch;
    [SerializeField] Vector2 spawnAreaMin;
    [SerializeField] Vector2 spawnAreaMax;

    // Start is called before the first frame update
    void Start()
    {
        List<EnemyObject> enemies = new List<EnemyObject>();
        enemies.Add(new EnemyObject(enemy1, enemy1Active, enemy1StartBatch, enemy1Interval, enemy1Amount));
        enemies.Add(new EnemyObject(enemy2, enemy2Active, enemy2StartBatch, enemy2Interval, enemy2Amount));

        if (startingBatch)
        {
            foreach (EnemyObject e in enemies)
            {
                StartSpawn(e);
            }
        }
        foreach (EnemyObject e in enemies)
        {
            if (e.enemyActive)
            {
                StartCoroutine(SpawnEnemy(e, false));
            }
        }
    }

    private void StartSpawn(EnemyObject e)
    {
        for (int i = 0; i < e.enemyStartBatch; i++)
        {
            StartCoroutine(SpawnEnemy(e, true));
        }
    }

    private IEnumerator SpawnEnemy(EnemyObject e, bool start)
    {
        //check if starting, then instantly spawn
        float interval;
        if (start) interval = 0;
        else interval = e.enemyInterval;

        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(e.enemy, new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y)), Quaternion.identity);
        StartCoroutine(enemyFadeIn(newEnemy, 1f));
        e.enemyAmount -= 1;
        if (e.enemyAmount > 0 && !start)
        {
            StartCoroutine(SpawnEnemy(e, false));
        }
    }

    private IEnumerator enemyFadeIn(GameObject enemy, float fadeTime)
    {
        enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.6f);
        enemy.GetComponent<Rigidbody2D>().isKinematic = true;
        enemy.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(fadeTime);
        enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        enemy.GetComponent<Rigidbody2D>().isKinematic = false;
        enemy.GetComponent<Collider2D>().enabled = true;
    }
}
