using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpSpeed = 1f;
    [SerializeField] LayerMask layerMask;
    Vector3 original;
    Rigidbody2D rigidBody;
    Animator playerAnimator;
    Collider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        //playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<Collider2D>();
        original = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.localScale = original;
            rigidBody.velocity = new Vector2(moveSpeed * 1, rigidBody.velocity.y);
            //playerAnimator.SetBool("IsWalking", true);
        }
        else if(Input.GetKey(KeyCode.A)){
            transform.localScale = new Vector3(-original.x, original.y, original.z);
            rigidBody.velocity = new Vector2(moveSpeed * -1, rigidBody.velocity.y);
            //playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            //playerAnimator.SetBool("IsWalking", false);
        }
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down
        , .1f, layerMask);
        return raycastHit2d.collider;
    }
    private bool IsMoveRightPossible()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right
        , .1f, layerMask);
        return !raycastHit2d.collider;
    }
    private bool IsMoveLeftPossible()
        {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left
        , .1f, layerMask);
        return !raycastHit2d.collider;
    }
}
