using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    public GameObject shotText = null; // Textオブジェクト
    private Vector3 m_velocity; // 速度

    // 初期化
    void Start()
    {
        // オブジェクトからTextコンポーネントを取得
        Text txt = shotText.GetComponent<Text>();
        // テキストの表示を入れ替える
        txt.text = "hell";
    }

    // 更新
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        Text txt = shotText.GetComponent<Text>();
        // テキストの表示を入れ替える
        txt.text = "hell";

        // 移動する
        transform.localPosition += m_velocity;
    }

    // 弾を発射する時に初期化するための関数
    public void Init(float angle, float speed)
    {
        // 弾の発射角度をベクトルに変換する
        var direction = Utils.GetDirection(angle);

        // 発射角度と速さから速度を求める
        m_velocity = direction * speed;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // 2 秒後に削除する
        Destroy(gameObject, 2);
    }
}
