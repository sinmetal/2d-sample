using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpannerAPI : MonoBehaviour
{
    const int MOVE = 32;

    // public GameObject nodePrefab;
    public GameObject splitPrefab;

    private List<GameObject> splits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //var nodePosition = new Vector3(transform.position.x, transform.position.y - 10) * 3;
        //var newNode = Instantiate(nodePrefab, nodePosition, Quaternion.identity);

        var splitPosition = new Vector3(transform.position.x, transform.position.y - 10) * 3;
        var newSplit = Instantiate(splitPrefab, splitPosition, Quaternion.identity);
        splits.Add(newSplit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        var bullet = coll.GetComponent<BulletController>();
        if (bullet == null)
        {
            Debug.Log("bullet is null");
            return;
        }
        if (bullet.ExistTarget())
        {
            Debug.Log("bullet exist target");
            return;
        }

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

            if (bullet.GetText().CompareTo(splitCon.GetLastKey()) < 0)
            {
                bullet.SetTarget(split);
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
