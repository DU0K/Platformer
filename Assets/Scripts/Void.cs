using UnityEngine;
using UnityEngine.SceneManagement;

public class Void : MonoBehaviour
{
    private Scene thisScene;
    private void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collectables.Bronze = false;
            Collectables.Silver = false;
            Collectables.Gold = false;
            SceneManager.LoadScene(thisScene.name);
        }
    }
}
