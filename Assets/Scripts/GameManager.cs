using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SaveScore;

/*

Skirpta se odnosi na game manager koji upravlja samom igrom
Kontrolira trenutni score, najveci score, broj zivota,

*/

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;
    public Text livesText;
    public Image fadeImage;

    private Blade blade;
    private Spawner spawner;
    private HealthBar healthBar;

    public Data saveData;

    private int score;
    public int highScore;
    public int index;

    // Broj zivota
    int lives = 3;

    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        healthBar = FindObjectOfType<HealthBar>();

        // Get high score from json file
        saveData = SaveScore.LoadMyData();
        highScore = saveData.highScore;
        Debug.Log(highScore);

        // Get color data from json file and change blade color to that color
        index = saveData.index;
        Debug.Log(index);



    }

    // Funkcija se aktivira kad se skripta enablea
    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        // Postavlja broj zivota na objekt healtbar s funkcijom iz healtbar skripte
        lives = 3;
        healthBar.SetHealt(lives);

        livesText.text = "Lives: " + lives;
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        highScoreText.text = "Best: " + highScore.ToString();

        ClearScene();
    }

    // Brise svo voce i bombe sa scene
    public void ClearScene()
    {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    // Povecava score
    public void IncreaseScore()
    {
        // Ako je score manji od high scora povecaj ga i promijeni text scora na igrici
        if(score < highScore)
        {
            score++;
            scoreText.text = score.ToString();
        }
        // Ako je score veci od high scora povecaj score i high score i promijeni oba na igrici 
        else
        {
            score++;
            highScore++;
            scoreText.text = score.ToString();
            highScoreText.text = "Best: " + highScore.ToString();
        }


    }

    public void Explode()
    {
        // Umanji zivote za 1 i promijeni ih na health baru
        lives--;
        healthBar.SetHealt(lives);

        // Ako su zivoti manji ili jedanki 0 ugasi igricu
        if (lives <= 0)
        {
            // Disablea blade i spawner
            blade.enabled = false;
            spawner.enabled = false;

            // Pokreni funkciju za zavrsetak igre
            StartCoroutine(ExplodeSequence());
        }
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        // Animacija fade in za prikaz zavrsetka igre
        while(elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);

            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        // Pokreni novu igru
        NewGame();

        elapsed = 0;

        // Animacija za fade out igre
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);

            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
    }

}
