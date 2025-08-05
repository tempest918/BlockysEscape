using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelTracker : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText;

    private int currentScore = 0;
    private int coinsCollected = 0;
    public int currentLevel = 1;
    public float levelTime = 300f;
    private float timeRemaining;

    private PlayerController playerController;
    public GameObject npc;
    private bool hasTalkedToNPC = false;
    private bool isNpcTalking = false;
    public Tilemap secretTilemap;
    public GameObject newDoor;
    public GameObject finalDoor;
    private string currentScene;

    public SpriteRenderer fadeSpriteRenderer;
    private Color fadeColor;
    public float badEndingDelay = 5f;

    public bool debugMode = false;

    private bool lifeGrantedAt1000 = false;
    private bool lifeGrantedAt2000 = false;
    private bool lifeGrantedAt3000 = false;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (fadeSpriteRenderer != null)
        {
            fadeColor = fadeSpriteRenderer.color;
        }

        if (currentScene == "Level1")
        {
            PlayerPrefs.DeleteKey("score");
            PlayerPrefs.DeleteKey("coins");
            PlayerPrefs.DeleteKey("level");
            PlayerPrefs.DeleteKey("lives");
            PlayerPrefs.Save();
        }

        lifeGrantedAt1000 = PlayerPrefs.GetInt("lifeGrantedAt1000", 0) == 1;
        lifeGrantedAt2000 = PlayerPrefs.GetInt("lifeGrantedAt2000", 0) == 1;
        lifeGrantedAt3000 = PlayerPrefs.GetInt("lifeGrantedAt3000", 0) == 1;

        timeRemaining = levelTime;
        playerController = FindObjectOfType<PlayerController>();
        LoadPlayerData();
        if (debugMode)
        {
            coinsCollected = 60;
            playerController.lives = 100;
        }
        UpdateUI();
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            HandleTimeOut();
        }

        UpdateTimeDisplay();

        if (currentScene == "Level3")
        {
            hasTalkedToNPC = npc.GetComponent<NPCController>().hasInteracted;
            isNpcTalking = npc.GetComponent<NPCController>().isTalking;

            if (coinsCollected >= 60 && hasTalkedToNPC && !isNpcTalking)
            {
                secretTilemap.gameObject.SetActive(false);
                newDoor.SetActive(true);
            }
            else if (coinsCollected < 60 && hasTalkedToNPC && !isNpcTalking)
            {
                StartCoroutine(InitiateBadEnding());
            }
        }
    }

    public void AddCoin()
    {
        coinsCollected++;
        UpdateUI();
    }

    public void UpdateScore(int amount)
    {
        currentScore += amount;
        UpdateLives();
        UpdateUI();
    }

    public void NextLevel()
    {
        currentLevel++;
        UpdateUI();
        SavePlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SecretLevel()
    {
        currentLevel = 0;
        UpdateUI();
        SavePlayerData();
        SceneManager.LoadScene("Cutscene4");
    }

    public void UpdateLives()
    {
        if (playerController != null)
        {
            UpdateUI();

            if (currentScore >= 3000 && !lifeGrantedAt3000)
            {
                playerController.lives++;
                lifeGrantedAt3000 = true;
                SavePlayerData();
            }
            else if (currentScore >= 2000 && !lifeGrantedAt2000)
            {
                playerController.lives++;
                lifeGrantedAt2000 = true;
                SavePlayerData();
            }
            else if (currentScore >= 1000 && !lifeGrantedAt1000)
            {
                playerController.lives++;
                lifeGrantedAt1000 = true;
                SavePlayerData();
            }
        }
    }

    private void UpdateTimeDisplay()
    {
        timeText.text = "Time: " + FormatTime(timeRemaining);
    }

    private string FormatTime(float time)
    {
        int seconds = Mathf.FloorToInt(time);

        return string.Format("{0:00}", seconds);
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + currentScore;
        coinsText.text = "Coins: " + coinsCollected;
        levelText.text = "Level: " + currentLevel;
        livesText.text = "Lives: " + playerController.lives;
    }

    private void HandleTimeOut()
    {
        playerController.RespawnPlayer();
    }

    public void GameOver()
    {
        StartCoroutine(FadeToBlackAndLoadScene("GameOver"));
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("score", currentScore);
        PlayerPrefs.SetInt("coins", coinsCollected);
        PlayerPrefs.SetInt("level", currentLevel);
        PlayerPrefs.SetInt("lives", playerController.lives);
        PlayerPrefs.SetInt("lifeGrantedAt1000", lifeGrantedAt1000 ? 1 : 0);
        PlayerPrefs.SetInt("lifeGrantedAt2000", lifeGrantedAt2000 ? 1 : 0);
        PlayerPrefs.SetInt("lifeGrantedAt3000", lifeGrantedAt3000 ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        currentScore = PlayerPrefs.GetInt("score", 0);
        coinsCollected = PlayerPrefs.GetInt("coins", 0);
        currentLevel = PlayerPrefs.GetInt("level", 1);
        playerController.lives = PlayerPrefs.GetInt("lives", 3);
    }
    IEnumerator InitiateBadEnding()
    {
        yield return new WaitForSeconds(badEndingDelay);
        StartCoroutine(FadeToBlackAndLoadScene("BadEnding"));
    }
    IEnumerator FadeToBlackAndLoadScene(string sceneName)
    {
        float fadeSpeed = .1f;

        while (fadeColor.a < 1f)
        {
            fadeColor.a += fadeSpeed * Time.deltaTime;
            fadeSpriteRenderer.color = fadeColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    public void NPCDefeated()
    {
        Destroy(npc);
        playerController.canMove = false;
        GameObject player = GameObject.Find("Player");

        finalDoor.gameObject.SetActive(true);

        StartCoroutine(FadeToBlackAndLoadScene("GoodEnding"));
    }
}
