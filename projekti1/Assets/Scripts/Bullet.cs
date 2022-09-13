using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float timer = 10f;

    float bulletSpeed;
    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }
    private void Update()
    {
        if (timer <= 0f) {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
        transform.Translate(transform.right * bulletSpeed * Time.deltaTime, Space.World);
    }
}
