using UnityEngine;
using System.Collections;

public class TransferPoint : MonoBehaviour {
    [SerializeField]
    private Transform gamemanager;
    [SerializeField]
    private Vector2 playerpos;
    [SerializeField]
    private string scencename;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private bool LoadBlakc = true;
    [SerializeField]
    private bool CanMove = true;

    private bool time = false;
    private bool Load = false;
    private float NowTime = 0;

    private bool cantransfer = true;
    // Use this for initialization
    void Awake () {
        // screenmanager = GameObject.FindWithTag("screenmanager").transform;
        NowTime = 0;
        time = false;
        Load = false;
    }
	
	// Update is called once per frame
	void Update () {
        NowTime += Time.deltaTime;
        if (NowTime > 5f) time = true;
    }
    /*
    void OnCollisionEnter2D(Collision2D obj)
    {
        gamemanager = GameObject.FindWithTag("GameManager").transform;
        if (obj.gameObject.tag == "player")
        {
            if(LoadBlakc == true)
                PlayerPrefs.SetInt("LoadingBlack", 0);
            else
                PlayerPrefs.SetInt("LoadingBlack", 1);
           if(time == true) StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(scencename, playerpos,music , CanMove , false));
        }
    }
    */
    void OnCollisionStay2D(Collision2D obj)
    {
        gamemanager = GameObject.FindWithTag("GameManager").transform;
        if (obj.gameObject.tag == "player")
        {
            if (LoadBlakc == true)
                PlayerPrefs.SetInt("LoadingBlack", 0);
            else
                PlayerPrefs.SetInt("LoadingBlack", 1);
            if (time == true && Load == false) {
                StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(scencename, playerpos, music, CanMove, false));
                Load = true;
            }
        }
    }
    
}
