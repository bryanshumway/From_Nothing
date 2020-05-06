using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 3f;
    float jumpSpeed = 1f;
    [SerializeField] LayerMask layerMask;
    Vector3 original;
    Vector3 flipped;
    Rigidbody2D rigidBody;
    Animator playerAnimator;
    BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        original = new Vector3(1,1,1);
        flipped = new Vector3(-1,1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0 && IsMoveRightPossible())
        {
            transform.localScale = original;
            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);
            playerAnimator.SetBool("IsWalking", true);
        }
        else if(Input.GetAxis("Horizontal") < 0 && IsMoveLeftPossible()){
            transform.localScale = flipped;
            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            playerAnimator.SetBool("IsWalking", false);
        }
        if(Input.GetAxis("Jump")!=0 && IsGrounded())
        {
            rigidBody.velocity += Vector2.up*jumpSpeed;
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down
        , .1f, layerMask);
        return raycastHit2d.collider !=null;
    }
    private bool IsMoveRightPossible()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right
        , .1f, layerMask);
        return raycastHit2d.collider ==null;
    }
    private bool IsMoveLeftPossible()
        {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left
        , .1f, layerMask);
        return raycastHit2d.collider ==null;
    }
}
