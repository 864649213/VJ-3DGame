﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    public static playerMovement instance;

    private float sideForce = 800;
    private bool finalSprint;
    private float zOfSnowyArea = 216;
    private float zWin = 435;

    public float forwardForce = 2600;
    public float sideVelocity = 3.5f;
    public float maxVelocity = 7;
    public bool dead = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        dead = false;
        rb = gameObject.GetComponent<Rigidbody>();
        finalSprint = false; 
    }


    void Update() {
        if (!dead)
        {
            Vector3 vel = rb.velocity;
            if (!finalSprint && vel.z > maxVelocity)
            {
                vel.z = maxVelocity;
                rb.velocity = vel;
            }

            if (!finalSprint && (Input.GetKey(KeyCode.A) || Input.GetKey("left")))
            {
                //rb.AddForce(-sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                vel.x = -sideVelocity;
                rb.velocity = vel;
                //Debug.Log(rb.velocity);
            }
            else if (!finalSprint && (Input.GetKey(KeyCode.D) || Input.GetKey("right")))
            {
                //rb.AddForce(sideForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                vel.x = sideVelocity;
                rb.velocity = vel;
            }
        }
    }
    void FixedUpdate()
    {
        if (!dead) {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                bool inSnowyArea = transform.position.z >= zOfSnowyArea;
                if (!finalSprint && transform.position.z >= zWin)
                {
                    rb.AddForce(0, 0, forwardForce * 2f);
                    finalSprint = true;
                }
                if (!dead)
                {
                    if (inSnowyArea)
                    {
                        float rF = Random.Range(0.75f, 1.15f);
                        float rS = Random.Range(-1.0f, 1.0f);
                        rb.AddForce(sideForce * Time.deltaTime * rS, 0, forwardForce * Time.deltaTime * rF);

                    }
                }
            }
            
            if (!finalSprint) rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        }
        
    }
}
