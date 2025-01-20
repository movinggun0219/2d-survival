using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int enemyNum;

    protected Player p;
    protected SpriteAnimation sa;
    protected SpriteRenderer sr;

    protected EnemyData data =  new EnemyData();

    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.P;
            return;
        }

        Vector2 pos = p.transform.position - transform.position;
        Vector2 dir = pos.normalized * Time.deltaTime * data.speed;

        if (dir.normalized.x > 0)
        {
            sr.flipX = false;
        }
        else if (dir.normalized.x < 0)
        {
            sr.flipX = true;
        }
        // 거리체크
        float dis = Vector2.Distance(p.transform.position,transform.position);
        if (dis > 1)
        {
            transform.Translate(dir);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet e = collision.GetComponent<Bullet>();
        if (e != null)
        {
            Debug.Log("Hit");
            Destroy(collision.gameObject);

            data.hp -= 20;
            if (data.hp <= 0)
            {
                Dead();
                Destroy(e.gameObject);
            }
        }
    }

    void Dead()
    {

    }
}
