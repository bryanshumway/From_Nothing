using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetStatus();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void ResetStatus()
    {
        Level_01_Interact_Manager.status01 = 0;
        Level_01_Interact_Manager.status02 = 0;
        Level_01_Interact_Manager.status03 = 0;
        Level_01_Interact_Manager.doorEnterStatus = 0;
    }
}
