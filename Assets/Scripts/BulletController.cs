using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BulletController : MonoBehaviour
{
    public GameObject defaultTarget;
    private GameObject nextTarget;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Guid.NewGuid().ToString("N").Substring(0, 4);
    }

    void FixedUpdate()
    {
        if (defaultTarget == null)
        {
            Debug.Log("default target is nothing");
            return;
        }

        const float SPEED = 4f;
        GameObject target = nextTarget;
        if (target == null)
        {
            target = defaultTarget;
        }

        float step = SPEED * Time.deltaTime;
        //Vector3 targetPos = target.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        //transform.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * SPEED;

        if (transform.position.y > Screen.height)
        {
            DestroyFamily();
        }
    }

    public void SetDefaultTarget(GameObject obj)
    {
        this.defaultTarget = obj;
    }

    public void SetTarget(GameObject obj)
    {
        nextTarget = obj;
    }

    public GameObject GetTarget()
    {
        if (nextTarget != null)
        {
            return nextTarget;
        }
        return defaultTarget;
    }

    public bool ExistTarget()
    {
        return (this.nextTarget != null);
    }

    public void DestroyFamily()
    {
        // せっせとDestoryしてみる
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

    public string GetText()
    {
        return transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
    }
}
