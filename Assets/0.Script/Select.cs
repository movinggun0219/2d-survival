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

    [SerializeField] private Image[] panels;
    void Start()
    {
        titleImg.transform.DOMoveX(640, 3f)
            .SetDelay(0.5f)
            .SetEase(Ease.OutElastic)
            .OnComplete(() =>
            {
                StartCoroutine(MovePanel());
            })
            ;
    }
    IEnumerator MovePanel()
    {
        for(int i = 0; i < panels.Length; i++)
        {
            panels[i].transform.DOMoveY(255f,1f)
                .SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void OnGame(int num)
    {
        GameManager.Instance.selectNum = num;
        SceneChanger.Instance.Game();
    }
}
