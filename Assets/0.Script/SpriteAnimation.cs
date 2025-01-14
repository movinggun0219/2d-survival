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

    //  sprite ������ Ÿ��
    private float spriteDelayTime;

    // ������Ÿ�� 0
    private float delayTime = 0f;
    private int spriteAnimationIndex = 0;
    private UnityAction action = null;
    public bool isCanvas = false;

    private void Start()
    {
        // isCanvas�� true�� �� �̹�����
        if (isCanvas)
        {
            image = GetComponent<Image>();
        }
        // �ƴ� ���� SpriteRenderer�� 
        else
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }
    private void Update()
    {
        // sprite�� ������ ó�� ���� ����
        if (sprites.Count == 0)
        {
            return;
        }
        // ������ ���� �ð��� ����
        delayTime += Time.deltaTime;

        if (delayTime >= spriteDelayTime)
        {
            delayTime = 0;

            // isCanvas�� true�϶�
            if (isCanvas)
            {
                // �̹��� sprite ��  List�ȿ� �ִ� sprites�� spriteAnimationIndex�� ������ ����
                image.sprite = sprites[spriteAnimationIndex];
            }
            else
            {
                //isCanvas�� false �϶� ��������Ʈ�������� ����
                sr.sprite = sprites[spriteAnimationIndex];
                spriteAnimationIndex++;
            }
              // sprite�� ������ �Ѿ��
            if (spriteAnimationIndex >= sprites.Count)
            {
                // action �� ���� ��
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
        // 0�ʿ��� float �ִ밪
        delayTime = float.MaxValue;
        // ������ ���� ���� ��������Ʈ�� ���� ����
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
        // ���� �׼�(��������Ʈ)�� ����Ƽ �׼� ��
        this.action = action;
    }
}
