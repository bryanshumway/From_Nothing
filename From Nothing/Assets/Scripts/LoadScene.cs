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

public class LoadScene : MonoBehaviour
{
    
    public string SceneToLoad;//variable name that will be the name of the next scene
    public string settings;//variable name to go to the settings
    public Animator animator; //access the animator

    //Gets the level that is to be loaded and fades from the current screen to the next level
    public void FadeToLevel(string scene)
    {
        //Gets the name of the scene that needs to be loaded in next
        SceneToLoad = scene;
        //Set the animation trigger to true
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
    
    //Loads the first level
    public void LoadGame(string scene)
    {
        //Calls Unity Scene manger to load the specific scene
        animator.SetTrigger("FadeOut");
        SceneManager.LoadScene(scene);
    }

    //Load the settings option in the main menu. 
    //Find some way to save these settings
    public void LoadSettings(string settings)
    {
        animator.SetTrigger("FadeOut");
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
