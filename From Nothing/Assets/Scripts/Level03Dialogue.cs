﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level03Dialogue : MonoBehaviour
{

    public GameObject player;
    public GameObject messagePanel;
    public Text messageName;
    public Text messageText;

    private bool dialogueActive = false;
    private int dialogueStatus;

    private void Start()
    {
        StartCoroutine(DialogueStart());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && dialogueActive)
        {
            if (dialogueStatus == 0)
            {
                messageName.text = "Overseer";
                messageText.text = "You better stop trying to leave.";
                dialogueStatus = 1;
            }
            else if (dialogueStatus == 1)
            {
                messageName.text = "You";
                messageText.text = "I'm getting out of here whether you help me or not.";
                dialogueStatus = 2;
            }
            else if (dialogueStatus == 2)
            {
                messageName.text = "Overseer";
                messageText.text = "You'll regret this.";
                dialogueStatus = 3;
            }
            else if (dialogueStatus == 3)
            {
                messageName.text = "";
                messageText.text = "The intercom turns off.";
                dialogueStatus = 4;
            }
            else if (dialogueStatus == 4)
            {
                messageName.text = "You";
                messageText.text = "...";
                dialogueStatus = 5;
            }
            else if (dialogueStatus == 5)
            {
                messageName.text = "";
                messageText.text = "";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                player.GetComponent<PlayerController>().enabled = true;
                dialogueStatus = 6;
            }
        }
    }

    IEnumerator DialogueStart()
    {
        yield return new WaitForSeconds(3);
        dialogueActive = true;
        messageName.text = "";
        messageText.text = "You hear an intercom turn on.";
        messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
    }



}
