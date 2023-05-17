using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string game;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Loads game scene when clicking play
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(game);
    }
    /// <summary>
    /// Exits application when clicking quit button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
