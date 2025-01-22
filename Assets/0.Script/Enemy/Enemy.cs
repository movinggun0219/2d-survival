using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] protected int enemyIndex;
    [SerializeField] private List<Exp> exps;
    [SerializeField] protected EnemyableData ableData;

    protected int enemyNum;

    protected Player p;
    protected SpriteAnimation sa;
    protected SpriteRenderer sr;

    protected EnemyData data =  new EnemyData();

    float hitTimer = 0;
    void Update()
    {
        if(p == null)
        {
            p = GameManager.Instance.P;
            return;
        }

        if(data.hp <= 0)
        {
            return;
        }

        if (hitTimer >= 0)
        {
            hitTimer -= Time.deltaTime;
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
                Destroy(gameObject,1f);
            }
            else
            {
                Hit();
            }
        }
    }

    void Hit()
    {
        float hitTime = 0.1f;
        // 맞는 애니 
        List<Sprite> sprite = ResManager.Instance.enemySprite[enemyIndex].charSprite.hitSprite.ToList();
        sa.SetSprite(sprite, hitTime, Run);
        hitTimer = hitTime;
    }

    void Run()
    {
        List<Sprite> sprite = ResManager.Instance.enemySprite[enemyIndex].charSprite.runSprite.ToList();
        sa.SetSprite(sprite, 0.25f / data.speed);
    }

    void Dead()
    {
        // 죽는 애니 
        List<Sprite> sprite = ResManager.Instance.enemySprite[enemyIndex].charSprite.deadSprite.ToList();
        sa.SetSprite(sprite,0.2f);

        //경험치 드랍
        int index = data.exp < 50 ? 0 : data.exp<100 ? 1:2;
        Exp e = Instantiate(exps[index],transform.position,Quaternion.identity);
        e.SetExp(data.exp);
    }
}
