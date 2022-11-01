using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy1
{
    [SerializeField] float shootCooldownMax = 2f;
    [SerializeField] float shootCooldownMin = 4f;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowSpeed = 1f;
    [SerializeField] float arrowDamage = 10f;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(Random.Range(shootCooldownMin, shootCooldownMax));
        if (!base.isDead)
        {
            Vector3 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject bullet = Instantiate(arrow, transform.position, rotation);

            Bullet bulletScript = bullet.GetComponent<Bullet>();

            bulletScript.bulletSpeed = arrowSpeed;
            bulletScript.damage = arrowDamage;
            bulletScript.kb = 0f;
            bulletScript.SetDamageLayer(6);
            StartCoroutine(Shoot());
        }
    }
}
