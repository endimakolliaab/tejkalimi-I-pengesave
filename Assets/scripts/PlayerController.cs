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
    private int jumpCount; // Track the number of jumps

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 0;  // Start with no jumps available
        anim.SetBool("isWalking", true);
    }

    // Update is called once per frame
    void Update()
    {
        // Only allow jumping if the player hasn't exceeded the max jump count (2)
        if (Input.GetMouseButtonDown(0))
        {
            if (jumpCount < 2) // Allow double jump
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                jumpCount++; // Increment jump count

                anim.SetBool("isWalking", false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Reset jump count when player touches the ground
            canJump = true;
            jumpCount = 0; // Allow jumping again on the ground

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
            FindObjectOfType<GameManager>().GameOver(); // Restart the game if collided with obstacle
        }
    }


}
