using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))] 
public class SpriteAnimation : MonoBehaviour
{
    private List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer sr;
    private Image image;

    //  sprite 딜레이 타임
    private float spriteDelayTime;

    // 딜레이타임 0
    private float delayTime = 0f;
    private int spriteAnimationIndex = 0;
    private UnityAction action = null;
    public bool isCanvas = false;

    private void Start()
    {
        // isCanvas가 true일 때 이미지로
        if (isCanvas)
        {
            image = GetComponent<Image>();
        }
        // 아닐 때는 SpriteRenderer로 
        else
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }
    private void Update()
    {
        // sprite가 없을때 처리 되지 않음
        if (sprites.Count == 0)
        {
            return;
        }
        // 프레임 간격 시간을 저장
        delayTime += Time.deltaTime;

        if (delayTime >= spriteDelayTime)
        {
            delayTime = 0;

            // isCanvas가 true일때
            if (isCanvas)
            {
                // 이미지 sprite 가  List안에 있는 sprites의 spriteAnimationIndex의 값으로 변경
                image.sprite = sprites[spriteAnimationIndex];
            }
            else
            {
                //isCanvas가 false 일때 스프라이트랜더러로 들어옴
                sr.sprite = sprites[spriteAnimationIndex];
                spriteAnimationIndex++;
            }
              // sprite의 갯수를 넘어설때
            if (spriteAnimationIndex >= sprites.Count)
            {
                // action 가 없을 때
                if (action == null)
                {
                    spriteAnimationIndex = 0;
                }
                else
                {
                    sprites.Clear();
                    action();
                    action = null;
                }
            }
        }
    }

    void Init(List<Sprite> argSprites, float delayTime)
    {
        // 0초에서 float 최대값
        delayTime = float.MaxValue;
        // 기존에 남아 있을 스프라이트를 완전 삭제
        sprites.Clear();

        spriteAnimationIndex = 0;
        sprites = argSprites.ToList();
        spriteDelayTime = delayTime;
    }

    public void SetSprite(List<Sprite> argSprites, float delayTime)
    {
        Init(argSprites, delayTime);
    }
                                                     
    public void SetSprite(List<Sprite> argSprites, float delayTime, UnityAction action)
    {
        Init(argSprites, delayTime);
        // 나의 액션(델리게이트)이 유니티 액션 값
        this.action = action;
    }
}
