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
    public GameObject settingsPanel;//panel that holds the settings' UI
    public GameObject menuPanel;//panel that holds the main menu's UI
    public GameObject fade;//fade panel
    public GameObject storyText;//story text
    public GameObject continueText;//press f to continue text

    private bool canContinue = false;//allow player to press f to continue


    private void Start()
    {
        fade.GetComponent<Animation>().Play("FadeToClear");
        StartCoroutine(FadeRemove());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canContinue)
        {
            StartCoroutine(Continue());
            canContinue = false;
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
        fade.SetActive(true);
        fade.GetComponent<Animation>().Play("FadeToBlack");
        yield return new WaitForSeconds(1);
        storyText.SetActive(true);
        storyText.GetComponent<Animation>().Play("StoryTextFade");
        yield return new WaitForSeconds(3);
        canContinue = true;
    }

    IEnumerator Continue()
    {
        storyText.GetComponent<Animation>().Play("StoryTextFadeOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Load the settings option in the main menu. 
    public void LoadSettings(string settings)
    {
        //animator.SetTrigger("FadeOut");
        //SceneManager.LoadScene(settings);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    //Returns to main menu from settings
    public void SettingsBack()
    {
        //animator.SetTrigger("FadeOut");
        //SceneManager.LoadScene(settings);
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    //Exits the game for the main start menu
    public void QuitGame()
    {
        //For testing that the game closes while testing in the editor
        Debug.Log("Game Exited");
        //Kills the program
        Application.Quit();
    }

    //remove fade panel after 1 second
    IEnumerator FadeRemove()
    {
        yield return new WaitForSeconds(1);
        fade.SetActive(false);
    }

}
