using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Data;
public class Player : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private Transform shieldParent;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletTrans;

    [Header("���� ����")]
    [SerializeField] private GameObject shieldPrefab; // ���� ������
    [SerializeField] private float shieldRadius = 2f; // ���� �ݰ�
    [SerializeField] private float shieldRotationSpeed = 30f; // ���� ȸ�� �ӵ�

    private List<GameObject> shields = new List<GameObject>(); // ������ ���� ���� ����Ʈ

    SpriteAnimation sa;
    GameManager gm;

    int charNum = 0;
    float fireTimer = 0;

    [HideInInspector] public PlayerData data = new PlayerData();

    void Start()
    {
        // ĳ���� �⺻����
        data.Speed = data.BaseSpeed = 3;
        data.HP = data.MaxHP = 50;
        data.Exp = 0;
        data.MaxExp = 50f;
        data.Level = 1;
        data.AtkDelay = 2f;
        data.Power = 10;

        sa = GetComponent<SpriteAnimation>();
        charNum = GameManager.Instance.selectNum;
        sa.SetSprite(ResManager.Instance.charSprite[charNum].charSprite.standSprite.ToList(),0.2f);

        switch(charNum)
        {
            case 0:
                data.Speed += (data.BaseSpeed *0.1f);
                break;
            case 1:
                data.AtkDelay -= (data.AtkDelay * 0.1f);
                break;
            case 2:
                data.Power += (int)((float)data.Power * 0.2f);
                break;
            case 3:
                break;
        }
    }

    void Update()
    {
        if (GameManager.Instance.gameState == GameState.Stop)
        {
            return;
        }
        Move();

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        fireTimer += Time.deltaTime;
        if (enemies.Length != 0)
        {
            //����� �� ã�� 
            float dis = 5;
            Enemy e = null;
            foreach(var enemy in enemies)
            {
                if(enemy.data.hp <= 0)
                {
                    continue;
                }
                float distance = Vector2.Distance(transform.position,enemy.transform.position);
                if (distance < dis)
                {
                    dis = distance;
                    e = enemy;  
                }
            }

            if(e!= null)
            {
                SetRotFirePos(e.transform);
                if (fireTimer > GameManager.Instance.P.data.AtkDelay)
                {
                    fireTimer = 0;
                    Bullet b = Instantiate(bullet, firePos);
                    b.transform.SetParent(bulletTrans);
                    b.Power = data.Power;
                }
            }
        }

        //shieldParent.Rotate(Vector3.back * Time.deltaTime * shieldRotationSpeed);

        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddShield();
        }

        //���� ����
        if (Input.GetKeyDown(KeyCode.F3))
        {
            AddShield();
        }

        // ���� ��ġ ����
        UpdateShieldPositions();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * GameManager.Instance.P.data.Speed;
        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * GameManager.Instance.P.data.Speed;

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
            // �����̰� ������ 
            if (x != 0 || y != 0)
            {
                if (gm.cState == CharacterState.Stand)
                {
                    gm.cState = CharacterState.Run;
                    List<Sprite> sprite = ResManager.Instance.charSprite[charNum].charSprite.runSprite.ToList();
                    sa.SetSprite(sprite, 0.5f / GameManager.Instance.P.data.Speed);
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
        // Ÿ���� ã�� ���� ��ȯ
        Vector2 vec = transform.position - trans.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    public void AddShield()
    {
        // ���� ������ ����
        GameObject newShield = Instantiate(shieldPrefab,shieldParent);
        shields.Add(newShield);

        // ��ġ ����
        UpdateShieldPositions();
    }
    // ����
    /*void Removeshield()
    {
        GameObject newShield = Instantiate(shieldPrefab, shieldParent);
        shields.Add(newShield);
    }*/

    void UpdateShieldPositions()
    {
        // ���带 �������� ��ġ
        int shieldCount = shields.Count;
        for (int i = 0; i < shieldCount; i++)
        {
            float angle = (360f / shieldCount) * i; // ���� ���
            float radian = angle * Mathf.Deg2Rad;

            Vector3 newPosition = new Vector3(
                Mathf.Cos(radian) * shieldRadius,
                Mathf.Sin(radian) * shieldRadius,
                0
            );
            shields[i].transform.localPosition = newPosition;
            shields[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        shieldParent.Rotate(Vector3.back * Time.deltaTime * shieldRotationSpeed);
    }
    public void SetExp(float exp)
    {
        data.Exp += exp;
    }
    public void Damage(int damage)
    {
        data.HP -= damage;

        if (data.HP <= 0)
        {
            //Time.timeScale = 0;
            GameManager.Instance.FindUI().ShowDeadUI();
        }
    }
}
