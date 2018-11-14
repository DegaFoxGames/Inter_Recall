using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] private string newLevel;

    // Use this for initialization
    public void PlayGame()
    {
        SceneManager.LoadScene(newLevel);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
