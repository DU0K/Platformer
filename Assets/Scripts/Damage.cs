using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    public int HP = 100;
    public int SpikeDamage = 25;

    private SpriteRenderer spriterenderer;
    private CapsuleCollider2D capsuleCollider2d;
    private UiManager uiManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = FindFirstObjectByType<UiManager>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        spriterenderer = GetComponent<SpriteRenderer>();

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
            capsuleCollider2d.enabled = false;
            StartCoroutine(RestartGameInSeconds(1f));
        }
    }

    private IEnumerator RestartGameInSeconds(float Time)
    {
        Debug.Log("Player Died");
        yield return new WaitForSeconds(Time);
        SceneManager.LoadScene("Main");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            HP -= SpikeDamage;
            uiManager.UpdateHP(HP);
            StartCoroutine(FlashDamage(0.1f));
            checkForHP();
        }
    }

    private IEnumerator FlashDamage(float Time)
    {
        spriterenderer.color = Color.indianRed;
        yield return new WaitForSeconds(Time);
        spriterenderer.color = Color.white;
    }
}
