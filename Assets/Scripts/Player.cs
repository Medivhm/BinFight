using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;


    public GameObject bulletTemp;
    public GameObject weapon;
    public Joystick joystick;

    private int numPerSec = 5;
    private float coldTime;

    public int blood = 10;
    public delegate void deathNotify();
    public deathNotify onDeath;


    void Start()
    {
        coldTime = 10f;
    }

    void Update()
    {
        coldTime += Time.deltaTime;
        Move();
    }

    private void Move()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 dir = new Vector3(horizontal, vertical).normalized;
        this.transform.Translate(dir * speed * Time.deltaTime);
    }

    private void Shoot()
    {
        GameObject go = Instantiate(bulletTemp);
        go.GetComponent<Bullet>().side = 1;
        go.transform.position = weapon.transform.position;
        coldTime = 0f;
    }

    public void AttackButtonOnClick()
    {
        if (coldTime > 1f / numPerSec)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet.side == -1)
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
        onDeath();
    }
}
