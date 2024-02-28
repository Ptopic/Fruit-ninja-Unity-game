using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Skirpta se odnosi na bomb element

Skripta omogucuje:
    - Pronalazenje i razlikovanje bombi od voca
    - Aktivira explode event ako je bomba presjecena
*/

public class Bomb : MonoBehaviour
{
    private ParticleSystem explodeParticleEffect;
    private Bomb bomb;


    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        // Dohvaca particle efekt i samu bombu
        explodeParticleEffect = GetComponentInChildren<ParticleSystem>();
        bomb = FindObjectOfType<Bomb>();
    }

    // Ovaj event se aktivira ako se blade objekt tjst noz s kojim sijecemo voce presjece (collida) s bombom
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Dohvaca blade komponentu
            Blade blade = other.GetComponent<Blade>();

            // Event kad je bomba sliceana
            explodeParticleEffect.Play();

            // Pokrece Explode event iz GameManager skripte koji oduzima jedan zivot
            FindObjectOfType<GameManager>().Explode();

            // Poziva event na cekanje da izbrise bombu kako je korisnik nebi ponovno vise puta presjekao
            StartCoroutine(wait());
            
        }
    }

    IEnumerator wait()
    {
        // Funkcija ceka .7 sec da izbrise bomb game object
        yield return new WaitForSeconds(.7f);

        Destroy(bomb.gameObject);
    }
}
