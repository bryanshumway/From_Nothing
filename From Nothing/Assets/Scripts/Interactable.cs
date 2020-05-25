using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{

    public GameObject interactPanel;
    public Text interactText;
    public GameObject pickupSpot;

    private bool canPickup;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canPickup)
            {
                transform.parent = pickupSpot.transform;
                transform.localPosition = new Vector3(0, 0, 0);
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("Pickup") && other.CompareTag("Player"))
        {
            interactPanel.GetComponent<Animation>().Stop();
            interactPanel.GetComponent<Animation>().Play("InteractFadeIn");
            interactText.text = "Press F to interact";
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CompareTag("Pickup") && other.CompareTag("Player"))
        {
            interactPanel.GetComponent<Animation>().Stop();
            interactPanel.GetComponent<Animation>().Play("InteractFadeOut");
            canPickup = false;
        }
    }

}
