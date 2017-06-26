using UnityEngine;
using System.Collections;

public class StoreScript : MonoBehaviour {
    [SerializeField]
    private Transform UIManager;
    [SerializeField]
    private GameObject WatchUI;

    [SerializeField]
    private GameObject ZeroUI;

    private Transform player;
    // Use this for initialization
    void Start () {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        ZeroUI = GameObject.FindWithTag("ZeroUI");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay2D(Collider2D obj)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            UIManager.transform.GetComponent<UIManagerScript>().ToScreen(WatchUI);
            if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
            if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(true));

        }
    }
}
