using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Transform p;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = p.position;
    }
}
