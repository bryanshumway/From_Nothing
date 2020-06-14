﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chimera : MonoBehaviour
{
    public static bool bossStop = false;
    public static bool bossDead = false;

    public GameObject player;
    public GameObject attackTrigger;
    public GameObject bossShot;
    public GameObject debris;
    public GameObject[] healthIcons;
    public int healthCurrent;
    public int healthMax;
    public GameObject[] spots;
    public GameObject[] balconies;
    public Transform currSpot;
    public Transform newSpot;
    public Transform prevSpot;
    List<int> balconyChoices = new List<int>();

    private GameObject shotSpawn;
    private bool bossStart = false;
    private bool dead = false;
    private bool isJumping = false;
    private int jumpLevel = 0;
    private float jumpDistance;
    private bool isRotating = false;
    public int attackStatus;
    public float currRot;
    private float oldRot;
    public int rotStatus = 0;
    private bool floorActive = false;
    private bool ceilingActive = false;
    private Rigidbody2D rb;

    private void Start()
    {
        SetHealth();
        player = GameObject.Find("Player");
        shotSpawn = GameObject.Find("shotSpawnBoss");
        spots = GameObject.FindGameObjectsWithTag("BossSpot");
        balconies = GameObject.FindGameObjectsWithTag("Balcony");
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
                        if (attackStatus == 0)
                        {
                            attackStatus = 1;
                        }
                        if (!bossStop)
                        {
                            PlayerCheckAttack();
                        }
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
                        if (attackStatus == 0)
                        {
                            attackStatus = 1;
                        }
                        if (!bossStop)
                        {
                            PlayerCheckAttack();
                        }
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
                    if (attackStatus == 0)
                    {
                        attackStatus = 1;
                    }
                    if (!bossStop)
                    {
                        PlayerCheckAttack();
                    }
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
                    if (attackStatus == 1)
                    {
                        attackStatus = 2;
                    }
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
                    if (attackStatus == 1)
                    {
                        attackStatus = 2;
                    }
                    PlayerCheckAttack();
                    isRotating = false;
                }
            }
        }
        #endregion

    }

    IEnumerator CeilingAttack()
    {
        ceilingActive = true;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 5; i++)
        {
            float xPos = Random.Range(-14, 14.1f);
            Vector3 position = new Vector3(xPos, 22, 0);
            Instantiate(debris, position, debris.transform.rotation);
            yield return new WaitForSeconds(2);
        }
        ceilingActive = false;
    }

    IEnumerator FloorAttack()
    {
        floorActive = true;
        for (int i = 0; i < 6; i ++)
        {
            //int choice = Random.Range(0, 9);
            //if (!balconyChoices.Contains(choice))
            //{
            //    balconyChoices.Add(choice);
            //    balconies[choice].GetComponent<Animation>().Play("BalconyRed");
            //    balconies[choice].GetComponent<BalconyHurt>().enabled = true;
            //}
            bool add = false;
            while (!add)
            {
                int choice = Random.Range(0, 9);
                if (!balconyChoices.Contains(choice))
                {
                    balconyChoices.Add(choice);
                    add = true;
                    balconies[choice].GetComponent<Animation>().Play("BalconyRed");
                    balconies[choice].GetComponent<BalconyHurt>().enabled = true;
                }
                else
                {
                    add = false;
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(10);
        floorActive = false;
        for (int i = 0; i < 9; i++)
        {
            if (balconyChoices.Contains(i))
            {
                balconies[i].GetComponent<Animation>().Play("BalconyNormal");
            }
            balconies[i].GetComponent<BalconyHurt>().enabled = false;
        }
        balconyChoices.Clear();
    }

    IEnumerator ShotAttack()
    {
        Vector3 zero = new Vector3(0, 0, 0);
        bossShot.transform.localScale = zero;
        bossShot.GetComponent<BossShot>().enabled = false;
        bossShot.GetComponent<Collider2D>().enabled = false;
        GameObject shot = (GameObject)Instantiate(bossShot, shotSpawn.transform.position, bossShot.transform.rotation);
        while (shot.transform.localScale.y < 1.5f)
        {
            Vector3 size = new Vector3(.6f, .6f, .6f);
            shot.transform.localScale += size * Time.deltaTime;
            yield return null;
        }
        Destroy(shot);
        ShotSpread();
    }

    void ShotSpread()
    {
        Vector3 size = new Vector3(1.5f, 1.5f, 1.5f);
        bossShot.transform.localScale = size;
        for (int i = 0; i < 12; i++)
        {
            GameObject shot = (GameObject)Instantiate(bossShot, shotSpawn.transform.position, bossShot.transform.rotation);
            shot.GetComponent<BossShot>().enabled = true;
            shot.GetComponent<Collider2D>().enabled = true;
            Vector3 rotation = new Vector3(bossShot.transform.eulerAngles.x,
                bossShot.transform.eulerAngles.y, bossShot.transform.eulerAngles.z + 30);
            bossShot.transform.eulerAngles = rotation;
        }
        Vector3 zero = new Vector3(0, 0, 0);
        bossShot.transform.localScale = zero;
    }

    IEnumerator HowlAttack()
    {
        if (!bossStart)
        {
            GetComponentInChildren<Animator>().SetBool("isHowling", true);
            bossStart = true;
            yield return new WaitForSeconds(3);
            GetComponentInChildren<Animator>().SetBool("isHowling", false);
            attackStatus = 0;
            FindNextSpot();
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isHowling", true);
            //StartCoroutine(CeilingAttack());
            bool chosen = false;
            while (!chosen)
            {
                int choice = Random.Range(1, 4);
                if (choice == 1)
                {
                    StartCoroutine(ShotAttack());
                    chosen = true;
                }
                else if (choice == 2)
                {
                    if (!floorActive)
                    {
                        StartCoroutine(FloorAttack());
                        chosen = true;
                    }
                    else
                    {
                        chosen = false;
                    }
                }
                else if (choice == 3)
                {
                    if (!ceilingActive)
                    {
                        StartCoroutine(CeilingAttack());
                        chosen = true;
                    }
                    else
                    {
                        chosen = false;
                    }
                }
            }
            yield return new WaitForSeconds(3);
            GetComponentInChildren<Animator>().SetBool("isHowling", false);
            attackStatus = 0;
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

    public void RotateCheck()
    {
        //jump location check
        if (newSpot.position.x < currSpot.position.x)
        {
            if (rotStatus == 0)
            {
                Rotate();
            }
        }
        else if (newSpot.position.x > currSpot.position.x)
        {
            if (rotStatus == 1)
            {
                Rotate();
            }
        }
        //current spot check
        if (currSpot.name == "9" || currSpot.name == "6" || currSpot.name == "3")
        {
            if (rotStatus == 0)
            {
                Rotate();
            }
        }
        else if (currSpot.name == "1" || currSpot.name == "4" || currSpot.name == "7")
        {
            if (rotStatus == 1)
            {
                Rotate();
            }
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
            if (!isRotating)
            {
                if (attackStatus == 1)
                {
                    attackStatus = 2;
                }
            }
            if (attackStatus == 2)
            {
                if (healthCurrent == healthMax)
                {
                    StartCoroutine(HowlAttack());
                }
                else
                {
                    int choice = Random.Range(0, 2);
                    if (choice == 0)
                    {
                        StartCoroutine(Heal());
                    }
                    else
                    {
                        StartCoroutine(HowlAttack());
                    }
                }
            }
        }
    }

    IEnumerator Heal()
    {
        GetComponentInChildren<Animator>().SetTrigger("isHealing");
        GetComponent<Animation>().Play();
        attackStatus = 0;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].SetActive(true);
        }
        healthCurrent = healthMax;
        yield return new WaitForSeconds(1);
        FindNextSpot();
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponentInChildren<Animator>().SetBool("isAttacking", false);
        player.GetComponent<PlayerController>().HealthLose();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(HowlAttack());
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
        StopAllCoroutines();
        GetComponentInChildren<Animator>().SetTrigger("isDead");
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        bossDead = true;
        GetComponent<Chimera>().enabled = false;
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
