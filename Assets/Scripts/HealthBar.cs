using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Skirpta se odnosi na healthbar element

Skripta omogucuje:
    - dohvacanje health bar elementa
    - postavljanje vrijednosti health bar elementa

*/

public class HealthBar : MonoBehaviour
{
    // Dodani elementi u Unityu
    public Slider slider;
    public Image fill;

    public void SetHealt(int healt)
    {
        // Postavlja vrijednost health bara na odredenu vrijednost
        slider.value = healt;
    }
}
