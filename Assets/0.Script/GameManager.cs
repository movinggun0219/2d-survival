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
    public float speed;
    public int hp;
    public float exp;
    public int level;
    public string name;
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
