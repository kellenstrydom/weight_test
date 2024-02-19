using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public float springConstant;
    public float resetDist;

    [SerializeField] private Transform top;
    [SerializeField] private Transform bot;

    [SerializeField] private Rigidbody2D rbTop;

    [SerializeField] private Transform pic;

    private void Update()
    {
        float distance = top.position.y - bot.position.y;
        pic.position = bot.position + Vector3.up * distance/2;
        pic.localScale = new Vector3(1, distance * 0.46f *0.5f, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = resetDist - (top.position.y - bot.position.y);
        
        rbTop.AddForce(springConstant * x * Vector2.up,ForceMode2D.Force);
    }
}
