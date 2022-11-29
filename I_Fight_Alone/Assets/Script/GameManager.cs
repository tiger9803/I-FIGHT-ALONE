using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
            {
                if(instance != this)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public int accScore = 0;     // 누적 score
    public int selectWeapon = 0; // 선택한 무기
    public int selectPlayer = 0; // 선택한 캐릭터

    // GameOver
    public string time = "Time 00:00";
}
