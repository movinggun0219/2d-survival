using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;
public class UI : MonoBehaviour
{
    [System.Serializable]
    public class ItemUI
    {
        public Image icon;
        public TMP_Text lvTxt;
        public TMP_Text titleTxt;
        public TMP_Text descTxt;
    }
    [SerializeField] private List<ItemUI> itemUIs;

    [Header("프로필 UI")]
    public Image icon;
    public TMP_Text lvTxt;
    public TMP_Text nameTxt;
    public Image hpImg;
    public Image hpdumyImg;
    public Image expImg;

    [Header("그외 UI")]
    public TMP_Text timerTxt;
    public TMP_Text killTxt;

    public Transform toastTrans;
    public TMP_Text toastTxt;
    public Queue<string> quStr = new Queue<string>();
    private bool toastPlay = false;

    [Space(1)]
    private float centerTimer;

    public List<Item> items;

    public List<Image> invenImgs;

    void Start()
    {
        // 캐릭터 얼굴 변경
        int index = GameManager.Instance.selectNum;
        Sprite icon = ResManager.Instance.uiProfileIcon[index];
        this.icon.sprite = icon;

        maxhp = hp = 130;
        killTxt.text = "00.00";
    }

    float hp, maxhp;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            /*hp -= 10; 
            float scaleHp = (hp / maxhp) * 180f;
            hpImg.rectTransform.sizeDelta = new Vector2(scaleHp, 30f);

            //알아보기 
            hpdumyImg.rectTransform.DOSizeDelta(new Vector2 (scaleHp,30f), 1f)
                .SetEase(Ease.Unset);*/

            /*string[] strs = { "가", "나", "다", "라", "마", "바", "사", "아", "자", "치", "카", "타", "파", "하"};
            int rand = Random.Range(0,strs.Length);
            ToastShow(strs[rand]);*/

            List<Item> dumyItem = new List<Item>();
            dumyItem = items.ToList();

            while(dumyItem.Count > 3)
            {
                int rand = Random.Range(0, dumyItem.Count);
                dumyItem.RemoveAt(rand);
            }

            for(int i = 0; i < dumyItem.Count; i++)
            {
                itemUIs[i].icon.sprite = dumyItem[i].Icon;
                itemUIs[i].titleTxt.text = dumyItem[i].ItemName;
                itemUIs[i].descTxt.text = dumyItem[i].Desc;
            }
        }

        if (quStr.Count != 0)
        {
            if (toastPlay == false)
            {
                toastPlay = true;
                toastTxt.text = quStr.Dequeue();
                toastTrans.DOMoveY(80, 0.6f)
                    .SetEase(Ease.OutBounce)
                    .OnComplete(() => {
                        toastTrans.DOMoveY(-20f, 0.3f)
                        .SetEase(Ease.Linear)
                        .SetDelay(2f)
                        .OnComplete(() => { toastPlay = false; });
                    });
            }
        }

        centerTimer += Time.deltaTime;


        // 1안타이머
        /*if((int)(centerTimer / 60) == 0)
        {
            timerTxt.text = $"{(int)(centerTimer % 60)}";
        }
        else
        {
            timerTxt.text = $"{(int)(centerTimer / 60)}:{(int)(centerTimer % 60)}";
        }*/
        //2안 타이머
        timerTxt.text = $"{(int)(centerTimer/60)}:{(int)(centerTimer % 60)}";
    }

    public void UIExp(float exp , float maxExp)
    {
        float scale = (exp / maxhp) * 180f;
        expImg.rectTransform.sizeDelta = new Vector2(scale, 30f);
    }

    public void ToastShow(string str)
    {
        quStr.Enqueue(str);
    }
    public void OnGameEnd()
    {
        SceneChanger.Instance.CharactorSelet();
    }
}
