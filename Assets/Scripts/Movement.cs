
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 3f;
    public int canJumpForTimes = 2;
    public int speedAndForceMultiplier = 100;

    [SerializeField] private AudioSource AudioSourceJump;

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
        rb.linearVelocityX = moveInputX * speed * speedAndForceMultiplier;

        if (moveInputX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInputX > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJumpFor > 0)
        {
            StartCoroutine(WaitForSoundJump());
            canJumpFor--;
            rb.linearVelocityY = jumpForce * speedAndForceMultiplier;
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

    private IEnumerator WaitForSoundJump()
    {
        AudioSourceJump.Play();
        yield return new WaitForSeconds(AudioSourceJump.clip.length);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isgrounded = true;
            canJumpFor = canJumpForTimes;
        }
    }
}
