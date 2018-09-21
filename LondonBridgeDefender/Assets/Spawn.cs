using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject skeleton1;
    private bool spawnTime;

	void Start ()
    {
		
	}
	
	
	void Update ()
    {
		if(spawnTime == true)
        {
            Instantiate(skeleton1, transform.position, Quaternion.identity);
            spawnTime = false;
        }
	}
    IEnumerator ResetSpawnTime()
    {
        yield return new WaitForSeconds(5.0f);
        spawnTime = true;
    }
}
