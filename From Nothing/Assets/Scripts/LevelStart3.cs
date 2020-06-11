using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart3 : MonoBehaviour
{
    public static bool levelEntered = false;

    public GameObject player;
    public GameObject fade;
    public GameObject dialogue;
    public GameObject platform;
    public GameObject keycard;
    public GameObject crystal;
    public GameObject crystalInserted;
    public GameObject boots;

    private void Awake()
    {
        fade.GetComponent<Animation>().Play("FadeIn");
        PlayerController.canJump = true;
        PlayerController.doubleJumpActive = true;
        PlayerController.footprintActive = false;
        StartCoroutine(DoorOpen());
        GameObject.Find("doorEnter").GetComponent<Level_01_Interact_Manager>().exitEnablePlayer = false;
        if (levelEntered)
        {
            GameObject.Find("doorEnter").GetComponent<Level_01_Interact_Manager>().exitEnablePlayer = true;
            dialogue.SetActive(false);
            if (Level_01_Interact_Manager.statusCrystal2 == 2)
            {
                platform.GetComponent<Animation>().Play();
                crystal.SetActive(false);
                crystalInserted.GetComponent<MeshRenderer>().enabled = true;
            }
            if (Level_01_Interact_Manager.statusKeycard2 == 1)
            {
                keycard.SetActive(false);
            }
            if (Level_01_Interact_Manager.statusBoots2 == 1)
            {
                PlayerController.doubleJumpActive = true;
                player.GetComponent<PlayerController>().canJumpDouble = true;
                boots.SetActive(false);
            }
        }
    }

    private void Start()
    {
        levelEntered = true;
    }

    IEnumerator DoorOpen()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("doorEnter").GetComponent<Level_01_Interact_Manager>().DoorEntered();
        GameObject.Find("doorEnter").GetComponent<Level_01_Interact_Manager>().DoorOpen();
    }
}
