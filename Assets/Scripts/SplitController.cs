using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SplitController : MonoBehaviour
{
    public GameObject upperText;
    public GameObject lowerText;
    private List<string> rows = new List<string>();
    private int moveCounter;
    private bool isMoving;
    private string lastKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCounter > 0)
        {
            transform.parent.transform.Translate(+0.2f, 0, 0);
            moveCounter--;
            isMoving = true;
        }
        else if (moveCounter < 0)
        {
            transform.parent.transform.Translate(-0.2f, 0, 0);
            moveCounter++;
            isMoving = true;
        }
        else if (isMoving)
        {
            isMoving = false;
            ViewText();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        var bullet = coll.GetComponent<BulletController>();
        if (bullet != null) {
            rows.Add(bullet.GetText());
            rows.Sort();

            ViewText();

            bullet.DestroyFamily();
        }
    }

    public string GetLastKey()
    {
        return lastKey;
    }

    public int Count()
    {
        return rows.Count;
    }

    private List<string> DivideLowerHalf()
    {
        var ret = rows.GetRange(3, rows.Count - 3);
        rows.RemoveRange(3, rows.Count - 3);
        lastKey = rows[rows.Count - 1];
        ViewText();
        return ret;
    }

    public void Split(GameObject lowerSplit)
    {
        var lower = DivideLowerHalf();
        var splitCon = lowerSplit.GetComponentInChildren<SplitController>();
        splitCon.SetRows(lower);
    }

    public void SetRows(List<string> rows)
    {
        this.rows = rows;
        this.lastKey = rows[rows.Count - 1];
        ViewText();
    }

    public void SetMoveCounter(int count)
    {
        moveCounter = count;
    }

    void ViewText()
    {
        if (rows.Count < 3)
        {
            upperText.GetComponent<TextMeshProUGUI>().text = " Split\n\n " + string.Join("\n ", rows);
        }
        else
        {
            var upperTextValue = " Split\n\n " + string.Join("\n ", rows.GetRange(0, 3));
            upperText.GetComponent<TextMeshProUGUI>().text = upperTextValue;

            var lowerTextValue = " " + string.Join("\n ", rows.GetRange(3, rows.Count - 3));
            lowerText.GetComponent<TextMeshProUGUI>().text = lowerTextValue;
        }
    }
}
