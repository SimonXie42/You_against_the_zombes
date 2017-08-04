using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    
    public void LoadAbout()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSurvivalNotes()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
}

