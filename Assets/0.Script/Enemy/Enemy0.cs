using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Enemy0 : Enemy
{
    
    void Start()
    {
        data.speed = 1.5f;
        data.hp = 60;
        data.exp = 2;

        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();

        data.speed = 1;
        enemyNum = 0;

        List<Sprite> sprite = ResManager.Instance.enemySprite[0].charSprite.runSprite.ToList();
        sa.SetSprite(sprite,0.25f/ data.speed);
    }
}
