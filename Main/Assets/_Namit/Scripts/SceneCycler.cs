using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // This line is crucial

public class SceneCycler : MonoBehaviour
{
    // Array of scene names to cycle through
    public string[] scenes;
    // Track the current scene index
    private static int currentSceneIndex = 0;

    // This method is called before any Start methods
    void Awake()
    {
        // Prevent the object from being destroyed on scene load
        DontDestroyOnLoad(gameObject);

        // If there are other objects of the same type, destroy the ones that aren't the original
        var cyclers = FindObjectsOfType<SceneCycler>();
        foreach (var cycler in cyclers)
        {
            if (cycler != this)
            {
                Destroy(cycler.gameObject);
            }
        }
    }

    public void LoadNextScene()
    {
        // Increment the index and wrap around if necessary
        currentSceneIndex = (currentSceneIndex + 1) % scenes.Length;
        // Load the next scene
        SceneManager.LoadScene(scenes[currentSceneIndex]);
    }

    public void LoadPreviousScene()
    {
        // Decrement the index and wrap around if necessary
        currentSceneIndex = (currentSceneIndex - 1 + scenes.Length) % scenes.Length;
        // Load the previous scene
        SceneManager.LoadScene(scenes[currentSceneIndex]);
    }
}
