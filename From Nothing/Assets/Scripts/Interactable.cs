using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    private GameObject player;
    private GameObject pickupSpot;
    private GameObject interactPanel;
    private Text interactText;
    public Color iPanelColorCurrent;

    public bool canPickup;
    public bool canInteract;

    private void Start()
    {
        player = GameObject.Find("PlayerModel");
        pickupSpot = GameObject.Find("PickupSpot");
        interactPanel = GameObject.Find("InteractPanel");
        interactText = GameObject.Find("InteractText").GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canPickup)
            {
                transform.parent = pickupSpot.transform;
                transform.localPosition = new Vector3(0, 0, 0);
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Level_01_Interact_Manager>().enabled = true;
                GetComponent<Level_01_Interact_Manager>().StatusCheck();
            }
            else if (canInteract)
            {
                player.GetComponent<PlayerController>().enabled = false;
                interactPanel.GetComponent<Animation>().Stop();
                interactPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                interactText.color = new Color(255, 255, 255, 0);
                GetComponent<Level_01_Interact_Manager>().enabled = true;
                GetComponent<Level_01_Interact_Manager>().StatusCheck();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("Pickup") && other.CompareTag("Player"))
        {
            //interactPanel.GetComponent<Animation>().Stop();
            //interactPanel.GetComponent<Animation>().Play("InteractFadeIn");
            iPanelColorCurrent = interactPanel.GetComponent<Image>().color;
            interactText.text = "Press F to pick up";
            canPickup = true;
        }
        else if (CompareTag("Interact") && other.CompareTag("Player"))
        {
            //interactPanel.GetComponent<Animation>().Stop();
            //interactPanel.GetComponent<Animation>().Play("InteractFadeIn");
            iPanelColorCurrent = interactPanel.GetComponent<Image>().color;
            interactText.text = "Press F to interact";
            StartCoroutine("FadeIn");
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CompareTag("Pickup") && other.CompareTag("Player"))
        {
            //interactPanel.GetComponent<Animation>().Stop();
            //interactPanel.GetComponent<Animation>().Play("InteractFadeOut");
            iPanelColorCurrent = interactPanel.GetComponent<Image>().color;
            canPickup = false;
        }
        else if (CompareTag("Interact") && other.CompareTag("Player"))
        {
            //interactPanel.GetComponent<Animation>().Stop();
            //interactPanel.GetComponent<Animation>().Play("InteractFadeOut");
            iPanelColorCurrent = interactPanel.GetComponent<Image>().color;
            StopCoroutine("FadeIn");
            StartCoroutine("FadeOut");
            canInteract = false;
        }
    }

    IEnumerator FadeIn()
    {
        for (float i = iPanelColorCurrent.a; i < 0.3f; i+=0.05f)
        {
            iPanelColorCurrent.a = i;
            interactPanel.GetComponent<Image>().color = iPanelColorCurrent;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = iPanelColorCurrent.a; i > 0; i -= 0.05f)
        {
            iPanelColorCurrent.a = i;
            interactPanel.GetComponent<Image>().color = iPanelColorCurrent;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
