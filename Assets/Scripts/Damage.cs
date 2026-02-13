using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    public int HP = 100;
    public float DamageJumpForce = 3f;
    public int SpikeDamage = 40;
    public int EnemyDamage = 25;
    public float DamageCooldownSeconds = 1f;

    private int MinHP = 0;
    private bool OnCooldown;

    [SerializeField] private AudioSource AudioSourceDamage;
    [SerializeField] private AudioSource AudioSourceDeath;

    private SpriteRenderer spriterenderer;
    private BoxCollider2D Collider;
    private UiManager uiManager;
    private Movement movement;
    private Rigidbody2D rb;
    private Scene thisScene;
    private Enemy enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindFirstObjectByType<UiManager>();
        Collider = GetComponent<BoxCollider2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
        thisScene = SceneManager.GetActiveScene();
        enemy = FindFirstObjectByType<Enemy>();

        uiManager.UpdateHP(HP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void checkForHP()
    {
        if (HP <= 0)
        {
            Collectables.Bronze = false;
            Collectables.Silver = false;
            Collectables.Gold = false;
            HP = 0;
            Collider.enabled = false;
            StartCoroutine(WaitForSoundDeath());
            StartCoroutine(RestartGameInSeconds(2f));
        }
    }

    private IEnumerator RestartGameInSeconds(float Time)
    {
        rb.linearVelocityY = DamageJumpForce * movement.speedAndForceMultiplier;
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene(thisScene.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && OnCooldown == false)
        {
            rb.linearVelocityY = DamageJumpForce * movement.speedAndForceMultiplier;
            HP = Mathf.Max(HP - EnemyDamage, MinHP);
            StartCoroutine(WaitForSoundDamage());
            uiManager.UpdateHP(HP);
            StartCoroutine(FlashDamage(0.1f));
            checkForHP();
            StartCoroutine(DamageWithCooldown(DamageCooldownSeconds));

        }
        else if (collision.gameObject.CompareTag("Spike") && OnCooldown == false)
        {
            rb.linearVelocityY = DamageJumpForce * movement.speedAndForceMultiplier;
            HP = Mathf.Max(HP - SpikeDamage, MinHP);
            StartCoroutine(WaitForSoundDamage());
            uiManager.UpdateHP(HP);
            StartCoroutine(FlashDamage(0.1f));
            checkForHP();
            StartCoroutine(DamageWithCooldown(DamageCooldownSeconds));

        }
    }

    private IEnumerator FlashDamage(float Time)
    {
        spriterenderer.color = Color.indianRed;
        yield return new WaitForSeconds(Time);
        spriterenderer.color = Color.white;
    }

    private IEnumerator DamageWithCooldown(float Time)
    {
        OnCooldown = true;
        yield return new WaitForSeconds(Time);
        OnCooldown = false;
    }
    private IEnumerator WaitForSoundDamage()
    {
        AudioSourceDamage.Play();
        yield return new WaitForSeconds(AudioSourceDamage.clip.length);
    }
    private IEnumerator WaitForSoundDeath()
    {
        AudioSourceDeath.Play();
        yield return new WaitForSeconds(AudioSourceDeath.clip.length);
    }
}
