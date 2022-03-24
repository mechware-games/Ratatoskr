using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform spawnLocation;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        player.position = spawnLocation.position;
	}
}
