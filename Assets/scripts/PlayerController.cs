using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public float jumpForce;
    private bool canJump;
    private int jumpCount; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    
    void Start()
    {
        jumpCount = 0;  
        anim.SetBool("isWalking", true);
    }

    
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (jumpCount < 2) 
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++; 

                anim.SetBool("isWalking", false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            canJump = true;
            jumpCount = 0; 

            anim.SetBool("isWalking", true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver(); 
        }
    }


}
