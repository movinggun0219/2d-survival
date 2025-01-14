using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Title : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text keyTxt;
    [SerializeField] private Image titleImg;
    [SerializeField] private GameObject btnObj;

    void Start()
    {
        titleImg.transform.DOMoveY(400f, 2f)
            .SetDelay(0.5f)
            .SetEase(Ease.OutElastic)
            .OnComplete(() => {
                StartCoroutine(CFade(true));
            });
    }

    IEnumerator CFade(bool isFade)
    {
        if (isFade)
        {
            Tween t = keyTxt.DOFade(1f, 1f);
            yield return t.WaitForCompletion();
            btnObj.SetActive(true);
            StartCoroutine(CFade(false)); //Àç±ÍÇÔ¼ö
        }
        else
        {
            Tween t = keyTxt.DOFade(0f, 1f);
            yield return t.WaitForCompletion();
            StartCoroutine(CFade(true));
        }
    }

    public void Onselect()
    {
        SceneChanger.Instance.CharactorSelet();
    }    
}
