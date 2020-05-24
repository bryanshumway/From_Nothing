using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameObject pickupSpot;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            this.transform.parent = pickupSpot.transform;
            this.transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

}
