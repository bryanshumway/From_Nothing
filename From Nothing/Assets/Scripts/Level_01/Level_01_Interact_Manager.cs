using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_01_Interact_Manager : MonoBehaviour
{

    private GameObject player;
    private GameObject messagePanel;
    private GameObject closestDoor;
    private GameObject closestEnterSpot;
    private GameObject closestExitSpot;
    private Text messageText;
    public Vector3 doorEnterSpotNext;
    private bool doorEntered;
    //enter door status
    private bool doorEnter;
    public static int doorEnterStatus = 0;
    //crystal launcher status
    private bool active01;
    public static int status01 = 0;
    //door 01 button status
    private bool active02;
    public static int status02 = 0;
    //boss door pass status
    private bool active03;
    public static int status03 = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        messagePanel = GameObject.Find("MessagePanel");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        closestDoor = GameObject.FindGameObjectWithTag("Door");
        closestEnterSpot = GameObject.FindGameObjectWithTag("DoorEnterSpot");
        closestExitSpot = GameObject.FindGameObjectWithTag("DoorExitSpot");
    }

    //door enter/exit logic
    //message box logic
    private void Update()
    {
        //entering door
        if (doorEnter)
        {
            player.GetComponent<PlayerController>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //crystal insert status
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
            //door 01 status
            if (active02)
            {
                //normal message exit
                if (status02 == 0)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    active02 = false;
                    GetComponent<Collider2D>().enabled = true;
                    GetComponent<Level_01_Interact_Manager>().enabled = false;
                }
            }
            //boss door pass status
            if (active03)
            {
                //normal message exit
                if (status03 == 0)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    active03 = false;
                    GetComponent<Collider2D>().enabled = true;
                    GetComponent<Level_01_Interact_Manager>().enabled = false;
                }
            }
        }
    }

    //when player presses F on object, this happens first
    public void StatusCheck()
    {
        GetComponent<Collider2D>().enabled = false;
        //crystal launcher
        if (name == "crystalReceptacle")
        {
            //no crystal picked up
            if (status01 == 0)
            {
                messageText.text = "Some sort of pedestal. You notice something can be inserted on top.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                active01 = true;
            }
            //if player has crystal
            else if (status01 == 1)
            {
                Destroy(GameObject.Find("crystal"));
                GameObject.Find("crystalInserted").GetComponent<MeshRenderer>().enabled = true;
                messageText.text = "You've inserted the Crystal. You hear something powered on.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                active01 = true;
                status02 = 1;
            }
            //after door opened
            else if (status01 == 2)
            {
                messageText.text = "The crystal turned on some sort of power.";
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
        //pass scan
        else if (name == "passScan")
        {
            //if player doesn't have pass
            if (status03 == 0)
            {
                messageText.text = "It's a pass scanner.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                active03 = true;
            }
        }
        //door buttons
        else if (CompareTag("DoorButton"))
        {
            //door button 01
            if (name == "door01Button")
            {
                //if crystal hasn't been inserted
                if (status02 == 0)
                {
                    messageText.text = "You press the button. There seems to be no power present.";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                    active02 = true;
                }
                else if (status02 == 1)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    GetComponent<Collider2D>().enabled = false;
                    DoorOpen();
                }
            }
            //any non-specific door button
            else
            {
                player.GetComponent<PlayerController>().enabled = true;
                GetComponent<Collider2D>().enabled = false;
                DoorOpen();
            }
        }
        //door enter
        else if (CompareTag("Door"))
        {
            DoorEnter();
        }
    }
    void DoorOpen()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Door");
        foreach (GameObject obj in objectsWithTag)
        {
            if (Vector3.Distance(player.transform.position, obj.transform.position) <= Vector3.Distance(player.transform.position, closestDoor.transform.position))
            {
                closestDoor = obj;
            }
        }
        if (doorEntered)
        {
            closestDoor.GetComponent<Animator>().Play("SlidingUpDoorOpen");
            StartCoroutine(DoorExit());
        }
        else
        {
            closestDoor.GetComponent<Animator>().Play("SlidingUpDoorOpen");
            closestDoor.GetComponent<Collider2D>().enabled = true;
        }
        Debug.Log(closestDoor);
    }


    void DoorEnter()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("DoorEnterSpot");
        //disable player movement
        player.GetComponent<PlayerController>().enabled = false;
        //disable door triggers
        GetComponent<Collider2D>().enabled = false;
        //find closest door
        foreach (GameObject obj in objectsWithTag)
        {
            if(Vector3.Distance(player.transform.position, obj.transform.position) <= Vector3.Distance(player.transform.position, closestEnterSpot.transform.position))
            {
                closestEnterSpot = obj;
            }
        }
        //set next door spot
        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            if (objectsWithTag[i].name == closestEnterSpot.name && objectsWithTag[i].transform.parent.name != closestEnterSpot.transform.parent.name)
            {
                doorEnterSpotNext = objectsWithTag[i].transform.position;
                break;
            }
        }
        Debug.Log(doorEnterSpotNext);
        player.transform.position = closestEnterSpot.transform.position;
        doorEntered = true;
        StartCoroutine(DoorClose(0.5f));
    }

    IEnumerator DoorExit()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("DoorExitSpot");
        GameObject[] doorButtons = GameObject.FindGameObjectsWithTag("DoorButton");
        yield return new WaitForSeconds(1);
        foreach (GameObject obj in objectsWithTag)
        {
            if (Vector3.Distance(player.transform.position, obj.transform.position) <= Vector3.Distance(player.transform.position, closestExitSpot.transform.position))
            {
                closestExitSpot = obj;
            }
        }
        foreach (GameObject button in doorButtons)
        {
            button.GetComponent<Collider2D>().enabled = true;
        }
        doorEntered = false;
        player.transform.position = closestExitSpot.transform.position;
        StartCoroutine(DoorClose(0));
    }

    IEnumerator DoorClose(float closeTime)
    {
        yield return new WaitForSeconds(closeTime);
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Door");
        Debug.Log("Before: " + closestDoor);
        foreach (GameObject obj in objectsWithTag)
        {
            if (Vector3.Distance(player.transform.position, obj.transform.position) <= Vector3.Distance(player.transform.position, closestDoor.transform.position))
            {
                closestDoor = obj;
            }
        }
        closestDoor.GetComponent<Animator>().SetTrigger("CloseDoor");
        yield return new WaitForSeconds(1);
        Debug.Log("After: " + closestDoor);
        if (doorEntered)
        {
            yield return new WaitForSeconds(1);
            player.transform.position = doorEnterSpotNext;
            DoorOpen();
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }

}
