using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Power { get; set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameState == GameState.Stop)
        {
            return;
        }
        transform.Translate(Vector2.up * Time.deltaTime * 10);
    }
}
