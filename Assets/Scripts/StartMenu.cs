using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public string sceneToLoad = "Cutscene1"; 
    public TMP_Text buttonText;

    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void SecretButton()
    {
        buttonText.text = "Secret Button Pressed!";
        SceneManager.LoadScene("Secret");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartGame();
        }
    }
}
