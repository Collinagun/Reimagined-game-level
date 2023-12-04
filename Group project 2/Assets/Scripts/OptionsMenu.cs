using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource buttonClickSound; // Add this line

    public void OptionMenu()
    {
        buttonClickSound.Play(); // Add this line
        // Add code to open the options menu
    }

    public void BackButton()
    {
        buttonClickSound.Play(); // Add this line
        // Add code to go back to the previous menu
    }
}
