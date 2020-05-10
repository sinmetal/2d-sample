using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static float score = 0;       // スコア

    void Start()
    {
    }

    void Update()
    {
        score++;
    }

    public static float Get()
    {
        return score;
    }

    public static float Increment()
    {
        return score++;
    }
}