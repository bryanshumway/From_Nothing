using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart2 : MonoBehaviour
{
    public GameObject player;
    public GameObject fade;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        fade.GetComponent<Animation>().Play("FadeIn");
        PlayerController.canJump = true;
        PlayerController.footprintActive = false;
        yield return new WaitForSeconds(1);
        GameObject.Find("door01").GetComponent<Level_01_Interact_Manager>().exitEnablePlayer = false;
        GameObject.Find("door01").GetComponent<Level_01_Interact_Manager>().DoorEntered();
        GameObject.Find("door01").GetComponent<Level_01_Interact_Manager>().DoorOpen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
