using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*

Skirpta se odnosi na main menu igrice

*/

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuUI;
    
    // Postotak sanse za spawnanje bombe umjesto voca
    public float bombChanceFloat = 0.1f;


    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        bombChanceFloat = 0.1f;
    }

    // Funkcija loada scenu igrice
    public void PlayGame()
    {
        SceneManager.LoadScene("FruitNinja");
    }

    // Funkcija loada scenu menua za biranje boje bladea
    public void openCustomizationScreen()
    {
        SceneManager.LoadScene("Theme");
    }

    // Funkcija seta difficulty na easy tako sto mijenja postotak sanse za spawnanje bombe umijesto voca
    public void setEasy() 
    {
        bombChanceFloat = 0.1f;
        PlayerPrefs.SetFloat("bombChangeFloat", bombChanceFloat);
    }

    // Funkcija seta difficulty na medium tako sto mijenja postotak sanse za spawnanje bombe umijesto voca
    public void setMedium()
    {
        bombChanceFloat = 0.25f;
        PlayerPrefs.SetFloat("bombChangeFloat", bombChanceFloat);

    }

    // Funkcija seta difficulty na hard tako sto mijenja postotak sanse za spawnanje bombe umijesto voca
    public void setHard()
    {
        bombChanceFloat = 0.4f;
        PlayerPrefs.SetFloat("bombChangeFloat", bombChanceFloat);

    }
}
