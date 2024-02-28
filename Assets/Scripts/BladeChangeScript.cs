using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static SaveScore;
using static PauseMenu;
using System.Security.Cryptography;

/*

Skirpta se odnosi na mijenjanje boja bladea
Koristi se u menu za mijenjanje boja

*/

public class BladeChangeScript : MonoBehaviour
{
    // Dohvaca sve toggle buttone s ekrana
    public ToggleGroup toggleGroup;
    public Toggle[] toggles;

    // Dohvaca save file
    public Data saveData;

    // Inicijalizira index
    public int index;

    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        // Dohvaca sve toggle grupe koje su linkane u editoru
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void ChangeColor()
    {
        // Prolazi kroz sve toggle u menu
        for(int i = 0; i < toggles.Length; i++)
        {
            // Trazi toggle koji je cekiran
            if (toggles[i].isOn)
            {
                index = i;
            }
        }
    }

    public void ConfirmColor()
    {
        // Dodaj boju u save file
        Debug.Log(index);

        // Dodaj u save file index
        saveData.index = index;
        // Dohvaca highscore koji je spramljen u player objektu
        int highScore = PlayerPrefs.GetInt("highscore");

        // Mijenja high score u save fileu
        saveData.highScore = highScore;
        SaveScore.SaveMyData(saveData);

        // Promijeni scenu
        SceneManager.LoadScene("Menu");
    }
}
