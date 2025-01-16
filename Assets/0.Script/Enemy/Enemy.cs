using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float speed;
    protected int enemyNum;

    protected Player p;
    protected SpriteAnimation sa;
    protected SpriteRenderer sr;
    
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.P;
            return;
        }

        Vector2 pos = p.transform.position - transform.position;
        Vector2 dir = pos.normalized * Time.deltaTime * 1.5f;

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
}
