using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{   public static bool Bronze = false;
    public static bool Silver = false;
    public static bool Gold = false;

    private UiManager uiManager;

    private AudioSource audioSource;
    private void Start()
    {
        uiManager = FindFirstObjectByType<UiManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collided with " + gameObject.name);
            if (gameObject.name == "Bronze")
            {
                audioSource.Play();
                Bronze = true;
                gameObject.GetComponent<Renderer>().enabled = false;
                StartCoroutine(WaitForSound());
            }
            else if (gameObject.name == "Silver")
            {
                audioSource.Play();
                Silver = true;
                gameObject.GetComponent<Renderer>().enabled = false;
                StartCoroutine(WaitForSound());
            }
            else if (gameObject.name == "Gold")
            {
                audioSource.Play();
                Gold = true;
                gameObject.GetComponent<Renderer>().enabled = false;
                StartCoroutine(WaitForSound());
            }
            uiManager.CheckForCollectables();
        }
    }

    IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.enabled = false;
    }
}
