using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMixer : MonoBehaviour
{

    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus sfxBus;

    [SerializeField]
    [Range(-80f, 10f)]
    private float musicBusVolume;
    private float musicVolume;

    [SerializeField]
    [Range(-80f, 10f)]
    private float sfxBusVolume;
    private float sfxVolume;

    private void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        musicVolume = Mathf.Pow(10.0f, musicBusVolume / 20f);
        musicBus.setVolume(musicVolume);

        sfxVolume = Mathf.Pow(10.0f, sfxBusVolume / 20f);
        sfxBus.setVolume(sfxVolume);
    }

}
