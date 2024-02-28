using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// Za spremanje save data
using static SaveScore;

/*

Ova skripta se koristi za Blade objekt koji se koristi za presjecanje voca

Skirpta se odnosi na Blade element

*/

public class Blade : MonoBehaviour
{
    // Dohvacanje objekata iz igrice
    private Camera mainCamera;
    private Collider bladeCollider;
    public TrailRenderer[] bladeTrails;
    private TrailRenderer bladeTrail;
    private bool slicing;

    // Spremanje u save file
    public Data saveData;
    public int index;

    // 3D vektor direkcije
    public Vector3 direction { get; private set; }

    public float sliceForce = 5f;
    public float minSliceVelocity = 0.2f;

    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        // Postavlja main camaeru
        mainCamera = Camera.main;

        // Dohvaca blade collider i blade trail
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();

        // Loada index iz json save filea - iz color change menua
        saveData = SaveScore.LoadMyData();
        index = saveData.index;

        // Mijenja boju bladea
        bladeTrail.colorGradient = bladeTrails[index].colorGradient;

    }

    // OnDisable i OnEnable funkcije omogucavaju da tjekom pokretanja blade ne krene automatski rezati nego samo ako je pritisnuta tipka misa
    private void OnDisable()
    {
        StopSlicing();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    // Update funkcija se pokrece svaki frame
    private void Update()
    {
        // Ako je pritisnut lijevi klik misa kreni slicat
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        // Ako je podignut lijevi klik misa prestani slicat
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        // Nastavi slicit ako je slicing variable true
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        // Updateaj poziciju collidera po poziciji misa
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        // Postavlja poziciju na novo stvorenu poziciju misa
        transform.position = newPosition;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        // Zaustavlja slicanje tako sto disablea blade collider i blade trail
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        // Updateaj poziciju collidera po poziciji misa
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        // Mijenja globalni vektor direkcije
        direction = newPosition - transform.position;

        // Govori nam koliko se brzo krece -> koliko se pomaklo u zadnjem frameu
        float velocity = direction.magnitude / Time.deltaTime;

        // Ako je brzina veca od minimalne brzine potrebne za slice voca collider se aktivira
        // Sprjecava akritivarnje collidera bez pomicanja misa
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = newPosition;

    }
}
