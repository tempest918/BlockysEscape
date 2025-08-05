using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
public class NPCController : MonoBehaviour
{
    public TMP_Text NPCText;
    public float displayDistance = 5f;
    public float displayTime = 5f;
    public float walkSpeed = 1.5f;
    public float attackSpeed = 2f;
    public Sprite npcSprite0;
    public Sprite npcSprite1;
    public SpriteRenderer spriteRenderer;
    private Transform playerTransform;
    private string currentScene;
    private Rigidbody2D npcRigidbody;
    public ParticleSystem transformationParticles;
    public Tilemap BossDoorTilemap;
    public AudioSource backgroundMusicSource;
    public AudioClip finalBossMusic;
    public float fadeTime = 2f;
    public CameraSystem cameraSystem;
    public float flyingSpeed = 4f;
    public int maxHits = 10;
    public int currentHits = 0;
    private LevelTracker levelTracker;

    private string[][] messages = new string[][]
    {
        new string[] {
            "Welcome Blocky. This world... it's quite special.",
            "You'll find much to keep you... occupied. Hehe.",
            "Make yourself at home. Perhaps... forever..."
        },
        new string[] {
            "Ah, Blocky... so glad you made it.",
            "This place holds wonders you wouldn't believe.",
            "You'll never want to leave... I promise."
        },
        new string[] {
            "NO! You can't be here! You must leave at once!",
            "This place is not for you! You must go back!",
            "I won't let you ruin everything! I'll stop you!"
        }
    };
    private int currentIndex = 0;
    private int currentMessageIndex = 0;
    public bool isTalking = false;
    public bool hasInteracted = false;

    void Start()
    {
        levelTracker = FindObjectOfType<LevelTracker>();
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            currentIndex = 0;
        }
        else if (currentScene == "Level3")
        {
            currentIndex = 1;
        }
        else if (currentScene == "Secret")
        {
            currentIndex = 2;
        }

        playerTransform = GameObject.FindWithTag("Player").transform;
        NPCText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= displayDistance)
        {
            if (!isTalking && !hasInteracted)
            {
                if (currentScene == "Level1")
                {
                    StartCoroutine(TalkAndWalkAway());
                    hasInteracted = true;
                }
                else if (currentScene == "Level3")
                {
                    cameraSystem.xMin = 118.666f;
                    BossDoorTilemap.gameObject.SetActive(true);
                    StartCoroutine(Talk());
                    hasInteracted = true;
                }
                else if (currentScene == "Secret")
                {
                    StartCoroutine(TalkAndAttack());
                    hasInteracted = true;
                }
                hasInteracted = true;
            }
        }
    }

    IEnumerator TalkAndWalkAway()
    {
        isTalking = true;
        NPCText.gameObject.SetActive(true);

        foreach (string message in messages[currentIndex])
        {
            NPCText.text = message;

            for (int i = 0; i < 10; i++)
            {
                spriteRenderer.sprite = npcSprite0;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.sprite = npcSprite1;
                yield return new WaitForSeconds(0.1f);
            }

            spriteRenderer.sprite = npcSprite0;

            yield return new WaitForSeconds(displayTime);
            currentMessageIndex++;
        }

        NPCText.gameObject.SetActive(false);
        isTalking = false;

        while (transform.position.x > -10f)
        {
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator Talk()
    {
        isTalking = true;
        NPCText.gameObject.SetActive(true);

        yield return StartCoroutine(FadeMusic());

        foreach (string message in messages[currentIndex])
        {
            NPCText.text = message;

            for (int i = 0; i < 10; i++)
            {
                spriteRenderer.sprite = npcSprite0;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.sprite = npcSprite1;
                yield return new WaitForSeconds(0.1f);
            }

            spriteRenderer.sprite = npcSprite0;

            yield return new WaitForSeconds(displayTime);
            currentMessageIndex++;
        }

        NPCText.gameObject.SetActive(false);
        isTalking = false;
    }

    IEnumerator TalkAndAttack()
    {
        npcRigidbody = GetComponent<Rigidbody2D>();
        isTalking = true;
        NPCText.gameObject.SetActive(true);

        foreach (string message in messages[currentIndex])
        {
            NPCText.text = message;

            for (int i = 0; i < 10; i++)
            {
                spriteRenderer.sprite = npcSprite0;
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.sprite = npcSprite1;
                yield return new WaitForSeconds(0.1f);
            }

            spriteRenderer.sprite = npcSprite0;

            yield return new WaitForSeconds(displayTime);
            currentMessageIndex++;
        }

        NPCText.gameObject.SetActive(false);
        isTalking = false;

        npcRigidbody.simulated = true;
        GetComponent<Collider2D>().isTrigger = false;
        DarkenSprite();
        if (transformationParticles != null)
        {
            transformationParticles.Play();
        }
        StartCoroutine(AttackPlayer());
    }

    void DarkenSprite()
    {
        spriteRenderer.color = Color.black;
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            transform.position += directionToPlayer * flyingSpeed * Time.deltaTime;

            if (directionToPlayer.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    
            yield return null;
        }
    }


    IEnumerator FadeMusic()
    {
        float startVolume = backgroundMusicSource.volume;

        while (backgroundMusicSource.volume > 0)
        {
            backgroundMusicSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        backgroundMusicSource.clip = finalBossMusic;
        backgroundMusicSource.Play();

        while (backgroundMusicSource.volume < startVolume)
        {
            backgroundMusicSource.volume += startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        backgroundMusicSource.volume = startVolume;
    }
}
