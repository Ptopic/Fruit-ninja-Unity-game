using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SaveScore;

/*

Skirpta se odnosi na pause menu igrice

*/

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public static GameManager manager;

    public Data saveData;

    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Funkcija se poziva svaki frame
    void Update()
    {
        // Ako je pritisnut escape key na tipkovnici funkcija provijerava vrijednost isPaused variable
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Ako je true nastavi igricu
            if(isPaused)
            {
                Resume();
            }
            // Ako je false pauziraj igricu
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Sakriva pause menu
        pauseMenuUI.SetActive(false);

        // Nastavlja vrijeme u igrici
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        // Prikazuje pause menu
        pauseMenuUI.SetActive(true);

        // Zaustavlja vrijeme u igrici
        Time.timeScale = 0f;
        isPaused = true;
    }


    public void LoadMenu()
    {
        // Sprema high score igrice
        saveData.highScore = manager.highScore;

        // postavlja high score u player container
        PlayerPrefs.SetInt("highscore", manager.highScore);
        // Store high score in json file
        SaveScore.SaveMyData(saveData);

        Time.timeScale = 1f;

        // Otvara main menu scenu
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        // Sprema high score igrice
        saveData.highScore = manager.highScore;

        // postavlja high score u player container
        PlayerPrefs.SetInt("highscore", manager.highScore);
        // Sprema high score u json file
        SaveScore.SaveMyData(saveData);

        // Zatvara igricu
        Application.Quit();
    }
}
