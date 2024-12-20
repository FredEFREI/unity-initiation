using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsController : MonoBehaviour
{
    // Method to switch scenes
    public void SwitchScene(string sceneName)
    {
        // Check if the scene is valid and exists
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " cannot be loaded. Make sure it exists in the build settings.");
        }
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("quitting");
        #if UNITY_EDITOR
                // If running in the editor, stop play mode
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // If running as a built game, quit the application
                Application.Quit();
        #endif
    }
}
