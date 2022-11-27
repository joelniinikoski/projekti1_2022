using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject pivot;
    public GameObject bulletPrefab;
    public float bulletSpeed = 0.1f;
    public float coolDown = 0.5f;
    [SerializeField] float bulletDmg = 10;
    [SerializeField] float bulletKb = 50;

    float coolDownTimer = 0f;

    private void OnEnable()
    {
        EventManager.OnPlayerLevelUp += UpdateStats;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerLevelUp -= UpdateStats;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("BulletPower")) PlayerPrefs.SetFloat("BulletPower", bulletDmg);
        else bulletDmg = PlayerPrefs.GetFloat("BulletPower");
        if (!PlayerPrefs.HasKey("BulletSpeed")) PlayerPrefs.SetFloat("BulletSpeed", bulletSpeed);
        else bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed");
        if (!PlayerPrefs.HasKey("ReloadSpeed")) PlayerPrefs.SetFloat("ReloadSpeed", coolDown);
        else coolDown = PlayerPrefs.GetFloat("ReloadSpeed");
        if (!PlayerPrefs.HasKey("BulletKnockback")) PlayerPrefs.SetFloat("BulletKnockback", bulletKb);
        else bulletKb = PlayerPrefs.GetFloat("BulletKnockback");
    }

    // Update is called once per frame
    void Update()
    {
        //direction
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = Input.mousePosition - pos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        pivot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //shooting
        if (coolDownTimer > 0f)
        {
            coolDownTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && coolDownTimer <= 0) {
            Shoot();
            coolDownTimer = coolDown;
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, pivot.transform.position, pivot.transform.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.bulletSpeed = bulletSpeed;
        bulletScript.damage = bulletDmg;
        bulletScript.kb = bulletKb;
        bulletScript.SetDamageLayer(7);
    }

    void UpdateStats()
    {
        bulletSpeed = PlayerPrefs.GetFloat("BulletSpeed");
        bulletDmg = PlayerPrefs.GetFloat("BulletPower");
        coolDown = PlayerPrefs.GetFloat("ReloadSpeed");
        bulletKb = PlayerPrefs.GetFloat("BulletKnockback");
    }
}
