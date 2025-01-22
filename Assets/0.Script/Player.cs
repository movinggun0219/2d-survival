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

    [Header("쉴드 세팅")]
    [SerializeField] private GameObject shieldPrefab; // 쉴드 프리팹
    [SerializeField] private float shieldRadius = 2f; // 쉴드 반경
    [SerializeField] private float shieldRotationSpeed = 30f; // 쉴드 회전 속도

    private List<GameObject> shields = new List<GameObject>(); // 생성된 쉴드 관리 리스트

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
        data.MaxExp = 50f;
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


        //shieldParent.Rotate(Vector3.back * Time.deltaTime * shieldRotationSpeed);

        if (Input.GetKeyDown(KeyCode.F2))
        {
            AddShield();
        }

        // 쉴드 위치 갱신
        UpdateShieldPositions();
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
        // 타켓을 찾아 방향 전환
        Vector2 vec = transform.position - trans.position;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    void AddShield()
    {
        // 쉴드 프리팹 생성
        GameObject newShield = Instantiate(shieldPrefab,shieldParent);
        shields.Add(newShield);

        // 위치 갱신
        UpdateShieldPositions();
    }

    void UpdateShieldPositions()
    {
        // 쉴드를 원형으로 배치
        int shieldCount = shields.Count;
        for (int i = 0; i < shieldCount; i++)
        {
            float angle = (360f / shieldCount) * i; // 각도 계산
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
}
