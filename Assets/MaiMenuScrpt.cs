using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaiMenuScrpt : MonoBehaviour
{
    public void playNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void continueGame()
    {
        // TooDoo>> Ad continue script after saving method has been added.
        // It is play new game functionality now!!
        playNewGame();
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
