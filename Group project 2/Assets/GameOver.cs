using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
   public void RestartButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
