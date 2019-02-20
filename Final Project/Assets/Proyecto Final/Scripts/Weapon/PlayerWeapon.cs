﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour 
{
    private int bonusStats;

    public GameObject gameOver;

    PlayerHealth playerHealth;

    public Image virusSlider;
    public Image healthSlider;
    public ParticleSystem virusEffect;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        DamageVirus();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
            Debug.Log("enemy atravesado");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			enemy.TakeDamage (bonusStats);
        }

        if (other.tag == "Boss")
        {
            Debug.Log("BOSS atravesado");
            BossHealth boss = other.GetComponent<BossHealth>();
            boss.currentHp -= bonusStats;
        }
    }

    public void DamageVirus()
    {
        Debug.Log("Sube Daño");

        if (virusSlider.fillAmount == 0)
        {
            Debug.Log("Base Daño");
            bonusStats = 50;
        }

        else if (virusSlider.fillAmount > 0 && virusSlider.fillAmount < 0.25f)
        {
            Debug.Log("10 Daño");
            bonusStats = 100;
        }

        if (virusSlider.fillAmount >= 0.25f && virusSlider.fillAmount < 0.5f)
        {
            Debug.Log("15 Daño");
            bonusStats = 150;
        }
        
        if (virusSlider.fillAmount >= 0.5f && virusSlider.fillAmount < 0.75f)
        {
            Debug.Log("20 Daño");
            bonusStats = 200;
        }

        if (virusSlider.fillAmount >= 0.75f && virusSlider.fillAmount < 1f)
        {
            Debug.Log("25 Daño");
            bonusStats = 250;
        }

        if (virusSlider.fillAmount >= 1)
        {
            Debug.Log("50 Daño");
            bonusStats = 500;
            virusEffect.Play();

            playerHealth.MaximusPower();
        } else virusEffect.Stop();
    }

    public void Death()
    {
    // Animacion de muerte;
        gameOver.SetActive(true);
        Cursor.visible = true;
        Destroy(gameObject);
        //playerControl.enabled = false;
        
        
    }
}
