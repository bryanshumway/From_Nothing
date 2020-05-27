/*Taylor Koonce
 April-June 2020
 Sound and volume control.
 Turorial vid: https://www.youtube.com/watch?v=34vXT1CrW_s
  */
using UnityEngine;
using UnityEngine.Audio; //neede for anything using aduio
using UnityEngine.UI; //needed to amnipulate the sliders

public class MixerController : MonoBehaviour
{
    /* Get the neccesary variables form the editor*/
    [Space(10)]
    public AudioMixer audioMixer; 
    public Slider musicSlider;
    public Slider sfxSlider;


    public void SetMusicVolume(float volume)
    {
        //goes and get the MusicVolume from the expose section of
        //the audio mixer, and sets it to the float volume
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    /*Sets the slider to the proper volume when started. */
    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", -40);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", -40);
    }


    //Save the volume to what the player selects in the screen
    private void OnDisable()
    {
        //Deafult values for the sound
        float musicVolume = -40;
        float sfxVolume = -40;

        //Gets the current value of the mixer
        audioMixer.GetFloat("MusicVolume", out musicVolume);
        audioMixer.GetFloat("SFXVolume", out sfxVolume);

        //Saves the players values when they have changed them
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
