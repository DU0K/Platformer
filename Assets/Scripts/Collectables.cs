using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Collectables : MonoBehaviour
{   public static bool Bronze = false;
    public static bool Silver = false;
    public static bool Gold = false;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WaitForSound());
            if (gameObject.name == "Bronze")
            {
                Bronze = true;
            }
            else if (gameObject.name == "Silver")
            {
                Silver = true;
            }
            else if (gameObject.name == "Gold")
            {
                Gold = true;
            }
        }
    }
    private IEnumerator WaitForSound()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        gameObject.SetActive(false);
    }
}
