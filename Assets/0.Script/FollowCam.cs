using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = GameManager.Instance.P;
    }

    // Update is called once per frame
    void Update()
    {
        if (p == null)
        {
            p = GameManager.Instance.P;
            return ;
        }
        Vector3 pos = p.transform.position; 

        float cX = Mathf.Clamp(pos.x, -11f, 11f);
        float cY = Mathf.Clamp(pos.y, -15f, 15f);

        //transform.position = new Vector3(cX,cY,- 10);
        transform.position = Vector3.Lerp(transform.position, new Vector3(cX,cY,- 10f),Time.deltaTime *10f);
    }
}
