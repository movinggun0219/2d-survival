using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy0 : Enemy
{

    void Start()
    {
        data.speed = ableData.Speed;
        data.hp = ableData.HP;
        data.exp = ableData.Exp;
        data.atkRange = ableData.AtkRange;
        data.atkSpeed = ableData.AtkSpeed;
        data.power = ableData.Power;

        sa = GetComponent<SpriteAnimation>();
        sr = GetComponent<SpriteRenderer>();

        data.speed = 1;
        enemyNum = 0;

        List<Sprite> sprite = ResManager.Instance.enemySprite[enemyIndex].charSprite.runSprite.ToList();
        sa.SetSprite(sprite, 0.25f / data.speed);
    }
}
