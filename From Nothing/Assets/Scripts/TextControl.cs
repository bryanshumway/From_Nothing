/*Taylor Koonce
 April - June 2020
 Controller for the text box. This gets teh proper components that make th text box.
 As things occur in the game different prompts shall be given from the Overseer*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    public Text textbox; //gets access to the text box
    public SpriteRenderer background; //gets the background
    Color invis = new Color(1f, 1f, 1f, 0f); //hides the textbox
    Color full = new Color(1f, 1f, 1f, 1f); //shows the text box
    
    //Hides everything when started
    public void Awake()
    {
        background.color = invis;
        textbox.text = ""; //Sets the text to an empty string
    }

    //Every frame run this
    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            background.color = full;
            textbox.text = "The Child has now been Punted!";
        }
    }
}
