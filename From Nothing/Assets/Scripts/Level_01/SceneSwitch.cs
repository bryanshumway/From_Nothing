using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{

    public GameObject camera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Animator>().SetBool("IsWalking", false);
            other.GetComponent<PlayerController>().enabled = false;
            camera.GetComponent<Animation>().Play("lvl1sc1to2");
            StartCoroutine(Switch());
        }
    }

    IEnumerator Switch()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
    }

}
