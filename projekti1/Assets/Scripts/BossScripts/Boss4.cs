using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : BossScript
{
    [SerializeField] Vector2 spawnAreaMin;
    [SerializeField] Vector2 spawnAreaMax;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] Material spawnMaterial;
    [System.NonSerialized]
    public int iteration = 0;
    float trueStartHealth = 999999;

    public override void Start()
    {
        base.Start();
        if (iteration == 0) trueStartHealth = startHealth;
    }

    public override void Update()
    {
        health = boss.GetHealth();
        if (healthBarBack)
        {
            healthBarBack.transform.position = Camera.main.WorldToScreenPoint(transform.position) + new Vector3(0f, 50f, 0f);
            hpBar.fillAmount = health / startHealth;
        }

        Multiply(2);
        Multiply(4);
        Multiply(8);

        if (health <= 0 && prevHealth > 0)
        {
            if (healthBarBack)
            {
                Destroy(healthBarBack, 0.5f);
            }
        }
        
        prevHealth = health;
    }
    
    public void Multiply(int division)
    {
        if (base.boss.GetHealth() <= trueStartHealth / division && base.prevHealth > trueStartHealth / division)
        {
            GameObject gO = Instantiate(bossPrefab, new Vector3(Random.Range(spawnAreaMin.x, spawnAreaMax.x), Random.Range(spawnAreaMin.y, spawnAreaMax.y)), Quaternion.identity);
            GameObject hBB = Instantiate(base.healthBarBack);
            hBB.transform.SetParent(GameObject.Find("Canvas").transform);
            gO.GetComponent<Boss4>().healthBarBack = hBB;
            gO.GetComponent<Boss4>().iteration = iteration + 1;
            gO.GetComponent<Boss4>().trueStartHealth = trueStartHealth;
            gO.GetComponent<Boss4>().health = trueStartHealth / division;
            gO.GetComponent<Boss4>().prevHealth = trueStartHealth / division;
            gO.transform.localScale = gameObject.transform.localScale / 1.3f;
            gO.GetComponent<SpriteRenderer>().material = spawnMaterial;
        }
    }
}
