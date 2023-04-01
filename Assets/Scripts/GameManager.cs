using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameEnded;

    public void WinLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Win!");
            gameEnded = true;
            SceneManager.LoadScene("Win");
        }
    }

    public void LoseLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Lose!");
            gameEnded = true;
            SceneManager.LoadScene("Lose");
        }
    }
}
