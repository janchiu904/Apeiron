using UnityEngine;
using System.Collections;

public class LightScript : MonoBehaviour {
    private Transform player;
    // Use this for initialization
    void Awake()
    {
        // Setting up the reference.
        player = GameObject.FindWithTag("player").transform;

    }

    // Update is called once per frame
    void Update () {

        transform.position = player.position;
	}
}
