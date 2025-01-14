using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int selectNum = 0;
    public float playerSpeed = 0;
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
