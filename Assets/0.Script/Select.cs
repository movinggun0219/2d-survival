using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Select : MonoBehaviour
{
    [SerializeField] private Image titleImg;
    [SerializeField] private TMP_Text title;
    void Start()
    {
        titleImg.transform.DOMoveX(640, 3f)
            .SetDelay(0.5f)
            .SetEase(Ease.OutElastic);
            
    }

    void Update()
    {
        
    }
}
