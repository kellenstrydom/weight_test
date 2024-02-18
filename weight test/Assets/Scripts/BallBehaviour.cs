using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class BallBehaviour : MonoBehaviour
{
    public enum ballModes
    {
        small = 0,
        medium = 1,
        large = 2
    }

    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    
    // checks
    [SerializeField] private bool isGrounded;
    private bool isChangingMode = false;
    
    [Header("Modes")] 
    [SerializeField] private BallMode[] modes;
    [SerializeField] private ballModes mode;

    [Header("Movement")] 
    [SerializeField] private float moveForce;
    [SerializeField] private float jumpForce;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        ChangeBallMode(1);
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        Movement();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeBallMode(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeBallMode(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeBallMode(2);
        }
    }

    void ChangeBallMode(int idx)
    {
        if (isChangingMode) return;
        
        Debug.Log($"change to mode {idx}");
        if ((ballModes)idx == mode) return;

        mode = (ballModes)idx;
        
        rb.mass = modes[idx].mass;
        
        // lerp amimation
        StartCoroutine(VisualModeChange(modes[idx].scale,modes[idx].RGB, 0.2f));

        //transform.localScale = modes[idx].scale * new Vector2(1,1);
        
    }

    void Movement()
    {
        float moveDir = Input.GetAxis("Horizontal");

        if (moveDir == 0f && isGrounded)
        {
            rb.drag = 5;
        }
        else
        {
            rb.drag = 0.5f;
        }
        rb.AddForce(Vector2.right * (moveDir * moveForce * modes[(int)mode].forceMultiplier),ForceMode2D.Force);

        if (rb.velocity.x > modes[(int)mode].maxSpeed) 
            rb.velocity = rb.velocity * Vector2.up + modes[(int)mode].maxSpeed *Vector2.right;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * (jumpForce * modes[(int)mode].forceMultiplier) ,ForceMode2D.Impulse);
            }
        }
    }
    

    bool IsGrounded() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = modes[(int)mode].scale * 0.5f + 0.05f;
    
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
    
    IEnumerator VisualModeChange( float upScale, float RGB, float duration)
    {
        isChangingMode = true;
        
        Vector3 initialScale = transform.localScale;
        float initialRGB = sprite.color.r;

        for(float time = 0 ; time < duration * 2 ; time += Time.deltaTime)
        {
            float progress = time / duration;
            transform.localScale = Vector3.Lerp(initialScale, upScale* new Vector3(1,1,1), progress);
            float x = math.lerp(RGB, initialRGB, progress);
            //sprite.color = new Color(x, x, x);
            yield return null;
        }
        transform.localScale = upScale* new Vector3(1,1,1);

        isChangingMode = false;
    }

    
}
