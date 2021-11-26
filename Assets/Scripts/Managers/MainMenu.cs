using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Scene scene;

    public void PlayGame()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")//load next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else                        //reload current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
