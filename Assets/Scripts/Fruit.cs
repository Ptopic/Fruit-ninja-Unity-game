using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Skirpta se odnosi na fruit element

Skripta omogucuje:
    - Presjecanje voca po pola s bladeom

*/

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    // Funkcija se pokrece kad se inicijalizira sam objekt
    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 postion, float force)
    {
        // Pokrece funkciju iz GameManagera koja incrementa score za 1
        FindObjectOfType<GameManager>().IncreaseScore();

        // Mijenja model voca iz cijelog u presjeceno
        whole.SetActive(false);
        sliced.SetActive(true);

        // Disablea fruit collider da igrac ne moze vise presjeci to voce
        fruitCollider.enabled = false;

        // aktivira particle efekt presjecanja voca
        juiceParticleEffect.Play();

        // Racuna kut pod kojim je presjeceno voce ovisno o direkciji presjecanja
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotira presjeceno voce ovisno o izracunatom kutu
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // sliced model se sastoji od 2 prepolovljena kruga koja se dodaju u array
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        // Za svaku polovinu presjecenog kruga
        foreach(Rigidbody slice in slices)
        {
            // Brzina kretanja tijela
            slice.velocity = fruitRigidBody.velocity;

            // Dodaje force da se 2 polovine odmaknu jedne od druge
            slice.AddForceAtPosition(direction * force, postion, ForceMode.Impulse);
        }
    }

    // Ako blade collida s vocem
    private void OnTriggerEnter(Collider other)
    {
        // Usporeduje jeli player aktivirao taj collider
        if(other.CompareTag("Player"))
        {
            // Dohvaca blade
            Blade blade = other.GetComponent<Blade>(); 

            // Presjeci voce
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
