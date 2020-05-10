using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSquare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().bounds.size.Set(200, 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        // 矢印キーの入力情報を取得する
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var velocity = new Vector3(h, v) * 1;
        transform.localPosition += velocity;
    }
}
