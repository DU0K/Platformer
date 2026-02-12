using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UiiManagerWinScene : MonoBehaviour
{
    [SerializeField] private Image bronzeImage;
    [SerializeField] private Image silverImage;
    [SerializeField] private Image goldImage;
    private AudioSource audioSource;
    private Collectables collectables;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bronzeImage.color = Color.black;
        silverImage.color = Color.black;
        goldImage.color = Color.black;
        if (Collectables.Bronze)
        {
            bronzeImage.color = Color.white;
        }
        if (Collectables.Silver == true)
        {
            silverImage.color = Color.white;
        }
        if (Collectables.Gold == true)
        {
            goldImage.color = Color.white;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(WaitForSoundSelect());
            
        }
    }

    private IEnumerator WaitForSoundSelect()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene("LevelSelector");
    }
}
