using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 랜덤스폰 
    // 위에서 언급한 plane의 자식인 respawnRange 오브젝트
    List<BoxCollider2D> rangecollier = new List<BoxCollider2D>();

    [SerializeField] private List<Enemy> enemies;

    private float timer;
    [SerializeField]
    private float delay;

    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            rangecollier.Add(transform.GetChild(i).GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameState.Stop)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer > delay )
        {
            timer = 0;
            int rand = Random.Range(0,enemies.Count);
            int spawnRand = Random.Range(0, rangecollier.Count);
            BoxCollider2D box = rangecollier[spawnRand];
            Instantiate(enemies[rand],Return_RandomPosition(box.gameObject,box),Quaternion.identity);
        }
    }
    Vector3 Return_RandomPosition(GameObject rangeObject,BoxCollider2D rangeCollier)
    {
        Vector3 originPosition = rangeObject.transform.position;

        float range_X = rangeCollier.bounds.size.x;
        float range_Y = rangeCollier.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPosition = new Vector3(range_X, range_Y, 0f);

        Vector3 respawnPosition = originPosition + RandomPosition;
        return respawnPosition;
    }
}
