using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_01_Interact_Manager : MonoBehaviour
{

    private GameObject player;
    private GameObject messagePanel;
    public Text messageText;

    private bool active01;
    public static int status01 = 0;

    private void Awake()
    {
        player = GameObject.Find("PlayerModel");
        messagePanel = GameObject.Find("MessagePanel");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //01's status
            if (active01)
            {
                //normal message exit
                if (status01 == 0 || status01 == 2)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    active01 = false;
                    GetComponent<Collider2D>().enabled = true;
                    GetComponent<Level_01_Interact_Manager>().enabled = false;
                }
                //crystal inserted
                else if (status01 == 1)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    active01 = false;
                    status01 = 2;
                    GetComponent<Collider2D>().enabled = true;
                    GetComponent<Level_01_Interact_Manager>().enabled = false;
                }
            }
        }
    }

    public void StatusCheck()
    {
        GetComponent<Collider2D>().enabled = false;
        //message displayed and action taken depending what item was interacted with and the what the current status is at
        //crystal launcher
        if (name == "crystalReceptacle")
        {
            //no crystal picked up
            if (status01 == 0)
            {
                messageText.text = "Nothing happens.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                active01 = true;
            }
            //if crystal was picked up
            else if (status01 == 1)
            {
                Destroy(GameObject.Find("crystal"));
                GameObject.Find("crystalInserted").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("floor01door").GetComponent<Animation>().Play("SlidingUpDoorOpen");
                messageText.text = "You've inserted the Crystal.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                active01 = true;
            }
            //after door opened
            else if (status01 == 2)
            {
                messageText.text = "A door opened.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                active01 = true;
            }
        }
        //crystal
        else if (name == "crystal")
        {
            //if picked up change 01's status
            if (status01 == 0)
            {
                status01 = 1;
            }
        }
    }

}
