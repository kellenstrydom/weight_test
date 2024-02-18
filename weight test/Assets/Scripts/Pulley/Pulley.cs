using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Pulley : MonoBehaviour
{
    public Platform platform1;
    public Platform platform2;
    public Rigidbody2D rb1;
    public Rigidbody2D rb2;

    [SerializeField] private Vector2 netAcceleration;
    [SerializeField] private float drag;
 
    private void FixedUpdate()
    {
        if (Math.Abs(platform1.massOnPlatform - platform2.massOnPlatform) < 0.1f)
        {
            if (Math.Abs(rb1.velocity.y) < 0.6)
                rb1.velocity = Vector2.zero;
            else
                rb1.velocity -= drag * Time.fixedDeltaTime * Math.Abs(rb1.velocity.y) / rb1.velocity.y * Vector2.up;

            rb2.velocity = -rb1.velocity;
        }
        else
        {
            netAcceleration = (Physics2D.gravity * ((platform1.massOnPlatform - platform2.massOnPlatform))) / platform1.massOnPlatform;
            if (((platform1.upper || platform2.lower) && (netAcceleration.y > 0f || rb1.velocity.y > 0f)) ||
                ((platform1.lower || platform2.upper) && (netAcceleration.y < 0f || rb1.velocity.y < 0f)))
            {
                rb1.velocity = rb2.velocity = Vector2.zero;
            }
            else
            {
                rb1.velocity += netAcceleration * Time.fixedDeltaTime;
                rb2.velocity = -rb1.velocity;
            }        
        }
        


    }
}
