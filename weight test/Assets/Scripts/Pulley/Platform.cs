using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> rbOnPlatform = new List<Rigidbody2D>();
    public float massOnPlatform;
    public bool upper;
    public bool lower;

    private void FixedUpdate()
    {
        massOnPlatform = 1;
        foreach (var rb in rbOnPlatform)
        {
            massOnPlatform += rb.mass;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("upper"))
            upper = false;
        if (other.CompareTag("lower"))
            lower = false;
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        
        if (rb)
        {
            rbOnPlatform.Remove(rb);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("upper"))
            upper = true;
        if (other.CompareTag("lower"))
            lower = true;
        
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        
        if (rb)
        {
            rbOnPlatform.Add(rb);
        }
    }

}
