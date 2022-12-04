using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ChangeScene : MonoBehaviour
{
    public void ClickBtn()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        
        switch(clickObject.name)
        {
            case "START Btn":
                SceneChange("CharacterSelectScene");
                break;
            case "Character1":
                GameManager.instance.selectPlayer = 0;
                SceneChange("WeaponSelect");
                Debug.Log("1");
                break;
            case "Character2":
                GameManager.instance.selectPlayer = 1;  
                SceneChange("WeaponSelect");
                Debug.Log("2");
                break;
            case "Character3":
                GameManager.instance.selectPlayer = 2;
                SceneChange("WeaponSelect");
                Debug.Log("3");
                break;
            case "toMain":
                SceneChange("StartScene");
                break;
            case "toRanking":
                SceneChange("RankingScene");
                break;
            case "Test Reset": // Ranking Data Reset
                GetComponent<Order>().ResetData();
                break;
        }
    }

    public void ClickWeapon()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        switch(clickObject.name)
        {
            case "wp1":
                GameManager.instance.selectWeapon = 0;
                break;
            case "wp2":
                GameManager.instance.selectWeapon = 1;
                break;
            case "wp3":
                GameManager.instance.selectWeapon = 2;
                break;
            case "wp4":
                GameManager.instance.selectWeapon = 3;
                break;
            case "wp5":
                GameManager.instance.selectWeapon = 4;
                break;
            case "wp6":
                GameManager.instance.selectWeapon = 5;
                break;
            case "wp7":
                GameManager.instance.selectWeapon = 6;
                break;
            case "wp8":
                GameManager.instance.selectWeapon = 7;
                break;
            case "wp9":
                GameManager.instance.selectWeapon = 8;
                break;
            case "wp10":
                GameManager.instance.selectWeapon = 9;
                break;
        }
        
        SceneChange("Map");
    }

    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
