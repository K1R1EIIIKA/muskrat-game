using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int Health;
    public static int Points;
    public static GameObject PlayerObject;
    
    public static bool HasNotGround;
    public static bool IsWaterContact;

    private Rigidbody2D _rb;

    private void Awake()
    {
        PlayerObject = gameObject;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
            Health--;

        if (other.gameObject.CompareTag("Enemy"))
            Health--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            IsWaterContact = true;
            PlayerMovement.IsDash = false;
        }

        if (other.CompareTag("Wall"))
        {
            PlayerMovement.IsDash = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            HasNotGround = true;
            StopFalling();
            return;
        }

        if (other.CompareTag("Wall"))
        {
            HasNotGround = true;
            _rb.gravityScale = 1.0f;
            PlayerMovement.CanMove = false;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            HasNotGround = false;
            StopFalling();
            return;
        }
        
        if (other.CompareTag("Wall"))
        {
            HasNotGround = false;
            StopFalling();
        }
    }

    private void StopFalling()
    {
        _rb.gravityScale = 0f;
        _rb.angularVelocity = 0f;
        _rb.velocity = Vector2.zero;
        PlayerMovement.CanMove = true;
    }
}