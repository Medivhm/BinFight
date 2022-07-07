using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncycleBin : MonoBehaviour
{
    public GameObject uncycleBinTemp;
    public GameObject bulletBananaTemp;
    public GameObject bulletApplecoreTemp;
    public GameObject weapon;
    public float speed = 1f;

    public int blood = 2;
    private int numPerSec = 1;
    private float coldTime;

    public bool isDead;


    public delegate void deathNotify(GameObject go);
    public deathNotify onDeath;
    void Start()
    {
        isDead = false;
        coldTime = 0f;
    }

    void Update()
    {
        coldTime += Time.deltaTime;
        if(coldTime > 1.2f / numPerSec)
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
        Vector3 dir = new Vector3(-1, 0);
        this.transform.Translate(dir * speed);
    }

    private void Shoot()
    {
        GameObject temp = Random.Range(0, 2) == 0 ? bulletApplecoreTemp : bulletBananaTemp;
        GameObject go = Instantiate(temp);
        if(temp == bulletApplecoreTemp)
        {
            go.GetComponent<Bullet>().damage = 1;
        }
        else
        {
            go.GetComponent<Bullet>().damage = 2;
        }
        go.GetComponent<Bullet>().side = -1;
        go.transform.position = weapon.transform.position;
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
