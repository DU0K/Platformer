using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{   
    public static bool Bronze = false;
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
            if (gameObject.name == "Bronze" && Bronze == false)
            {
                audioSource.Play();
                Bronze = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else if (gameObject.name == "Silver" && Silver == false)
            {
                audioSource.Play();
                Silver = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else if (gameObject.name == "Gold" && Gold == false)
            {
                audioSource.Play();
                Gold = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            uiManager.CheckForCollectables();
        }
    }
}
