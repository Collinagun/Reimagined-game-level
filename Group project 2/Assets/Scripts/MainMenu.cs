using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this line

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonClickSound;
    public AudioSource backgroundMusic; // Add this line
    public Slider volumeSlider; // Add this line

    private void Start()
    {
        // Set the slider's value to the current music volume
        volumeSlider.value = backgroundMusic.volume;
    }

    private void Update()
    {
        // Update the music volume to the slider's value
        backgroundMusic.volume = volumeSlider.value;
    }

    public void PlayGame()
    {
        buttonClickSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        buttonClickSound.Play();
        Debug.Log("QUIT");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
