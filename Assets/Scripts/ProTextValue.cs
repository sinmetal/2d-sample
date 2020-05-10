using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProTextValue : MonoBehaviour
{
    public GameObject scoreObject = null; // Textオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI score_text = scoreObject.GetComponent<TextMeshProUGUI>();
        score_text.text = "Score:";
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI score_text = scoreObject.GetComponent<TextMeshProUGUI>();
        score_text.text = "Score:" + ScoreManager.Increment();
    }
}
