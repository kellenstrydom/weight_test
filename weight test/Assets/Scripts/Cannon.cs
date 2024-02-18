using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private bool isLoaded = false;
    [SerializeField] private Transform ball;
    [SerializeField] private Rigidbody2D rbBall;

    public float fireForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLoaded) return;

        ball.position = transform.position;
        rbBall.velocity = Vector2.zero;
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.right = (mousePos - (Vector2)transform.position).normalized;
        
        Fire();
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            isLoaded = false;
            
            rbBall.AddForce(transform.right*fireForce,ForceMode2D.Impulse);
            
            Debug.Log(transform.right*fireForce);

            //transform.right = Vector3.right;
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        
        if (isLoaded) return;
            
            
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLoaded = true;
            ball = col.transform;
            rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.velocity = Vector2.zero;
        }
    }
}
