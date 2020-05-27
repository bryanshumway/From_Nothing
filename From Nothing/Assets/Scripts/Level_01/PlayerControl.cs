/*Taylor Koonce
 *April- June 2020
 *Player control code. Anything to do with movement 
 * and items that affect how the player moves. This
 * includes the boots that allow you to jump 
 * and cape that allows you to glide*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed; //Alows us to change move speed from the editor
    private Animator anim;  //For animations later

    // Update is called once per frame
    void Update()
    {
        //Crouch????
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, 0);
            //anim.Play("Back");
        }
        //Jump
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, 0);
            //anim.Play("Forward");
        }
        //Move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, 0);
            //anim.Play("Right");
        }
        //Move Left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y, 0);
            //anim.Play("Left");
        }
        //When Idle
        else
        {
            //anim.Play("Idle");
        }
    }
}
