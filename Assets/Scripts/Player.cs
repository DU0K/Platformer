using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1000f;
    public float jumpForce = 300f;
    public int canJumpForTimes = 2;

    private int canJumpFor;
    private bool isgrounded;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canJumpFor = canJumpForTimes;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        rb.linearVelocityX = moveInputX * speed * Time.deltaTime;

        if (moveInputX <0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInputX > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJumpFor > 0)
        {
            canJumpFor--;
            rb.linearVelocityY = jumpForce * Time.deltaTime;
        }

        if (rb.linearVelocityY > 0)
        {
            animator.Play("Jump");
        }
        else if (moveInputX != 0)
        {
            animator.Play("Walk");
        }
        else if (isgrounded && moveInputX <= 0)
        {
            animator.Play("Idle");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
            canJumpFor = canJumpForTimes;
        }
    }
}
