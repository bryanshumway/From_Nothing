using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static bool canJump = true;
    public static bool footprintActive = false;

    public float moveSpeed = 3f;
    public float jumpSpeed = 1f;
    public GameObject footprint;
    public GameObject footprintSpot;
    public GameObject playerBoots;

    [SerializeField] LayerMask layerMask;
    Vector3 original;
    Rigidbody2D rigidBody;
    Animator playerAnimator;
    Collider2D boxCollider2D;
    public bool canFootprint;
    public bool isJumping;
    public GameObject[] batteryJump;
    private int batteryJumpMaxCharge;
    private int batteryJumpCurrentCharge;

    //FMOD
    //private FMOD.Studio.EventInstance footstepSound; (maybe not needed???)

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<Collider2D>();
        original = transform.localScale;
        batteryJump = GameObject.FindGameObjectsWithTag("BatteryJump");
        batteryJumpMaxCharge = batteryJump.Length;
        for (int i = 0; i < batteryJump.Length; i++)
        {
            batteryJump[i].SetActive(false);
        }
        batteryJumpCurrentCharge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = original;
            rigidBody.velocity = new Vector2(moveSpeed * 1, rigidBody.velocity.y);
            playerAnimator.SetBool("IsWalking", true);
            if (IsGrounded() && canFootprint)
            {
                StartCoroutine("FootPrint");
                canFootprint = false;
            }
        }
        //move left
        else if(Input.GetKey(KeyCode.A)){
            transform.localScale = new Vector3(original.x, original.y, -original.z);
            rigidBody.velocity = new Vector2(moveSpeed * -1, rigidBody.velocity.y);
            playerAnimator.SetBool("IsWalking", true);
            if (IsGrounded() && canFootprint)
            {
                StartCoroutine("FootPrint");
                canFootprint = false;
            }
        }
        //not moving
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            playerAnimator.SetBool("IsWalking", false);
            if (IsGrounded())
            {
                StopCoroutine("FootPrint");
                canFootprint = true;
            }
        }
        //jump
        if(Input.GetButtonDown("Jump") && IsGrounded() && canJump && batteryJumpCurrentCharge > 0)
        {
            GetComponent<Animator>().SetInteger("JumpStatus", 1);
            playerBoots.GetComponent<Animation>().Stop();
            playerBoots.GetComponent<Animation>().Play();
            StartCoroutine(IsJumping());
            FootPrintStep();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            for (int i = 4; i >= 0; i--)
            {
                if (batteryJump[i].activeInHierarchy)
                {
                    batteryJump[i].SetActive(false);
                    batteryJumpCurrentCharge--;
                    break;
                }
            }
        }
        //landed
        if (IsGrounded() && isJumping)
        {
            GetComponent<Animator>().SetInteger("JumpStatus", 3);
            FootPrintStep();
            isJumping = false;
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

    IEnumerator IsJumping()
    {
        yield return new WaitForSeconds(0.1f);
        isJumping = true;
        GetComponent<Animator>().SetInteger("JumpStatus", 2);
    }

    IEnumerator FootPrint()
    {
        //yield return new WaitForSeconds(0.1f);
        if (IsGrounded())
        {
            FootPrintStep();
        }
        yield return new WaitForSeconds(0.2f);
        canFootprint = true;
    }

    public void FootPrintStep()
    {
        RuntimeManager.PlayOneShot("event:/Player/footsteps");
        if (footprintActive)
        {
            Vector3 step = new Vector3(transform.position.x, transform.position.y - .24f, -3);
            Instantiate(footprint, footprintSpot.transform.position, footprintSpot.transform.rotation);
        }
        //isJumping = false;
        //FMOD
        //footstepSound = RuntimeManager.CreateInstance("event:/Player/footsteps");
        //footstepSound.setParameterByName("GroundMaterial", "STRING FOR GROUND MATERIAL TYPE GOES HERE");
        //footstepSound.start();
        //footstepSound.release();
        //RuntimeManager.PlayOneShot("event:/Player/footsteps");
    }

    public void EnableBoots()
    {
        playerBoots.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChargeStation"))
        {
            for (int i = 0; i < batteryJump.Length; i++)
            {
                batteryJump[i].SetActive(true);
            }
            batteryJumpCurrentCharge = batteryJumpMaxCharge;
        }
    }

}
