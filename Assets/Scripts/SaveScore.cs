using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*

Skirpta se odnosi na spremanje podataka iz igrice
Sprema highscore igraca i izgled bladea koji je korisnik odabrao

*/

public static class SaveScore
{
    // Serolizable omogucuje pretvorbu objekta u memory block da se on moze spremiti u json file
    [Serializable]

    // Klasa Data sadrzi highscore igraca i boju bladea koju je on odabrao
    public class Data
    {
        public int highScore;
        public int index = 0;
    }

    // Direktorij i ime filea gdje ce se on saveati
    public static string directory = "/SaveData";
    public static string fileName = "savefile";

    public static void SaveMyData(Data data)
    {
        // Direktorij gdje se aplikacija nalazi + /SaveData direktorij
        string dir = Application.persistentDataPath + directory;

        // Ako direktorij ne postoji kreiraj ga
        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        // Stvara json od klase Data
        string json = JsonUtility.ToJson(data);

        // Sprema klasu Data u json file
        File.WriteAllText(dir + fileName, json); 
    }

    public static Data LoadMyData()
    {
        // Direktorij gdje se save file nalazi - direktorij gdje se aplikacija nalazi + /SaveData direktorij + ime save filea
        string fullPath = Application.persistentDataPath + directory + fileName;
        // Inicijalizacija novog data objekta
        Data data = new Data();

        // Ako postoji save file
        if(File.Exists(fullPath))
        {
            // Cita string iz filea
            string json = File.ReadAllText(fullPath);

            // Pretvara string u objekt tipa Data
            data = JsonUtility.FromJson<Data>(json);
        }
        else
        {
            Debug.Log("Error saving file");
        }
        
        // Vraca loadani data i save filea
        return data;
    }
}
