using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingGenerator : MonoBehaviour
{
    public Text ranking;

    void Start()
    {
        string tmp = "";

        for (int i = 0; i < 5; i++)
        {
            tmp += GameManager.instance.ranking[i] + "\n\n";
        }

        ranking.text = tmp;
    }
}
