using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamContoller : MonoBehaviour
{
    public Transform ball;
    public Rigidbody2D ballRb;
    public float rubberbandOffset;

    private void Update()
    {
        if (Math.Abs(ball.position.x - transform.position.x) > rubberbandOffset)
        {
            float offset;
            if (ball.position.x > transform.position.x)
            {
                offset = rubberbandOffset;
            }
            else
            {
                offset = -rubberbandOffset;
            }

            transform.position = new Vector3(ball.position.x - offset, transform.position.y, transform.position.z);
        }
    }
}
