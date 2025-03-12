using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerakan karakter
    
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    private EncounterManager encounterManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        encounterManager = FindObjectOfType<EncounterManager>();
    }

    private void Update()
    {
        if (!GameManager.isPaused)
        {
            // Mendapatkan input dari tombol
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Memastikan gerakan hanya dalam satu arah pada satu waktu
            Vector2 movement = Vector2.zero;
            if (horizontalInput != 0)
            {
                movement = new Vector2(horizontalInput, 0) * moveSpeed * Time.deltaTime;
            }
            else if (verticalInput != 0)
            {
                movement = new Vector2(0, verticalInput) * moveSpeed * Time.deltaTime;
            }

            if (horizontalInput > 0f)
            {
                spriteRenderer.flipX = false;
            }

            else if (horizontalInput < 0f)
            {
                spriteRenderer.flipX = true;
            }

            if (!Mathf.Approximately(horizontalInput, 0f))
            {
                animator.Play("nira-run");
            }else if (!Mathf.Approximately(verticalInput, 0f))
            {
                animator.Play("nira-run");
            }
            else
            {
                animator.Play("nira-idle");
            }




            // Menggerakkan karakter
            rb.MovePosition(rb.position + movement);
        }
    }
}
