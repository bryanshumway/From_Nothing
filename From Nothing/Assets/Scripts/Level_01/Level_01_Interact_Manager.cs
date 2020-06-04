using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_01_Interact_Manager : MonoBehaviour
{

    public static Vector3 elevatorEnterSpot;
    public static Vector3 camSpot;
    public static Vector3 camSpotNew;
    public static bool elevatorButtonPressed = false;
    public static bool level2Access = false;
    public static bool level3Access = false;
    public static bool level4Access = false;
    public static bool level5Access = false;

    public GameObject elevatorPanel;

    private int floor = 5;
    private GameObject player;
    private GameObject camera;
    private GameObject messagePanel;
    private GameObject closestDoor;
    private GameObject closestEnterSpot;
    private GameObject closestExitSpot;
    private Text messageName;
    private Text messageText;
    private Vector3 doorEnterSpotNext;
    private Vector3 doorEnterSpotNextCustom;
    private bool customEnterSpot = false;
    private bool customEnterSpotElevator = false;
    private bool doorEntered;

    //status variables
    #region

    //enter door status
    private bool doorEnter;
    public static int doorEnterStatus = 0;
    //elevator status
    public bool elevatorActive;
    public static int elevatorStatus = 0;
    //crystal launcher status
    private bool active01;
    public static int status01 = 0;
    //door 01 button status
    private bool active02;
    public static int status02 = 0;
    //boss door pass status
    private bool active03;
    public static int status03 = 0;
    //boots status
    private bool activeBoots;
    public static int statusBoots = 0;
    //tube status
    private bool activeTube;
    public static int statusTube = 0;
    //keycard01 status
    private bool activeKeycard01;
    public static int statusKeycard = 0;

    #endregion

    private void Awake()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        messagePanel = GameObject.Find("MessagePanel");
        messageName = GameObject.Find("MessageName").GetComponent<Text>();
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        closestDoor = GameObject.FindGameObjectWithTag("Door");
        closestEnterSpot = GameObject.FindGameObjectWithTag("DoorEnterSpot");
        closestExitSpot = GameObject.FindGameObjectWithTag("DoorExitSpot");
    }

    //door enter/exit logic
    //message box logic
    #region

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
            //boots status
            if (activeBoots)
            {
                //normal message exit
                if (statusBoots == 1)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    PlayerController.canJump = true;
                    Destroy(gameObject);
                }
            }
            //tube status
            if (activeTube)
            {
                //normal message exit
                if (statusTube == 0)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageName.text = "";
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    activeTube = false;
                    GetComponent<Collider2D>().enabled = true;
                    GetComponent<CapsuleCollider2D>().enabled = true;
                    GetComponent<Level_01_Interact_Manager>().enabled = false;
                }
            }
            //keycard01 status
            if (activeKeycard01)
            {
                //normal message exit
                if (statusKeycard == 0)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    messageText.text = "";
                    messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    Destroy(gameObject);
                }
            }
            //eleavtor
            if (elevatorActive)
            {
                //normal message exit
                if (elevatorStatus == 0)
                {
                    elevatorPanel.SetActive(false);
                    player.GetComponent<PlayerController>().enabled = true;
                    elevatorActive = false;
                    GetComponent<Collider2D>().enabled = true;
                    GetComponent<Level_01_Interact_Manager>().enabled = false;
                }
            }
        }
    }

    #endregion

    //when player presses F on object, this happens first
    #region

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
        //boots
        else if (name == "boots")
        {
            //when player picks up boots
            if (statusBoots == 0)
            {
                messageText.text = "You found some Boots. You can now jump.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                activeBoots = true;
                player.GetComponent<PlayerController>().EnableBoots();
                PlayerController.footprintActive = false;
                GetComponentInChildren<MeshRenderer>().enabled = false;
                StartCoroutine(StatusBoots());
            }
        }
        //test tube
        else if (name == "Test_Tube_01")
        {
            //when player interacts with test tube
            if (statusTube == 0)
            {
                messageName.text = "You";
                messageText.text = "Why am I here..?";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                GetComponent<CapsuleCollider2D>().enabled = false;
                activeTube = true;
            }
        }
        //keycard01
        else if (name == "keycard01")
        {
            //when player picks up keycard
            if (statusKeycard == 0)
            {
                messageText.text = "You found a keycard that allows access to Floor 03.";
                messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
                activeKeycard01 = true;
                level2Access = true;
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
                    messageText.text = "You press the button. There seems to be no power running.";
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
        //elevator buttons
        else if (CompareTag("ElevatorButton"))
        {
            if (elevatorStatus == 0)
            {
                player.GetComponent<PlayerController>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                elevatorPanel.SetActive(true);
                elevatorActive = true;
            }
            else if (elevatorStatus == 1)
            {
                customEnterSpotElevator = true;
                DoorEnter();
            }
        }
        //door enter
        else if (CompareTag("Door"))
        {
            if (name == "floor02door02")
            {
                doorEnterSpotNextCustom = GameObject.Find("enterSpotBoots").transform.position;
                customEnterSpot = true;
                camSpot = camera.transform.position;
                camSpotNew = GameObject.Find("camSpotBoots").transform.position;
                StartCoroutine(DirectionalLightOff());
            }
            else if (name == "bootsRoomDoor")
            {
                doorEnterSpotNextCustom = GameObject.Find("enterSpotf01d02").transform.position;
                customEnterSpot = true;
                camSpotNew = camSpot;
                StartCoroutine(DirectionalLightOn());
            }
            DoorEnter();
        }
    }

    #endregion

    //door code
    #region
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
            if (!elevatorButtonPressed)
            {
                closestDoor.GetComponent<Collider2D>().enabled = true;
            }
        }
        //Debug.Log(closestDoor);
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
        //Debug.Log(doorEnterSpotNext);
        player.transform.position = closestEnterSpot.transform.position;
        doorEntered = true;
        StartCoroutine(DoorClose(0.5f));
    }

    IEnumerator DoorExit()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("DoorExitSpot");
        GameObject[] doorButtons = GameObject.FindGameObjectsWithTag("DoorButton");
        GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
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
        foreach (GameObject button in elevatorButtons)
        {
            button.GetComponent<Collider2D>().enabled = true;
            button.GetComponent<Level_01_Interact_Manager>().enabled = false;
        }
        doorEntered = false;
        if (elevatorButtonPressed)
        {
            player.transform.localScale = new Vector3(player.transform.localScale.x, player.transform.localScale.y, -player.transform.localScale.z);
            elevatorButtonPressed = false;
        }
        player.transform.position = closestExitSpot.transform.position;
        elevatorStatus = 0;
        StartCoroutine(DoorClose(0));
    }

    IEnumerator DoorClose(float closeTime)
    {
        yield return new WaitForSeconds(closeTime);
        if (doorEntered && customEnterSpot)
        {
            GameObject.Find("Fade").GetComponent<Animation>().Play("FadeOut");
        }
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Door");
        //Debug.Log("Before: " + closestDoor);
        foreach (GameObject obj in objectsWithTag)
        {
            if (Vector3.Distance(player.transform.position, obj.transform.position) <= Vector3.Distance(player.transform.position, closestDoor.transform.position))
            {
                closestDoor = obj;
            }
        }
        closestDoor.GetComponent<Animator>().SetTrigger("CloseDoor");
        yield return new WaitForSeconds(1);
        if (doorEntered)
        {
            yield return new WaitForSeconds(1);
            if (customEnterSpot)
            {
                player.transform.position = doorEnterSpotNextCustom;
                camera.transform.position = camSpotNew;
                GameObject.Find("Fade").GetComponent<Animation>().Play("FadeIn");
                customEnterSpot = false;
            }
            else if (customEnterSpotElevator)
            {
                player.transform.position = elevatorEnterSpot;
                customEnterSpotElevator = false;
                elevatorActive = false;
            }
            else
            {
                player.transform.position = doorEnterSpotNext;
            }
            DoorOpen();
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
    #endregion

    //elevator code
    #region

    public void Floor4A()
    {
        if (floor == 5)
        {
            messageText.text = "You're already on this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else
        {
            floor = 5;
            elevatorPanel.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            elevatorEnterSpot = GameObject.Find("enterSpotf02e01").transform.position;
            GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
            foreach (GameObject button in elevatorButtons)
            {
                button.GetComponent<Collider2D>().enabled = true;
                button.GetComponent<Level_01_Interact_Manager>().enabled = false;
            }
            elevatorStatus = 1;
            elevatorButtonPressed = true;
            DoorOpen();
        }
    }

    public void Floor4B()
    {
        if (floor == 4)
        {
            messageText.text = "You're already on this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else
        {
            floor = 4;
            elevatorPanel.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            elevatorEnterSpot = GameObject.Find("enterSpotf01e01").transform.position;
            GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
            foreach (GameObject button in elevatorButtons)
            {
                button.GetComponent<Collider2D>().enabled = true;
                button.GetComponent<Level_01_Interact_Manager>().enabled = false;
            }
            elevatorStatus = 1;
            elevatorButtonPressed = true;
            DoorOpen();
        }
    }

    public void Floor3()
    {
        if (floor == 3)
        {
            messageText.text = "You're already on this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else if (!level2Access)
        {
            messageText.text = "It seems you need a keycard to access this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else
        {
            floor = 3;
            elevatorPanel.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            customEnterSpot = true;
            StartCoroutine(Level02Load());
            GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
            foreach (GameObject button in elevatorButtons)
            {
                button.GetComponent<Collider2D>().enabled = true;
                button.GetComponent<Level_01_Interact_Manager>().enabled = false;
            }
            elevatorStatus = 1;
            elevatorButtonPressed = true;
            DoorOpen();
        }
    }

    public void Floor2()
    {
        if (floor == 2)
        {
            messageText.text = "You're already on this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else if (!level3Access)
        {
            messageText.text = "It seems you need a keycard to access this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else
        {
            floor = 2;
            elevatorPanel.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            elevatorEnterSpot = GameObject.Find("enterSpotf01e01").transform.position;
            GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
            foreach (GameObject button in elevatorButtons)
            {
                button.GetComponent<Collider2D>().enabled = true;
                button.GetComponent<Level_01_Interact_Manager>().enabled = false;
            }
            elevatorStatus = 1;
            elevatorButtonPressed = true;
            DoorOpen();
        }
    }

    public void Floor1()
    {
        if (floor == 1)
        {
            messageText.text = "You're already on this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else if (!level4Access)
        {
            messageText.text = "It seems you need a keycard to access this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else
        {
            floor = 1;
            elevatorPanel.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            elevatorEnterSpot = GameObject.Find("enterSpotf01e01").transform.position;
            GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
            foreach (GameObject button in elevatorButtons)
            {
                button.GetComponent<Collider2D>().enabled = true;
                button.GetComponent<Level_01_Interact_Manager>().enabled = false;
            }
            elevatorStatus = 1;
            elevatorButtonPressed = true;
            DoorOpen();
        }
    }

    public void Floor0()
    {
        if (floor == 0)
        {
            messageText.text = "You're already on this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else if (!level5Access)
        {
            messageText.text = "It seems you need a keycard to access this floor.";
            messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.4f);
            StopCoroutine("MessageOff");
            StartCoroutine("MessageOff");
        }
        else
        {
            floor = 0;
            elevatorPanel.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            elevatorEnterSpot = GameObject.Find("enterSpotf01e01").transform.position;
            GameObject[] elevatorButtons = GameObject.FindGameObjectsWithTag("ElevatorButton");
            foreach (GameObject button in elevatorButtons)
            {
                button.GetComponent<Collider2D>().enabled = true;
                button.GetComponent<Level_01_Interact_Manager>().enabled = false;
            }
            elevatorStatus = 1;
            elevatorButtonPressed = true;
            DoorOpen();
        }
    }

    #endregion

    //coroutines
    #region

    IEnumerator DirectionalLightOff()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0;
    }

    IEnumerator DirectionalLightOn()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0.3f;
    }

    IEnumerator MessageOff()
    {
        yield return new WaitForSeconds(2);
        messageText.text = "";
        messagePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    IEnumerator StatusBoots()
    {
        yield return new WaitForSeconds(0.1f);
        statusBoots = 1;
    }
    IEnumerator Level02Load()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("level02");
    }

    #endregion
}
