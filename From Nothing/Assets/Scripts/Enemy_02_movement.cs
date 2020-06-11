using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_02_movement : MonoBehaviour
{

    public GameObject player;
    public float distanceToMove;
    public float distanceFromPlayer;
    public float speed;
    public float jumpSpeed;
    public float jumpTime;
    public float pushSpeed;
    public float health;

    private float currPos;
    private float newPos;
    private float newPos2;
    private bool startPos = true;
    private bool playerNear = false;
    private bool jumpCall = false;
    private bool dead = false;

    private float step;
    private Vector2 target;

    private void Start()
    {
        currPos = transform.position.x;
        newPos = transform.position.x + distanceToMove;
        newPos2 = transform.position.x - distanceToMove;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceFromPlayer)
        {
            playerNear = true;
            if (!jumpCall)
            {
                StartCoroutine(SlimeJump());
                jumpCall = true;
            }
        }
        else
        {
            StopCoroutine(SlimeJump());
            playerNear = false;
            jumpCall = false;
        }

        if (!playerNear)
        {
            if (startPos)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                if (transform.position.x >= newPos)
                {
                    startPos = false;
                }
            }
            else
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                if (transform.position.x <= newPos2)
                {
                    startPos = true;
                }
            }
        }
        else
        {
            target = new Vector2(player.transform.position.x, transform.position.y);
            step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, step);
        }

        if (health <= 0 && !dead)
        {
            Death();
            dead = true;
        }

    }

    void Death()
    {
        Destroy(gameObject);
    }

    IEnumerator SlimeJump()
    {
        jumpCall = true;
        while (playerNear)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
            yield return new WaitForSeconds(jumpTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<PlayerController>().JumpLose();
            other.transform.GetComponent<PlayerController>().ShotLose();
            if (transform.position.x < other.transform.position.x)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * pushSpeed);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushSpeed);
            }
        }
    }

}
