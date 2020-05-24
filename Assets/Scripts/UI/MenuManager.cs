using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ShowMenuPanel(GameObject obj)
    {
        obj.SetActive(true);
        SetGamePause(true);
    }

    public void HideMenuPanel(GameObject obj)
    {
        obj.SetActive(false);
        SetGamePause(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    void SetGamePause(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }    
}
