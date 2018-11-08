﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Animator anim;

    private CharacterController controller;

    public Transform lookAt;

    public float forwardSpeed;              // Velocidad de avance
    private float diagonalForwardSpeed;     // Velocidad de avance en cada eje cuando avanza diagonalmente
    private float backSpeed;                // Velocidad de retroceso
    private float diagonalBackSpeed;        // Velocidad de retroceso en cada eje cuando retrocede diagonalmente
    public float jumpSpeed;                 // Velocidad de salto
    public float gravity;                   // Gravedad

    private Vector3 moveDirection;          // Vector de la direccion

    private float inputV;                   // Tecla de avance recto
    private float inputH;                   // Tecla de avance lateral
    private float jumpInput;                // Tecla de salto

	// Use this for initialization
	void Start ()
    {
        this.controller = GetComponent<CharacterController>();

        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed / 2;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * backSpeed / 2);

        this.moveDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GetInput ())
        {
            Rotate ();
        }
        Move ();
	}

    // Escucha todas las teclas que controlan al jugador
    private bool GetInput()
    {
        this.inputV = Input.GetAxis("Vertical");
        this.inputH = Input.GetAxis("Horizontal");
        this.jumpInput = Input.GetAxis("Jump");

        if(this.inputV != 0 || this.inputH != 0 || this.jumpInput != 0)
        {
            return true;
        }
        return false;
    }

    // Mueve al personaje si se usan las teclas de control
    private void Move()
    {
        if (Grounded()) // Si el jugador esta en el suelo se puede mover y saltar
        {
            this.gravity = 0;

            if (this.inputV == 0 && this.inputH == 0) // QUIETO

            {
                this.moveDirection.Set(0, 0, 0);
                anim.SetTrigger("Idle");
                
            }
            else if (this.inputV > 0 && this.inputH == 0) // AVANZA
            {
                this.moveDirection.Set(0, 0, this.inputV * this.forwardSpeed);
                anim.SetTrigger("Run");

            }

            else if (this.inputV < 0 && this.inputH == 0) // RETROCEDE
            {
                this.moveDirection.Set(0, 0, this.inputV * this.backSpeed);
            }
            
            else if (this.inputH > 0 && this.inputV == 0) // DERECHA
            {
                this.moveDirection.Set(this.inputH * this.forwardSpeed, 0, 0);
            }

            else if (this.inputH < 0 && this.inputV == 0) // IZQUIERDA

            {
                this.moveDirection.Set(this.inputH * this.backSpeed, 0, 0);
            }

            else if (this.inputV > 0 && this.inputH > 0) // AVANZA-DERECHA
            {
                this.moveDirection.Set(this.inputH * this.diagonalForwardSpeed, 0, this.inputV * this.diagonalForwardSpeed);
            }

            else if (this.inputV > 0 && this.inputH < 0) // AVANZA-IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.diagonalForwardSpeed, 0, this.inputV * this.diagonalForwardSpeed);
            }

            else if (this.inputV < 0 && this.inputH > 0) // RETROCEDE-DERECHA
            {
                this.moveDirection.Set(this.inputH * this.diagonalBackSpeed, 0, this.inputV * this.diagonalBackSpeed);
            }

            else if (this.inputV < 0 && this.inputH < 0) // RETROCEDE-IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.diagonalBackSpeed, 0, this.inputV * this.diagonalBackSpeed);
            }

            this.moveDirection = transform.TransformDirection(this.moveDirection); // Transformamos la direccion de loca a world space

            if (this.jumpInput > 0) // SALTA
            {
                this.moveDirection.y = this.jumpSpeed;
            }
        }

        else // EN EL ARIE
        {
            this.gravity = 25.0f;

            if ((this.controller.collisionFlags & CollisionFlags.Above) != 0) //Cuando choque la cabeza contra algo que rapidamente cambie a zero el salto y comience a caer
            {
                this.moveDirection.y = 0;
            }
        }

        this.moveDirection.y -= this.gravity * Time.deltaTime; // Le aplica gravedad constante a la direccion Y

        this.controller.Move(this.moveDirection * Time.deltaTime); // SE MUEVE
    }

    private void Rotate ()
    {
        transform.rotation = Quaternion.Euler(0, this.lookAt.eulerAngles.y, 0);
    }
    private bool Grounded ()
    {
        return Physics.Raycast(transform.position + this.controller.center, Vector3.down, this.controller.bounds.extents.y + 0.001f);
    }
}
