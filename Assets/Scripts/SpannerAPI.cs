using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpannerAPI : MonoBehaviour
{
    const int MOVE = 32;

    // public GameObject nodePrefab;
    public GameObject splitPrefab;

    private List<GameObject> splits = new List<GameObject>();

    public GameObject bulletPrefab;
    public GameObject defaultTarget;
    private List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //var nodePosition = new Vector3(transform.position.x, transform.position.y - 10) * 3;
        //var newNode = Instantiate(nodePrefab, nodePosition, Quaternion.identity);

        var splitPosition = new Vector3(transform.position.x, transform.position.y - 10);
        var newSplit = Instantiate(splitPrefab, splitPosition, Quaternion.identity);
        splits.Add(newSplit);
    }

    // Update is called once per frame
    void Update()
    {
        SplitBreaker();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bulletPosition = new Vector3(transform.position.x, transform.position.y - 1);
            var bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
            var bulletCon = bullet.GetComponentInChildren<BulletController>();
            bullets.Add(bullet);
        }

        foreach (var bullet in bullets)
        {
            
            var bulletCon = bullet.GetComponentInChildren<BulletController>();
            if (bulletCon == null)
            {
                bullets.Remove(bullet);
                return;
            }
            Targeting(bulletCon);

            //弾のvelocityにベクトルを入れる
            //弾からターゲットへのベクトルを求めて、正規化し任意の速さ3をかける
            bullet.GetComponentInChildren<Rigidbody2D>().velocity = (bulletCon.GetTarget().transform.position - bullet.transform.position).normalized * 3;
        }
    }

    void SplitBreaker()
    {
        for (int i = 0; i < splits.Count; i++)
        {
            var split = splits[i];
            var splitCon = split.GetComponentInChildren<SplitController>();
            if (splitCon == null)
            {
                Debug.Log("splitCon is null");
                return;
            }

            if (splitCon.Count() > 5)
            {
                var newSplit = Instantiate(splitPrefab, split.transform.position, Quaternion.identity);
                splitCon.Split(newSplit);

                var newSplitCon = newSplit.GetComponentInChildren<SplitController>();
                // もうちょいいい感じの位置指定をしたいけど、今はパターンごとに決め打ちになってる
                if (splits.Count > 1)
                {
                    if (i == 0)
                    {
                        splitCon.SetMoveCounter(-MOVE);
                    }
                    else
                    {
                        splits[0].GetComponentInChildren<SplitController>().SetMoveCounter(-MOVE);
                        splits[1].GetComponentInChildren<SplitController>().SetMoveCounter(-MOVE);
                    }
                }
                else
                {
                    newSplitCon.SetMoveCounter(MOVE);
                }
                splits.Insert(i + 1, newSplit);

                break;
            }
        }
    }

    void Targeting(BulletController bullet)
    {
        for (int i = 0; i < splits.Count; i++)
        {
            var split = splits[i];
            var splitCon = split.GetComponentInChildren<SplitController>();
            if (splitCon == null)
            {
                Debug.Log("splitCon is null");
                return;
            }

            if (bullet.GetText().CompareTo(splitCon.GetLastKey()) < 0)
            {
                bullet.SetTarget(split);
                return;
            }
        }

        if (!bullet.ExistTarget())
        {
            bullet.SetTarget(splits[splits.Count - 1]);
        }
    }

    //void Moses(int centerIndex)
    //{
    //    for (int i = 0; i < splits.Count; i++)
    //    {
    //        var split = splits[i];
    //        var splitCon = split.GetComponentInChildren<SplitController>();
    //        if (i <= centerIndex) {
    //            splitCon.SetMoveCounter(-MOVE);
    //        }
    //        else
    //        {
    //            splitCon.SetMoveCounter(MOVE);
    //        }
    //    }
    //}
}
