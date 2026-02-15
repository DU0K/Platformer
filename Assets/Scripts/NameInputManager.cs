using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameInputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField NameInput;
    public static string Name;

    private void Start()
    {
        NameInput.text = PlayerPrefs.GetString("Username");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Name = NameInput.text;
            SceneManager.LoadScene("LevelSelector");
            PlayerPrefs.SetString("Username", Name);
        }
    }
}
