using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int Health;
    public static int Points;
    public static GameObject PlayerObject;

    private void Awake()
    {
        PlayerObject = gameObject;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
            Health--;
        
        if (other.gameObject.CompareTag("Enemy"))
            Health--;
    }
}
