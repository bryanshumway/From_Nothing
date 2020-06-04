/* Taylor Koonce 
 * April-June 2020
 * Load Scene Manager is needed for the statr menu 
 * Scenes that can lead to different scenes such as settings
 * or the beginning of the game. Also used for quitting
 * Can be used again for loading other scenes. Best used with
 * hit boxes/colliders.
 * Video: https://www.youtube.com/watch?v=Oadq-IrOazg
 */
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour
{

    public string SceneToLoad;//variable name that will be the name of the next scene
    public string settings;//variable name to go to the settings
    //public Animator animator; //access the animator
    int count = 1; //Counter for how many times escape is pressed. Must be set to 1. Code checks for odd or even counts
    public GameObject PauseMenu; //variable for the pause menu game object
    private GameObject player;  //Variable for the player to disable movement

    private void Start()
    {
        GetComponent<Animation>().Play("FadeToClear");//Animation between scenes
        PauseMenu = GameObject.Find("objPauseMenu");//Pause Menu to be able to show it and hide it
        PauseMenu.SetActive(false);
        player = GameObject.Find("Player");

    }

    //Checks for input so often
    private void Update()
    {
        //Check to see if the escape key has been pressed. MUST BE KEY DOWN
        if (Input.GetKeyDown("escape"))
        {
            count++;//Add one to the counter to check to see how many times the escape is pressed

            //If the count is even then activate the menu, disable player movement
            if (count % 2 == 0)
            {
                player.GetComponent<PlayerController>().enabled = false;//Disable player movement
                PauseMenu.SetActive(true);
            }
            //Else If the count is odd then, deactivate the menu, activate player movement
            else
            {
                player.GetComponent<PlayerController>().enabled = true;//Enable player movement
                PauseMenu.SetActive(false);
            }
        }
    }

    //Gets the level that is to be loaded and fades from the current screen to the next level
    public void FadeToLevel(string scene)
    {
        //Gets the name of the scene that needs to be loaded in next
        SceneToLoad = scene;
        //Set the animation trigger to true
        //animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    //Loads the first level
    public void LoadGame(string scene)
    {
        //Calls Unity Scene manger to load the specific scene
        //animator.SetTrigger("FadeOut");
        StartCoroutine(anim(scene));

    }

    IEnumerator anim(string scene)
    {
        GetComponent<Animation>().Play("FadeToBlack");
        yield return new WaitForSeconds(1);
        if (SceneManager.GetActiveScene().name == "scLevel1")
        {
            GameObject.Find("GameObject").GetComponent<LevelStart2>().enabled = true;
        }
        SceneManager.LoadScene(scene);
    }

    //Load the settings option in the main menu. 
    public void LoadSettings(string settings)
    {
        //animator.SetTrigger("FadeOut");
        SceneManager.LoadScene(settings);
    }

    //Exits the game for the main start menu
    public void QuitGame()
    {
        //For testing that the game closes while testing in the editor
        Debug.Log("Game Exited");
        //Kills the program
        Application.Quit();
    }

}
