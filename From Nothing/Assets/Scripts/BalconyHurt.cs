using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalconyHurt : MonoBehaviour
{

    public GameObject player;

    private bool hurtActive = false;
    private bool balconyActive = false;
    Collider2D boxCollider2D;
    [SerializeField] LayerMask layerMask;

    IEnumerator Start()
    {
        player = GameObject.Find("Player");
        yield return new WaitForSeconds(2);
        balconyActive = true;
        if (IsGrounded())
        {
            hurtActive = true;
            StartCoroutine("PlayerHurt");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && balconyActive)
        {
            hurtActive = true;
            StartCoroutine("PlayerHurt");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player") && balconyActive)
        {
            hurtActive = false;
            StopCoroutine("PlayerHurt");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.up
        , .1f, layerMask);
        return raycastHit2d.collider;
    }

    IEnumerator PlayerHurt()
    {
        while (hurtActive)
        {
            player.GetComponent<PlayerController>().HealthLose();
            yield return new WaitForSeconds(1);
        }
    }

}
