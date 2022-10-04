using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemy1")]
    [SerializeField] GameObject enemy1;
    [SerializeField] bool enemy1Active;
    [SerializeField] float enemy1Interval = 5f;
    [SerializeField] int enemy1Amount;

    [Header("Spawn Area")]
    [SerializeField] Vector2 spawnAreaMin;
    [SerializeField] Vector2 spawnAreaMax;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy1Active)
        {
            StartCoroutine(spawnEnemy(enemy1, enemy1Interval));
        }
    }

    private IEnumerator spawnEnemy(GameObject enemy, float interval)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y)), Quaternion.identity);
        StartCoroutine(enemyFadeIn(newEnemy, 1f));
        enemy1Amount -= 1;
        if (enemy1Amount > 0)
        {
            StartCoroutine(spawnEnemy(enemy, interval));
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
