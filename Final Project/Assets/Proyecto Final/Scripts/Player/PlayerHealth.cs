﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
	[Header("Life")]
	public float startingHp;
	//public int startingV;
	
	[Header("Life InGame")]
	public float currentHp;
	//public int currentV;

	[Header("Damage")]
	//private int vDamage;

	[Header("Times")] 
	//private float toxicTime;
	private float timeCount;

    [Header("Bars")]
    public Image healthSlider;
    //public Image virusSlider;

    //public Slider virusSlider;

    // Activamos el gameobject GameOver
    public GameObject gameOver;

    // Sonido muerte

    [Header("Animation")]
	Animator anim;

	// Sonidos

	PlayerBehaviour playerControl;

	bool isDead;
	bool damaged;
	//bool intoxicate = false;


	void Awake()
	{
		anim = GetComponent<Animator>();
		playerControl = GetComponent<PlayerBehaviour>();
		currentHp = startingHp;
        healthSlider.fillAmount = currentHp/startingHp;
		//currentV = startingV;
        //virusSlider.value = startingV;
        //virusSlider.fillAmount = currentV / startingV;
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
        TakeDamage();

        /*if(intoxicate)
		{
			timeCount += Time.deltaTime;
			toxicTime += Time.deltaTime;
			Virus();
		}*/
    }

	void TakeDamage ()
	{
        healthSlider.fillAmount = currentHp / startingHp;
        if (currentHp <= 0 && !isDead)
        {

            Death();
        }
    }

		/*public void TakeVirus (int vAmount)
	{
		
		intoxicate = true;

        vDamage = vAmount;
		currentV += vAmount;

        //virusSlider.fillAmount = currentV / 100;
		//virusSlider.value = currentV;

		// Sonido asignado del jugador

		if(currentHp <= 0 && !isDead)
		{
			return;
		}
	}*

	/*void Virus()
	{
		if (currentHp > 0)
		{
			
			if (timeCount >= 3f)
			{
				Debug.Log("Me sube el Virus");
				currentV += 20;

                virusSlider.fillAmount = currentV / 100;
				//virusSlider.value = currentV;
				
				timeCount = 0;
			}

			toxicTime += Time.deltaTime;
			if (toxicTime >= 7)
			{
					intoxicate = false;
					toxicTime = 0;
			}
		}
	}*/

    public void Death()
	{
		isDead = true;

		// Animacion de muerte;

		playerControl.enabled = false;
        gameOver.SetActive(true);
        Cursor.visible = true;
    }
}
