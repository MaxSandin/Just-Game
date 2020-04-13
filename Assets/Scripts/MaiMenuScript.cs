using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaiMenuScript : MonoBehaviour
{
//Load next scene in stack
    public void PlayNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
//Load last scene
    public void ContinueGame()
    {
        // TooDoo>> Ad continue script after saving method has been added.
        // It is play new game functionality now!!
        PlayNewGame();
    }
 //Exit from app
    public void ExitGame()
    {
        Application.Quit();
    }
}
