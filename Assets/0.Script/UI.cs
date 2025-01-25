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
    private List<Item> dumyItem = new List<Item>();

    public GameObject popup;
    public GameObject lvPopup;
    public GameObject overPopup;

    public List<Image> invenImgs;

    // 키: index, 값: Level
    public Dictionary<int, int> invenLevelDic = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
        popup.SetActive(false);
        // 캐릭터 얼굴 변경
        GameManager gm = GameManager.Instance;
        ResManager rm = ResManager.Instance;

        int index = GameManager.Instance.selectNum;
        Sprite icon = ResManager.Instance.uiProfileIcon[index];
        this.icon.sprite = icon;

        killTxt.text = "0";

        foreach (var invenItem in invenImgs)
        {
            invenItem.gameObject.SetActive(false);
        }

        UILevel(gm.P.data.Level);
        nameTxt.text = $"{rm.charSprite[index].name}";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Stop)
            return;

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.Instance.P.data.Exp += 10;
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

        if ((int)(centerTimer / 60) == 0)
        {
            timerTxt.text = $"{(int)(centerTimer % 60)}";
        }
        else
        {
            timerTxt.text = $"{(int)(centerTimer / 60)}:{(int)(centerTimer % 60)}";
        }
    }
    public void UILevel(int level)
    {
        lvTxt.text = $"Lv{level}";
    }

    public void UIExp(float exp, float maxExp)
    {
        float scale = (exp / maxExp) * 180f;
        expImg.rectTransform.sizeDelta = new Vector2(scale, 30f);
    }

    public void ToastShow(string str)
    {
        quStr.Enqueue(str);
    }


    public void UIKillCount(int count)
    {
        killTxt.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        killTxt.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBounce);

        killTxt.text = $"{count}";
    }

    public void OnGameEnd()
    {
        SceneChanger.Instance.CharacterSelect();
    }

    public void ShowLevelUP()
    {
        dumyItem = items.ToList();
        // 인벤이 꽉착을때 처리
        bool isInvenFull = invenImgs[invenImgs.Count - 1].IsActive() ? true : false;

        if (isInvenFull == true)
        {
            List<Item> itemList = new List<Item>();
            for (int i = 0; i < invenImgs.Count; i++)
            {
                for (int j = 0; j < dumyItem.Count; j++)
                {
                    if (invenImgs[i].sprite.name.Equals(dumyItem[j].Icon.name))
                    {
                        itemList.Add(dumyItem[j]);
                        break;
                    }
                }
            }
            dumyItem.Clear();
            dumyItem = itemList.ToList();
        }

        while (dumyItem.Count > 3)
        {
            int rand = Random.Range(0, dumyItem.Count);
            dumyItem.RemoveAt(rand);
        }

        //------ 리스트 안에 데이터 섞기 ------
        dumyItem = Shuffle(dumyItem);
        List<Item> Shuffle(List<Item> list)
        {
            return list.OrderBy(_ => Random.Range(0, list.Count)).ToList();
        }
        //-------------------------------------

        for (int i = 0; i < dumyItem.Count; i++)
        {
            itemUIs[i].icon.sprite = dumyItem[i].Icon;
            itemUIs[i].titleTxt.text = dumyItem[i].ItemName;
            itemUIs[i].descTxt.text = dumyItem[i].Desc;

            if (invenLevelDic.ContainsKey(dumyItem[i].Index))
            {
                itemUIs[i].lvTxt.text = $"Lv.{invenLevelDic[dumyItem[i].Index]}";
            }
            else
            {
                itemUIs[i].lvTxt.text = "Lv.1";
            }
        }

        lvPopup.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        popup.SetActive(true);
        lvPopup.SetActive(true);
        overPopup.SetActive(false);

        lvPopup.transform.DOScale(1f, 0.6f)
            .SetEase(Ease.OutBounce);
    }

    public void OnLevelupChoice(int index)
    {
        // 0-2 쉴드
        // 3 관통
        // 4 연사 10%
        // 5 샷건 총구 +1 
        // 6 최대 체력 +10
        // 7 이동속도 10%
        // 8 전체회복
        // 9 발사속도 증가
        switch (dumyItem[index].Index)
        {
            case 0:
            case 1:
            case 2:
                GameManager.Instance.P.AddShield();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                GameManager.Instance.P.data.MaxHP += 10;
                break;
            case 7:
                GameManager.Instance.P.data.Speed += (GameManager.Instance.P.data.BaseSpeed * 0.1f);
                break;
            case 8:
                GameManager.Instance.P.data.HP = GameManager.Instance.P.data.MaxHP;
                break;
            case 9:
                break;
        }

        InvenCheck(index);

        lvPopup.transform.DOScale(0.4f, 0.15f)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                GameManager.Instance.gameState = GameState.Play;

                popup.SetActive(false);
                lvPopup.SetActive(false);
                overPopup.SetActive(false);
            });
    }

    void InvenCheck(int index)
    {
        bool isCheck = false;
        foreach (var item in invenImgs)
        {
            if (item.IsActive() && item.sprite.name.Equals(dumyItem[index].Icon.name))
            {
                invenLevelDic[dumyItem[index].Index]++;

                item.sprite = dumyItem[index].Icon;
                item.gameObject.SetActive(true);
                isCheck = true;
                break;
            }
        }

        if (isCheck == false)
        {
            foreach (var item in invenImgs)
            {
                if (!item.IsActive())
                {
                    int key = dumyItem[index].Index;
                    invenLevelDic.Add(key, 1);
                    invenLevelDic[key]++;

                    item.sprite = dumyItem[index].Icon;
                    item.gameObject.SetActive(true);

                    break;
                }
            }
        }
    }

    public void ReflushHP(bool isAnimation = true)
    {
        float hp = GameManager.Instance.P.data.HP;
        float maxHP = GameManager.Instance.P.data.MaxHP;

        float scaleHP = (hp / maxHP) * 180f;
        hpImg.rectTransform.sizeDelta = new Vector2(scaleHP, 30f);

        if (isAnimation)
        {
            hpdumyImg.rectTransform.DOSizeDelta(new Vector2(scaleHP, 30f), 2f)
                .SetEase(Ease.Unset);
        }
        else
        {
            hpdumyImg.rectTransform.sizeDelta = new Vector2(scaleHP, 30f);
        }
    }

    public void ShowDeadUI()
    {
        popup.SetActive(true);
        lvPopup.SetActive(false);
        overPopup.SetActive(true);

        GameManager.Instance.gameState = GameState.Stop;
    }
}
