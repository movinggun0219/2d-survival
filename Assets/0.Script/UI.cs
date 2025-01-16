using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("프로필 UI")]
    public Image icon;
    public TMP_Text lvTxt;
    public TMP_Text nameTxt;
    public Image hpImg;
    public Image hpdumyImg;
    public Image expImg;

    public List<Image> invenImgs;

    void Start()
    {
        maxhp = hp = 130;
    }

    float hp, maxhp;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            hp -= 10; 
            float scaleHp = (hp / maxhp) * 180f;
            hpImg.rectTransform.sizeDelta = new Vector2(scaleHp, 30f);

            //알아보기 
            hpdumyImg.rectTransform.DOSizeDelta(new Vector2 (scaleHp,30f), 1f)
                .SetEase(Ease.Unset);
        }
    }
}
