using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStation : MonoBehaviour
{

    public static int enemySpawns;

    public GameObject slime;
    public GameObject slimeSpawn;
    public GameObject slimeLight;
    public int slimeCount;
    public float spawnTime;

    IEnumerator Start()
    {
        while (enemySpawns < slimeCount)
        {
            Instantiate(slime, slimeSpawn.transform.position, slime.transform.rotation);
            slime.GetComponent<Animation>().Play();
            slimeLight.GetComponent<Animation>().Play();
            enemySpawns += 1;
            yield return new WaitForSeconds(spawnTime);
        }
    }

}
