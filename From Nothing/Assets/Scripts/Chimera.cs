using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chimera : MonoBehaviour
{

    public GameObject player;
    public GameObject attackTrigger;
    public GameObject[] healthIcons;
    public int healthCurrent;
    public int healthMax;
    public GameObject[] spots;
    public Transform currSpot;
    public Transform newSpot;
    public Transform prevSpot;

    private bool bossStart = false;
    private bool dead = false;
    private bool isJumping = false;
    private int jumpLevel = 0;
    private float jumpDistance;
    private bool isRotating = false;
    public float currRot;
    private float oldRot;
    public int rotStatus = 0;
    private Rigidbody2D rb;

    private void Start()
    {
        SetHealth();
        player = GameObject.Find("Player");
        spots = GameObject.FindGameObjectsWithTag("BossSpot");
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(JumpToSpot());
        StartCoroutine(HowlAttack());
    }

    private void Update()
    {
        //if dead
        #region
        if (healthCurrent == 0)
        {
            dead = true;
        }
        if (dead)
        {
            healthCurrent = -1;
            Death();
        }
        #endregion

        //jumping code
        #region
        if (isJumping)
        {
            //if balcony is on same level
            if (jumpLevel == 0)
            {
                //if its right
                if (newSpot.position.x > currSpot.position.x)
                {
                    if (Vector3.Distance(transform.position, newSpot.position) > jumpDistance / 2)
                    {
                        Vector2 jumpSpeed = new Vector2(7, 1);
                        rb.velocity = jumpSpeed;
                    }
                    else
                    {
                        Vector2 jumpSpeed = new Vector2(7, -1);
                        rb.velocity = jumpSpeed;
                    }
                    if (Vector3.Distance(transform.position, newSpot.position) < .5f)
                    {
                        Vector2 stop = new Vector2(0, 0);
                        rb.velocity = stop;
                        currSpot = newSpot;
                        PlayerCheckAttack();
                        isJumping = false;
                    }
                }
                //if its left
                else
                {
                    if (Vector3.Distance(transform.position, newSpot.position) > jumpDistance / 2)
                    {
                        Vector2 jumpSpeed = new Vector2(-7, 1);
                        rb.velocity = jumpSpeed;
                    }
                    else
                    {
                        Vector2 jumpSpeed = new Vector2(-7, -1);
                        rb.velocity = jumpSpeed;
                    }
                    if (Vector3.Distance(transform.position, newSpot.position) < .5f)
                    {
                        Vector2 stop = new Vector2(0, 0);
                        rb.velocity = stop;
                        currSpot = newSpot;
                        PlayerCheckAttack();
                        isJumping = false;
                    }
                }
            }
            //if balcony is higher or lower
            else if (jumpLevel == -1 || jumpLevel == 1)
            {
                float step = 8 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, newSpot.position, step);
                if (Vector3.Distance(transform.position, newSpot.position) < .5f)
                {
                    Vector2 stop = new Vector2(0, 0);
                    rb.velocity = stop;
                    currSpot = newSpot;
                    PlayerCheckAttack();
                    isJumping = false;
                }
            }
        }
        #endregion

        //rotate code
        #region
        if (isRotating)
        {
            currRot = transform.localEulerAngles.y;
            transform.Rotate(Vector2.up * (100 * Time.deltaTime));
            if (rotStatus == 0)
            {
                if (currRot <= 5)
                {
                    GetComponentInChildren<Animator>().SetBool("isWalking", false);
                    rotStatus = 1;
                    PlayerCheckAttack();
                    isRotating = false;
                }
            }
            else if (rotStatus == 1)
            {
                if (currRot >= 180)
                {
                    GetComponentInChildren<Animator>().SetBool("isWalking", false);
                    rotStatus = 0;
                    PlayerCheckAttack();
                    isRotating = false;
                }
            }
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.E))
        {
            FindNextSpot();
        }

    }

    IEnumerator HowlAttack()
    {
        if (!bossStart)
        {
            GetComponentInChildren<Animator>().SetBool("isHowling", true);
            bossStart = true;
            yield return new WaitForSeconds(3);
            GetComponentInChildren<Animator>().SetBool("isHowling", false);
            FindNextSpot();
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isHowling", true);
            yield return new WaitForSeconds(3);
            GetComponentInChildren<Animator>().SetBool("isHowling", false);
            FindNextSpot();
        }
    }

    public void FindNextSpot()
    {
        int choice = Random.Range(0, 9);
        newSpot = spots[choice].transform;
        if (Vector3.Distance(currSpot.transform.position, newSpot.transform.position) <= 13 && 
            currSpot.transform.position.x != newSpot.transform.position.x && currSpot != newSpot && newSpot != prevSpot)
        {
            StartCoroutine(JumpToSpot());
        }
        else
        {
            FindNextSpot();
        }
    }

    public void Rotate()
    {
        oldRot = transform.localEulerAngles.y;
        GetComponentInChildren<Animator>().SetBool("isWalking", true);
        isRotating = true;
    }

    public bool RotateCheck()
    {
        //jump location check
        if (newSpot.position.x < currSpot.position.x)
        {
            if (rotStatus == 0)
            {
                Rotate();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (newSpot.position.x > currSpot.position.x)
        {
            if (rotStatus == 1)
            {
                Rotate();
                return true;
            }
            else
            {
                return false;
            }
        }
        //current spot check
        if (currSpot.name == "9" || currSpot.name == "6" || currSpot.name == "3")
        {
            if (rotStatus == 0)
            {
                Rotate();
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (currSpot.name == "1" || currSpot.name == "4" || currSpot.name == "7")
        {
            if (rotStatus == 1)
            {
                Rotate();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void PlayerCheckAttack()
    {
        if (BossAttackTrigger.playerInRange == true)
        {
            GetComponentInChildren<Animator>().SetBool("isAttacking", true);
            StartCoroutine(Attack());
        }
        else
        {
            RotateCheck();
            if (!RotateCheck())
            {
                StartCoroutine(HowlAttack());
            }
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Animator>().SetBool("isAttacking", false);
        player.GetComponent<PlayerController>().HealthLose();
        yield return new WaitForSeconds(1);
        RotateCheck();
    }

    IEnumerator JumpToSpot()
    {
        prevSpot = currSpot;
        yield return new WaitForSeconds(0.1f);
        //rotation check
        RotateCheck();
        //set jump level
        yield return new WaitForSeconds(0.5f);
        if (newSpot.position.y < currSpot.position.y)
        {
            jumpLevel = -1;
        }
        else if (newSpot.position.y > currSpot.position.y)
        {
            jumpLevel = 1;
        }
        else if (newSpot.position.y == currSpot.position.y)
        {
            jumpLevel = 0;
        }
        //call jump update
        if (jumpLevel == 0)
        {
            jumpDistance = Vector3.Distance(transform.position, newSpot.position);
            GetComponentInChildren<Animator>().SetTrigger("isJumping");
            yield return new WaitForSeconds(0.5f);
            isJumping = true;
        }
        else if (jumpLevel == -1 || jumpLevel == 1)
        {
            GetComponentInChildren<Animator>().SetTrigger("isJumping");
            yield return new WaitForSeconds(0.5f);
            isJumping = true;
        }
    }

    public void SetHealth()
    {
        healthIcons = GameObject.FindGameObjectsWithTag("BossHealth");
        healthMax = healthIcons.Length;
        healthCurrent = healthMax;
    }

    public void HealthLost()
    {
        for (int i = 9; i >= 0; i--)
        {
            if (healthIcons[i].activeInHierarchy)
            {
                healthIcons[i].SetActive(false);
                healthCurrent--;
                break;
            }
        }
    }

    public void Death()
    {
        dead = false;
        GetComponentInChildren<Animator>().SetTrigger("isDead");
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ChargeStation"))
        {
            for (int i = 0; i < healthIcons.Length; i++)
            {
                healthIcons[i].SetActive(true);
            }
            healthCurrent = healthMax;
        }
    }

}
