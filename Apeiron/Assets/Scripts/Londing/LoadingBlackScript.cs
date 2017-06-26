using UnityEngine;
using System.Collections;

public class LoadingBlackScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        bool Balck = true;
        if (PlayerPrefs.GetInt("LoadingBlack", 1) == 0)
            Balck = true;
        else
            Balck = false;
        gameObject.SetActive(Balck);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
