using UnityEngine;
using System.Collections;

public class StageUIScript : MonoBehaviour {
    [SerializeField]
    private Transform UIManager;
    [SerializeField]
    private GameObject WatchUI;

    [SerializeField]
    private GameObject ZeroUI;

    private bool UIisOpen=false;

    private Transform player;
    // Use this for initialization
    void Start () {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        UIisOpen = false;
        ZeroUI = GameObject.FindWithTag("ZeroUI");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Cancel") && UIisOpen == false) {
            UIManager.transform.GetComponent<UIManagerScript>().ToScreen(WatchUI);
            UIisOpen = true;
            if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
            if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(true));
        }
        if (ZeroUI != null) {

                UIisOpen = false;

        }
        
	
	}
}
