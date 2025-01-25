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
    public int Power {  get; set; } 
    public float AtkDelay {  get; set; }

    public float BaseSpeed { get; set; }

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
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            UI ui = FindUI();
            if (ui != null)
            {
                ui.ReflushHP();
            }
        }
    }

    private int maxHP;
    public int MaxHP
    {
        get { return maxHP; }
        set
        {
            maxHP = value;
            UI ui = FindUI();
            if (ui != null)
            {
                ui.ReflushHP(false);
            }
        }
    }

    private float exp;
    public float Exp
    {
        get { return exp; }
        set
        {
            UI ui = FindUI();
            if (ui != null)
            {
                exp = value;

                // 경험치 풀 - 수정 필요
                if (exp >= MaxExp)
                {
                    exp -= MaxExp;
                    MaxExp += 20;
                    Level++;
                }

                ui.UIExp(exp, MaxExp);
            }
        }
    }
    public float MaxExp { get; set; }

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            if(level>1)
            {
                UI ui = FindUI();
                if (ui != null)
                {
                    GameManager.Instance.gameState = GameState.Stop;
                    ui.UILevel(level);
                    ui.ShowLevelUP();
                }
            }
           
            
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


    private int killCnt;
    public int KillCnt
    {
        get { return killCnt; }
        set
        {
            killCnt = value;
            UI ui = FindUI();
            if(ui != null)
            {
                ui.UIKillCount(killCnt);
            }
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
    public float atkRange;
    public float atkSpeed;
    public int power;
}

public enum GameState
{
    Play,Stop
}

public class GameManager : Singleton<GameManager>
{
    public int selectNum = 0;

    public CharacterState cState = CharacterState.Stand;
    public GameState gameState = GameState.Play;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private Player p;
    public Player P
    {
        get
        {
            if (p == null)
            {
                p = FindObjectOfType<Player>();
            }
            return p;
        }
    }

    public UI FindUI()
    {
        return GameObject.FindObjectOfType<UI>();
    }
}
