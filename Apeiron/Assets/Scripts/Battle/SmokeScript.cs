using UnityEngine;
using System.Collections;

public class SmokeScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Destroy(this.gameObject, 10f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
