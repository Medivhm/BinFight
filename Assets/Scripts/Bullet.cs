using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 2.7f;
    public int side;  //1为玩家子弹  -1为敌人子弹
    public int damage = 1; //伤害
    
    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        this.transform.Translate(new Vector3(side, 0) * speed);
        if (!Screen.safeArea.Contains(Camera.main.WorldToScreenPoint(this.transform.position)))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy")&& this.side == 1)
        {
            Text text = GameObject.Find("ScoreInGame").GetComponent<Text>();
            int score = Convert.ToInt32(text.text);
            score++;
            text.text = score.ToString();
        }
    }
}
