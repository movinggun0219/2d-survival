using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy ", menuName = "Data/Enemy")]
public class EnemyableData : ScriptableObject
{
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
        }
    }

    [SerializeField] private int hp;
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }

    [SerializeField] private float exp;
    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;
        }
    }

    [SerializeField] private float atkSpeed;
    public float AtkSpeed
    {
        get { return atkSpeed; }
        set
        {
            atkSpeed = value;
        }
    }

    [SerializeField] private float atkRange;
    public float AtkRange
    {
        get { return atkRange; }
        set
        {
            atkRange = value;
        }
    }

    [SerializeField] private int power;
    public int Power
    {
        get { return power; }
        set
        {
            power = value;
        }
    }
}
