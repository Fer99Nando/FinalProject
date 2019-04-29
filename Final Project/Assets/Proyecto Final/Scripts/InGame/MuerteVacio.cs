﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteVacio : MonoBehaviour
{
    PlayerHealth player;
    GameObject playerObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    /*public void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            player.DieAcabado();
        }
    }*/

     public void OnCollisionEnter (Collision col)
 {
     if (col.gameObject.tag == "Player")
     {
         Debug.Log("TOCOESPASIO");
         player.DieAcabado();
     }
 }
}
