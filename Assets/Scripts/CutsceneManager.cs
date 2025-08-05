using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 
using UnityEditor;

public class CutsceneManager : MonoBehaviour
{
    public TextMeshProUGUI storyText; 
    public float textDisplaySpeed = 0.05f; 
    public float fadeOutTime = 2f;

    private string[][] cutscenes = new string[][]
    {
        new string[] {
            "Blocky's form flickers into being. Sunlight falls on digital skin with no warmth.",
            "The grass and wildflowers hold a plastic sheen, their scent a simulation.",
            "Beyond the field... pixels stretch to an artificial horizon, the limits of this programmed reality."
        },
        new string[] {
            "Blocky's journey continues. The path twists and turns.",
            "The world is a maze of dead ends and false promises.",
            "Surely this castle contains the answers Blocky seeks."
        },
        new string[] {
            "The door slams shut behind Blocky",
            "Blocky senses a presence. A voice echoes in the void: 'You are not real.'",
            "Blocky must find a way out. But how?"
        },
        new string[] {
            "The way out is blocked. There is no way back.",
            "Blocky is trapped here forever. Or so it seems...",
            "Perhaps the coins hold the key to escape?"
        },
        new string[] {
            "Blocky collected all the coins. The door opens.",
            "Blocky steps through the glowing door, the only exit.",
            "He finds himself in a glitched world of chaos and color."
        },
        new string[] {
            "A portal shimmers in the center of the room.",
            "A faint voice whispers from the other side: 'Prove you are real.'",
            "Blocky hesitates... then jumps in." 
        },
        new string[] {
            "You Win! Congratulations!"
        },
        new string[] {
            "Game Over..."
        }
    };

    private int currentCutsceneIndex = 0; 
    
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Cutscene1") {
            currentCutsceneIndex = 0;
        } else if (currentScene == "Cutscene2") {
            currentCutsceneIndex = 1;
        } else if (currentScene == "Cutscene3") {
            currentCutsceneIndex = 2;
        } else if (currentScene == "BadEnding") {
            currentCutsceneIndex = 3;
        } else if (currentScene == "Cutscene4") {
            currentCutsceneIndex = 4;
        } else if (currentScene == "GoodEnding") {
            currentCutsceneIndex = 5;
        } else if (currentScene == "YouWin") {
            currentCutsceneIndex = 6;
        } else if (currentScene == "GameOver") {
            currentCutsceneIndex = 7;
        }

        StartCoroutine(DisplayCutscene());
    }

    IEnumerator DisplayCutscene()
    {
        foreach (string line in cutscenes[currentCutsceneIndex])
        {
            for (int i = 0; i < line.Length; i++)
            {
                storyText.text += line[i];
                yield return new WaitForSeconds(textDisplaySpeed);
            }
            storyText.text += "\n";
            yield return new WaitForSeconds(1f);
        }

        StartCoroutine(FadeOutText());
        yield return new WaitForSeconds(2f);

        if (currentCutsceneIndex == 6 || currentCutsceneIndex == 7)
        {
            SceneManager.LoadScene("StartMenu");
        }
        else if (currentCutsceneIndex == 5)
        {
            SceneManager.LoadScene("YouWin");
        }
        else if (currentCutsceneIndex == 3)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color textColor = storyText.color;

        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeOutTime);
            storyText.color = textColor;
            yield return null;
        }
    }
}
