using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;

    public int lives = 3;
    public GameObject respawnPoint;
    public Camera mainCamera;
    public AudioClip jumpSound;
    public AudioClip enemyDefeatSound;
    public AudioClip coinSound;
    public Sprite defaultSprite;
    public Sprite defaultSprite2;
    public Sprite happySprite;
    public Sprite sadSprite;


    private Rigidbody2D rb;
    private SpriteRenderer playerRenderer;
    private LevelTracker levelTracker;

    private bool isInvincible = false;
    private float cameraBottomEdge;
    private NPCController npcController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
        levelTracker = FindObjectOfType<LevelTracker>();
        cameraBottomEdge = mainCamera.ViewportToWorldPoint(Vector3.zero).y;
        npcController = FindObjectOfType<NPCController>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (canMove)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rb.velocity += Vector2.up * jumpForce;
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                targetSpeed = speed * 1.5f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                targetSpeed = speed;

                speed = Mathf.Lerp(speed, targetSpeed, 0.1f);
            }

            if (transform.position.y < cameraBottomEdge)
            {
                RespawnPlayer();
            }

            if (moveInput < 0)
            {
                playerRenderer.flipX = true;
            }
            else if (moveInput > 0)
            {
                playerRenderer.flipX = false;
            }

            if (moveInput != 0)
            {
                AnimatePlayer();
            }
        }
    }

    bool IsGrounded()
    {
        float extraHeight = 0.1f;
        Collider2D myCollider = GetComponent<Collider2D>();

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, myCollider.bounds.extents.y + extraHeight, LayerMask.GetMask("Ground"));

        if (raycastHit.collider != null)
        {
            return true;
        }

        return Mathf.Abs(rb.velocity.y) <= 0.05f;
    }

    public void RespawnPlayer()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
            levelTracker.GameOver();
            return;
        }

        lives--;
        levelTracker.UpdateLives();
        transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.transform.rotation;
        StartCoroutine(BlinkEffect());
        StartCoroutine(ShowSpriteTemporarily(sadSprite));
    }

    IEnumerator BlinkEffect()
    {
        isInvincible = true;
        for (int i = 0; i < 3; i++)
        {
            playerRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        isInvincible = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            Bounds enemyBounds = col.collider.bounds;
            float enemyTop = enemyBounds.center.y + enemyBounds.extents.y;

            if (transform.position.y >= enemyTop)
            {
                DefeatEnemy(col.gameObject);
            }
            else
            {
                RespawnPlayer();
            }
        }
        else if (col.gameObject.CompareTag("NPC") && !isInvincible)
        {
            float playerMidpoint = transform.position.y;

            float npcMidpoint = col.transform.position.y;

            if (playerMidpoint > npcMidpoint)
            {
                npcController.currentHits++;
                rb.velocity += Vector2.up * 10f;
                if (npcController.currentHits >= npcController.maxHits)
                {
                    levelTracker.NPCDefeated();
                }
            }
            else
            {
                RespawnPlayer();
            }
        }
    }

    void DefeatEnemy(GameObject enemy)
    {
        levelTracker.UpdateScore(100);
        Destroy(enemy);
        rb.velocity += Vector2.up * 10f;
        GetComponent<AudioSource>().PlayOneShot(enemyDefeatSound);
        StartCoroutine(ShowSpriteTemporarily(happySprite));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            levelTracker.UpdateScore(10);
            levelTracker.AddCoin();
            GetComponent<AudioSource>().PlayOneShot(coinSound);
            StartCoroutine(ShowSpriteTemporarily(happySprite));
        }
        else if (other.CompareTag("Goal"))
        {
            levelTracker.NextLevel();
        }
        else if (other.CompareTag("SecretGoal"))
        {
            levelTracker.SecretLevel();
        }
        else if (other.CompareTag("Lava") && !isInvincible)
        {
            RespawnPlayer();
        }
    }

    IEnumerator ShowSpriteTemporarily(Sprite tempSprite)
    {
        Sprite originalSprite = playerRenderer.sprite;
        playerRenderer.sprite = tempSprite;
        yield return new WaitForSeconds(1f);
        playerRenderer.sprite = originalSprite;
    }

    void AnimatePlayer()
    {
        if (Time.frameCount % 10 == 0)
        {
            if (playerRenderer.sprite == defaultSprite)
            {
                playerRenderer.sprite = defaultSprite2;
            }
            else
            {
                playerRenderer.sprite = defaultSprite;
            }
        }
    }

}