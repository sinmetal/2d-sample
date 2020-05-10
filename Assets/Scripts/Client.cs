using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject defaultTarget;
    private List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 矢印キーの入力情報を取得する
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var velocity = new Vector3(h, v) * 1;
        transform.localPosition += velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var bulletCon = bullet.GetComponentInChildren<BulletController>();
            bulletCon.SetDefaultTarget(defaultTarget);
            bullets.Add(bullet);
        }

        foreach (var bullet in bullets)
        {
            //弾のvelocityにベクトルを入れる
            //弾からターゲットへのベクトルを求めて、正規化し任意の速さ3をかける
            var bulletCon = bullet.GetComponentInChildren<BulletController>();
            if (bulletCon == null)
            {
                Debug.Log("bulletCon is null");
                return;
            }
            bullet.GetComponentInChildren<Rigidbody2D>().velocity = (bulletCon.GetTarget().transform.position - bullet.transform.position).normalized * 3;
        }
    }
}
