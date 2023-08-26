using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpot;

    private int randomSpot;
    public float lenght;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpot.Length);
    }

    void Update()
    {
        lenght = moveSpot.Length;
        if (moveSpot.Length != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot[randomSpot].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpot[randomSpot].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpot.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}