using UnityEngine;
using System.Collections;

public class MapReastScript : MonoBehaviour {
    private Transform player;
    // Use this for initialization
    void Awake () {
        if (player == null) {
            player = GameObject.FindWithTag("player").transform;

            gameObject.transform.position = new Vector3(player.position.x - 0.5f, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
