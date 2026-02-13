using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    public static bool finishSceneIsOpen = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && finishSceneIsOpen == false)
        {
            finishSceneIsOpen = true;
            audioSource.Play();
            Player.SetActive(false);
            SceneManager.LoadScene("WinScene", LoadSceneMode.Additive);
            SpeedrunTimer.TimerLoop = false;
        }
    }
}
