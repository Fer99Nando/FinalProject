﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{ 
    public float startingHp;

    public float currentHp;

    //public Slider healthSlider;
    public Image healthSlider;

    public GameObject victory;

    int phase;

    // Sonido muerte

    //Animator anim;

    // Sonidos

    BossPrueba bossBehaviour;

    bool isDead;
    bool segundaFase;

    void Awake()
    {
        segundaFase = true;

        bossBehaviour = GetComponent<BossPrueba>();
        //anim = GetComponent<Animator>();
        currentHp = startingHp;
        healthSlider.fillAmount = currentHp / startingHp;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    private void Update()
    {
        switch (phase)
        {
            case 0:
                PhaseOne();
                break;
            case 1:
                PhaseTwo();
                break;
            default:
                break;
        }

        TakeDamage();


    }

    public void TakeDamage()
    {
        healthSlider.fillAmount = currentHp / startingHp;
    }

    void PhaseOne()
    {
        if (currentHp <= 0 && !isDead && segundaFase == true)
        {
            bossBehaviour.ChangePhase();
            currentHp += startingHp;
            healthSlider.fillAmount = currentHp / startingHp;

            segundaFase = false;

            phase = 1;

        }
    }

    void PhaseTwo()
    {
        if (currentHp <= 0 && !isDead && segundaFase == false)
        {
            isDead = true;
            if (isDead)
            {
                Death();
            }
        }
    }

    void Death()
    {
        // Animacion de muerte;
        Destroy(gameObject);
        victory.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }
}
