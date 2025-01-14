using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player : MonoBehaviour
{
    SpriteAnimation sa;
    void Start()
    {
        sa = GetComponent<SpriteAnimation>();
        int sel = GameManager.Instance.selectNum;
        sa.SetSprite(ResManager.Instance.charSprite[sel].charSprite.standSprite.ToList(),0.2f);
    }

    void Update()
    {
        Move();
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
    }
}
