using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharacterState
{
    Stand,
    Run,
    Dead
}

public class PlayerData
{
    private float speed;
    public float Speed
    {
        get { return speed; }
        set 
        {
            speed = value;
        }
    }
    private int hp;
    public int Hp
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }
    private float exp;
    public float Exp
    {
        get { return exp; }
        set
        {
            UI ui = FindUI();
            if(FindUI() != null)
            {
                exp = value;
                ui.UIExp(exp,MaxExp);
            }
        }
    }
    public float MaxExp { get; set; }
    
    private float level;
    public float Level
    {
        get { return level; }
        set
        {
            level = value;
        }
    }

    private string name;
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
        }
    }

    UI FindUI()
    {
        return GameObject.FindObjectOfType<UI>();
    }
}

public class EnemyData
{
    public float speed;
    public int hp;
    public float exp;
}

public class GameManager : Singleton<GameManager>
{
    public int selectNum = 0;
    public float playerSpeed = 0;

    public CharacterState cState = CharacterState.Stand;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private Player p;
    public Player P
    {
        get
        {
            if(p == null)
            {
                p = FindObjectOfType<Player>();
            }
            return p;
        }
    }
}
