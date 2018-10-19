﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour 
{
	float curHp;
	float curVirus;

	public float maxHp = 100f;

	public Image healthBar;
	public Image virusBar;

    private float timeCount;
	private float toxicTime;

	private bool intoxicate;
	
	//Animator myAnim;

	// Use this for initialization
	void Start () 
	{
		//myAnim = GetComponent<Animator> ();
		
		curHp = maxHp;

		healthBar.fillAmount = curHp / maxHp;

		virusBar.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCount += Time.deltaTime;
		if(timeCount >= 3 && intoxicate)
		{
			curVirus += 2f;

			virusBar.fillAmount = curVirus / 500;

			timeCount = 0;
		}

		if (intoxicate)
		{
			toxicTime += Time.deltaTime;
			if(toxicTime >= 7)
			{
				intoxicate = false;
				toxicTime = 0;
			}
		}

	}
	void LateUpdate ()
	{

	}
	private void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Enemy"))
		{
			intoxicate = true;
			
			//curHp -= col.GetComponent<EnemyManager>().damageValue;
			curHp -= 10f;

			healthBar.fillAmount = curHp / maxHp;

			curVirus += 10f;

			virusBar.fillAmount = curVirus / 500;

			if (curVirus >= 50)
			{
				//SONIDO 50
			}
			if (curVirus >= 100)
			{
				//SONIDO 100
			}

			if (curHp <= 0)
			{
				//myAnim.SetBool("dead", true);
			}
			toxicTime = 0;
		}
	}
}
