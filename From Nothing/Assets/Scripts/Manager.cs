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
            Level_01_Interact_Manager.status01 = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
