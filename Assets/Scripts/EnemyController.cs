using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float movementDistance = 3f;

    private bool movingRight = true;
    private Vector3 startPosition;
    public bool isVisible = false;
    public bool isAirborne = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isVisible)
        {
            if (isAirborne)
            {
                AirborneMove();
            }
            else
            {
                GroundMove();
            }
        }
    }
    void OnBecameVisible()
    {
        isVisible = true;
    }

    void OnBecameInvisible()
    {
        isVisible = false;
    }

    void GroundMove()
    {
        Vector3 newPosition = transform.position;

        if (movingRight)
        {
            newPosition.x += speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            newPosition.x -= speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Mathf.Abs(newPosition.x - startPosition.x) >= movementDistance)
        {
            movingRight = !movingRight;
        }

        transform.position = newPosition;
    }

    void AirborneMove()
    {
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            transform.position += directionToPlayer * speed * Time.deltaTime;

            if (directionToPlayer.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
