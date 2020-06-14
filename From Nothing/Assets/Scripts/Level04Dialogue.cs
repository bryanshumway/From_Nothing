using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level04Dialogue : MonoBehaviour
{

    public GameObject player;
    public GameObject messagePanel;
    public GameObject boss;
    public GameObject bossMain;
    public GameObject healthPanel;
    public GameObject bossBattery;
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
                messageText.text = "You made me do this.";
                dialogueStatus = 1;
            }
            else if (dialogueStatus == 1)
            {
                messageName.text = "You";
                messageText.text = "What are you doing??";
                dialogueStatus = 2;
            }
            else if (dialogueStatus == 2)
            {
                boss.GetComponent<Animator>().SetBool("isHowling", true);
                StartCoroutine(Howl());
                dialogueActive = false;
                messageName.text = "";
                messageText.text = "";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);   
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
                messageText.text = "Wait!";
                dialogueStatus = 5;
            }
            else if (dialogueStatus == 5)
            {
                messageName.text = "You";
                messageText.text = "Huh.. What's this..?";
                dialogueStatus = 6;
            }
            else if (dialogueStatus == 6)
            {
                messageName.text = "You";
                messageText.text = "I feel like.. I'm using a battery..?";
                dialogueStatus = 7;
            }
            else if (dialogueStatus == 7)
            {
                messageName.text = "";
                messageText.text = "";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                healthPanel.GetComponent<Animation>().Play("Health_Fade");
                dialogueActive = false;
                StartCoroutine(HealthFade());
            }
            else if (dialogueStatus == 8)
            {
                messageName.text = "";
                messageText.text = "";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                player.GetComponent<PlayerController>().enabled = true;
                bossBattery.SetActive(true);
                bossMain.GetComponent<Chimera>().SetHealth();
                GameObject.Find("platformFloating (2)").GetComponent<UpDown>().enabled = true;
                GameObject.Find("platformFloating (3)").GetComponent<UpDown>().enabled = true;
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

    IEnumerator Howl()
    {
        yield return new WaitForSeconds(2.5f);
        boss.GetComponent<Animator>().SetBool("isHowling", false);
        dialogueStatus = 3;
        dialogueActive = true;
        messageName.text = "Overseer";
        messageText.text = "You're just like the rest. Goodbye.";
        messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
    }

    IEnumerator HealthFade()
    {
        yield return new WaitForSeconds(2);
        dialogueActive = true;
        dialogueStatus = 8;
        messageName.text = "You";
        messageText.text = "I don't know what's going on, but I can't let that thing kill me.";
        messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
    }

}
