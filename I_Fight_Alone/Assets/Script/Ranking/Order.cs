using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private string[] bestName = new string[5];
    private int[] bestScore = new int[5];
    private int[] bestRecord = new int[5];

    public void ScoreSet(string currentName, int currentScore, string currentRecord)
    {
        // 현재에 저장하고 시작
        PlayerPrefs.SetString("CurrentPlayerName", currentName);
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);
        PlayerPrefs.SetString("CurrentPlayerRecord", currentRecord);

        string tmpName = "";
        int tmpScore = 0;
        int tmpRecord = 0;
        string[] time = new string[2];
        int sec, min = 0;
        int total = 0;

        for(int i=0; i<5; i++)
        {
            // 저장된 최고점수와 이름을 가져오기
            bestName[i] = PlayerPrefs.GetString(i + "BestName");
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            bestRecord[i] = PlayerPrefs.GetInt(i + "BestRecord");

            // 현재 점수가 랭킹에 오를 수 있을 때
            while(bestScore[i] < currentScore)
            {
                time = currentRecord.Split(":");
                min = int.Parse(time[0]);
                sec = int.Parse(time[1]);
                total = sec + min * 60;

                // 자리 바꾸기
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                tmpRecord = bestRecord[i];
                bestScore[i] = currentScore;
                bestName[i] = currentName;
                bestRecord[i] = total;

                // 랭킹에 저장
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);
                PlayerPrefs.SetInt(i +"BestRecord", total);

                // 다음 반복을 위한 준비
                currentScore = tmpScore;
                currentName = tmpName;
                total = tmpRecord;
            }

            
            
            if(bestScore[i] == currentScore)
            {
                while(bestRecord[i] < total)
                {
                    time = currentRecord.Split(":");
                    min = int.Parse(time[0]);
                    sec = int.Parse(time[1]);
                    total = sec + min * 60;

                    // 자리 바꾸기
                    tmpScore = bestScore[i];
                    tmpName = bestName[i];
                    tmpRecord = bestRecord[i];
                    bestScore[i] = currentScore;
                    bestName[i] = currentName;
                    bestRecord[i] = total;

                    // 랭킹에 저장
                    PlayerPrefs.SetInt(i + "BestScore", currentScore);
                    PlayerPrefs.SetString(i.ToString() + "BestName", currentName);
                    PlayerPrefs.SetInt(i + "BestRecord", total);

                    // 다음 반복을 위한 준비
                    currentScore = tmpScore;
                    currentName = tmpName;
                    total = tmpRecord;
                }
            }
        }

        // 랭킹에 맞춰 점수와 이름 저장
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetInt(i + "BestRecord", bestRecord[i]);
        }

        for (int i = 0; i < 5; i++)
        {
            string a = PlayerPrefs.GetString(i.ToString() + "BestName", bestName[i]);
            int b = PlayerPrefs.GetInt(i + "BestScore", bestScore[i]);
            int c = PlayerPrefs.GetInt(i + "BestRecord", bestRecord[i]);

            int _sec, _min;
            _sec = c % 60;
            _min = c / 60;

            GameManager.instance.ranking[i] 
                    = "RANK " + (i+1).ToString() + " : " + b.ToString() 
                            + " / " + _min.ToString() + ":" + _sec.ToString();
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
