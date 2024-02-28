using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Skirpta se odnosi na spawner element koji spawna voce i bombe

*/

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;

    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChange = 0.1f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    // 18
    public float minForce = 18f;
    // 22
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake() 
    {
        // Dohvaća box colider koji je spojen ovom skriptom
        spawnArea = GetComponent<Collider>();
        bombChange = PlayerPrefs.GetFloat("bombChangeFloat");
    }

    // Kad se spawner objekt inicijalizira krece spawnati voce
    private void OnEnable() 
    {
        StartCoroutine(Spawn());
    }

    // Zaustavlja spawnanje voca
    private void OnDisable()
    {
        StopAllCoroutines();
    }
 
    private IEnumerator Spawn()
    {
        // Ceka 2 sekunde prije nego krene spawnati voce
        yield return new WaitForSeconds(2f);

        while(enabled) 
        {
            // Dohvaca od UI-a array mogucih voca koji ce spawnati 
            GameObject prefab = fruitPrefabs[Random.Range(0,fruitPrefabs.Length)];

            // Ako je random vrijednost manja od postotka sanse za spawnanje bombe spawna bombu umijesto voca
            if(Random.value < bombChange)
            {
                prefab = bombPrefab;
            }

            // Kalkulira poziciju gdje ce se na spawner planeu spawnat objekt
            Vector3 postion = new Vector3();
            postion.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            postion.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            postion.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Random rotiracijom spawner planea salje objekt lijevo ili desno
            Quaternion rotation = Quaternion.Euler(0f,0f, Random.Range(minAngle,maxAngle));

            // Spawn fruit

            GameObject fruit =  Instantiate(prefab, postion, rotation);
            Destroy(fruit, maxLifetime);

            // Launch fruit in the air

            // Generira random silu kojom ce launchati objekt
            float force = Random.Range(minForce, maxForce);

            // Dodaje silu na objekt
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
