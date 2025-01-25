using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Exp : MonoBehaviour
{
    protected float exp;
    [SerializeField] private Player p;

    private bool isFind = false;

    public void SetExp(float exp)
    {
        this.exp = exp;
    }
    //public void SetExp(float exp) => this.exp = exp;  위와 동일 람다식으로 

   
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Stop)
        {
            return;
        }
        if (p == null)
        {
            p = GameManager.Instance.P;
            return;
        }

        float dis = Vector2.Distance(transform.position, p.transform.position);
        if(dis < 3)
        {
            isFind = true;
        }
        if(isFind)
        {
            transform.position = Vector3.Lerp(transform.position, p.transform.position, Time.deltaTime * 1f);
        }

        if(isFind && dis < 1f)
        {
            p.SetExp(exp);
            Destroy(gameObject);
        }
    }
}
