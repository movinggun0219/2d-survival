using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform shieldParent;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletTrans;

    SpriteAnimation sa;
    GameManager gm;

    int charNum = 0;
    float fireTimer = 0;
    public float delay;

    [HideInInspector] public PlayerData data = new PlayerData();

    void Start()
    {
        data.Speed = 3;
        data.Hp = 50;
        data.Exp = 2;
        data.MaxExp = 120f;
        data.Level = 1;


        sa = GetComponent<SpriteAnimation>();
        charNum = GameManager.Instance.selectNum;
        sa.SetSprite(ResManager.Instance.charSprite[charNum].charSprite.standSprite.ToList(),0.2f);
    }

    void Update()
    {
        Move();

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        fireTimer += Time.deltaTime;
        if (enemies.Length != 0)
        {
            SetRotFirePos(enemies[0].transform);
            if (fireTimer > delay)
            {
                fireTimer = 0;
                Instantiate(bullet, firePos).transform.SetParent(bulletTrans);
            }
        }

        shieldParent.Rotate(Vector3.back * Time.deltaTime * 30f);
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * GameManager.Instance.playerSpeed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * GameManager.Instance.playerSpeed;

        float cX = Mathf.Clamp(transform.position.x + x, -19.5f, 19.5f);
        float cY = Mathf.Clamp(transform.position.y + y, -19.5f, 19.5f);

        transform.position = new Vector3(cX, cY, 0f);

        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (x > 0)
        {
            transform.localScale = Vector3.one;
        }


        if(gm == null)
        {
            gm = GameManager.Instance;
        }
        else
        {
            // 움직이고 있을때 
            if (x != 0 || y != 0)
            {
                if (gm.cState == CharacterState.Stand)
                {
                    gm.cState = CharacterState.Run;
                    List<Sprite> sprite = ResManager.Instance.charSprite[charNum].charSprite.runSprite.ToList();
                    sa.SetSprite(sprite, 0.5f / GameManager.Instance.playerSpeed);
                }
            }
            else
            {
                if (gm.cState == CharacterState.Run)
                {
                    sa.SetSprite(ResManager.Instance.charSprite[charNum].charSprite.standSprite.ToList(), 0.2f);
                    gm.cState = CharacterState.Stand;
                }
            }
        }
    }
    void SetRotFirePos(Transform trans)
    {
        Vector2 vec = trans.localPosition - trans.position;
        float angle = Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    public void SetExp(float exp)
    {
        data.Exp += exp;
    }
}
