using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animation>().Play("FadeToClear");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
