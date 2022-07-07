using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleBin : MonoBehaviour
{
    public GameObject cycleBinTemp;
    public GameObject batteryTemp;
    public float speed = 1f;

    public int blood = 1;
    private int numPerSec = 1;
    private float coldTime;

    public bool isDead;

    public delegate void deathNotify(GameObject go);
    public deathNotify onDeath;

    void Start()
    {
        coldTime = 0f;
    }

    void Update()
    {
        coldTime += Time.deltaTime;
        if (coldTime > 1.7f / numPerSec)
        {
            Shoot();
        }
        Move();
        if (!Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)))
        {
            Die();
        }
    }
    private void Move()
    {
        Vector3 dir = new Vector3(-1, Mathf.Sin(Time.time) * 1f);
        this.transform.Translate(dir * speed);
    }

    private void Shoot()
    {
        GameObject go = Instantiate(batteryTemp);
        go.GetComponent<Bullet>().side = -1;
        go.transform.position = this.transform.position;
        coldTime = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet.side == 1)
            {
                blood -= bullet.damage;
                Destroy(collision.gameObject);
                if (blood <= 0)
                {
                    Die();
                }
            }
        }
    }

    private void Die()
    {
        if (isDead)
            return;
        isDead = true;
        onDeath(this.gameObject);
    }

}
