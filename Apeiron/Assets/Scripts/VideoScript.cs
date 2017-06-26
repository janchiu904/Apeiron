using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VideoScript : MonoBehaviour {
    [SerializeField]
    private MovieTexture movTexture;
    [SerializeField]
    private float time;
    [SerializeField]
    private string scencename;
    [SerializeField]
    private Vector2 playerpos;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private Animator SkipText;
    [SerializeField]
    private bool PlayerCanMove = true;

    private AudioSource audioSource;

    private Transform gamemanager;

    [SerializeField]
    private bool LoadBlakc = false;

    [SerializeField]
    private bool skip;

    private GameObject player;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        //設置是否循環撥放，這邊設定開頭撥放完就跳轉場景所以不需要Loop
        movTexture.loop = false;
        skip = false;
        gamemanager = GameObject.FindWithTag("GameManager").transform;
        player = GameObject.FindWithTag("player");
        if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(true));

        StartCoroutine(load());
    }
    void stare() {
        if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));

    }
    void OnGUI()
    {
        //繪製紋理
        //GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), movTexture, ScaleMode.StretchToFill);
        
        
    }

    void Update()
    {
        audioSource.volume = PlayerPrefs.GetInt("KEY_MUSIC", 100)*0.01f;
        //if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(true));
        movTexture.Play();
        if (Input.anyKeyDown && skip == true)
        {
            
            StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(scencename, playerpos, music, PlayerCanMove, false));

        }
        else if(Input.anyKeyDown && skip == false) {
            skip = true;
            StartCoroutine(videoskip());
        }
    }

    IEnumerator videoskip()
    {
        SkipText.SetBool("SkipTrue", true);
        yield return new WaitForSeconds(3);
        SkipText.SetBool("SkipTrue", false);
        skip = false;
    }
    IEnumerator load()
    {
        yield return new WaitForSeconds(time);//填入影片時間，這邊假設是20秒
        if (LoadBlakc == true)
            PlayerPrefs.SetInt("LoadingBlack", 0);
        else
            PlayerPrefs.SetInt("LoadingBlack", 1);
        StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(scencename, playerpos, music , PlayerCanMove, false));

        //Application.LoadLevel(2);//自動跳轉第n關，這邊假設是第1關
    }
}
