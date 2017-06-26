using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattenLightScript : MonoBehaviour {
    private bool canMove = true;
    private GameManager gameManagerScript;
    Animator anim;

    private GameObject player;

    private float batten = 1;
    private float MUbatten = 0;
    [SerializeField]
    private Transform[] BattenPosition;

    [SerializeField]
    private Transform gamemanager;
    [SerializeField]
    private Vector2 playerpos;
    [SerializeField]
    private string scencename;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private AudioClip Choose;
    [SerializeField]
    private AudioClip Confirm;

    [SerializeField]
    private Transform musicLight;
    [SerializeField]
    private Text musicText;
    [SerializeField]
    private Text LeangeText;
    [SerializeField]
    private string[] musicTextALL;
    [SerializeField]
    private int musicTextNow = 3;
    private bool musicUi = false;
    private bool ApeironUi = false;
    [SerializeField]
    private Image[] KEYmusic;
    [SerializeField]
    private Image[] KEYsound;
    [SerializeField]
    private Sprite[] KEYAddLess;
    [SerializeField]
    private int sound;
    [SerializeField]
    private int kmusic;

    private Transform UIManager;
    [SerializeField]
    private GameObject ApeironUIGameobject;


    private AudioClip MusicName;
    [SerializeField]
    private AudioClip[] Savemusic;
    private AudioSource audioSource;

    // Use this for initialization

    void Awake()
    {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        sound = PlayerPrefs.GetInt("KEY_SOUND", 100);
        kmusic = PlayerPrefs.GetInt("KEY_MUSIC", 100);
        anim = GetComponent<Animator>();
        canMove = true;
        batten = 1;
        player = GameObject.FindWithTag("player");
        if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(true));
    }
	
	// Update is called once per frame
	void Update () {
        if (ApeironUIGameobject.active == false) {

            ApeironUi = false;
        }
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        if (musicUi == false && ApeironUi == false)
        {
            float move = 0;

            if (canMove == true) { move = Input.GetAxisRaw("Vertical"); }

            if (Input.anyKeyDown) {
                batten -= move;
                if (move != 0) {
                    //audioSource.clip = Choose;
                    audioSource.PlayOneShot(Choose);
                }               
            }
            if (batten > BattenPosition.Length) batten = 1;
            if (batten < 1) batten = BattenPosition.Length;
            gameObject.transform.localPosition = new Vector2(-83, BattenPosition[(int)batten - 1].localPosition.y - 11);
            //transform.position = BattenPosition[(int)batten - 1].position;
            if (Input.GetButtonDown("Fire1"))
            {
                if (batten == 1)
                {
                    gameManagerScript.PlayerHP =  3;
                    gameManagerScript.PlayerMAXHP =  3;
                    gameManagerScript.PlayerMAXMove =  2;
                    gameManagerScript.PlayerMoney =  0;
                    gameManagerScript.PlayerMoveAddTime = 2;
                    gameManagerScript.PlayerMagicAddTime = 3;
                    gameManagerScript.PlayerHaveMineMagic =  0;
                    gameManagerScript.PlayerHaveEarthMagic = 0;

                    canMove = false;
                    audioSource.PlayOneShot(Confirm);
                    gamemanager = GameObject.FindWithTag("GameManager").transform;
                    StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(scencename, playerpos, music, false, false));
                }
                else if (batten == 4)
                {
                    anim.SetBool("Music", true);
                    musicUi = true;
                }
                else if (batten == 5)
                {
                    Application.Quit();
                    //UnityEditor.EditorApplication.isPlaying = false;
                }
                else if (batten == 2)
                {
                    if (PlayerPrefs.GetInt("HaveSave", 0) == 1) {
                        gamemanager = GameObject.FindWithTag("GameManager").transform;
                        PlayerPrefs.SetInt("LoadingBlack", 1);
                        string SaveBackScence = PlayerPrefs.GetString("SaveBackScence", "StareMenu");
                        float SaveIsBattlePosX;
                        float SaveIsBattlePosY;
                        SaveIsBattlePosX = PlayerPrefs.GetFloat("IsSavePosX", 0);
                        SaveIsBattlePosY = PlayerPrefs.GetFloat("IsSavePosY", 0);
                        if (PlayerPrefs.GetString("SaveBackAudio", "forest") == "forest") MusicName = Savemusic[0];
                        if (PlayerPrefs.GetString("SaveBackAudio", "forest") == "cave") MusicName = Savemusic[1];
                        Vector2 saveplayerpos = new Vector2(SaveIsBattlePosX, SaveIsBattlePosY);
                        StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(SaveBackScence, saveplayerpos, MusicName, true, false));

                    }
                   
                }
                else if (batten == 3)
                {
                   ApeironUi = true;
                   UIManager.transform.GetComponent<UIManagerScript>().ToScreen(ApeironUIGameobject);

                }

            }

        }
        else if(musicUi)
        {
            for (int x = 0; x < 10; x++)
            {

                if (kmusic / 10 <= x)
                {
                    KEYmusic[x].sprite = KEYAddLess[0];
                }
                else
                {
                    KEYmusic[x].sprite = KEYAddLess[1];

                }
                if (sound / 10 <= x)
                {
                    KEYsound[x].sprite = KEYAddLess[0];
                }
                else
                {
                    KEYsound[x].sprite = KEYAddLess[1];

                }

            }
            MusicUI();
        }
        
        
    }
    void MusicUI() {

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetBool("Music", false);
            musicUi = false;
        }
            float move = 0;


        if (canMove == true) { move = Input.GetAxisRaw("Vertical"); }

        if (Input.anyKeyDown) MUbatten -= move;
        if (Input.anyKeyDown)
        {
            //audioSource.clip = Choose;
            audioSource.PlayOneShot(Choose);
        }
        if (MUbatten >= 4) MUbatten = 0;
        if (MUbatten < 0) MUbatten = 3;
        musicLight.transform.localPosition = new Vector2(286.32f, 167- (MUbatten * 141));
        //transform.position = BattenPosition[(int)batten - 1].position;
        float AddSound = 0;
        if (canMove == true) { AddSound = Input.GetAxisRaw("Horizontal"); }
        if (AddSound != 0) {
            PlayerPrefs.SetInt("KEY_MUSIC", kmusic);
            PlayerPrefs.SetInt("KEY_SOUND", sound);

        }
        if (Input.anyKeyDown)
        {
            if (MUbatten == 2)
            {
                if (AddSound == 1)
                {
                    if (kmusic < 100) kmusic += 10;
                }
                if (AddSound == -1)
                {
                    if (kmusic > 0) kmusic -= 10;
                }
            }
            else if (MUbatten == 3)
            {
                if (AddSound == 1)
                {
                    if (sound < 100) sound += 10;
                }
                if (AddSound == -1)
                {
                    if (sound > 0) sound -= 10;
                }
            }
            else if (MUbatten == 0)
            {
                if (AddSound == 1)
                {
                    musicTextNow += 1;
                    if (musicTextNow >= musicTextALL.Length) musicTextNow = 0;
                }
                if (AddSound == -1)
                {
                    musicTextNow -= 1;
                    if (musicTextNow < 0) musicTextNow = musicTextALL.Length-1;
                }
                musicText.text = musicTextALL[musicTextNow];
            }
            else if (MUbatten == 1)
            {
                if (AddSound == 1 || AddSound == -1) {
                    if (LeangeText.text == "中")
                        LeangeText.text = "英";
                    else
                        LeangeText.text = "中";
                }
                

            }
            for (int x = 0; x < 10; x++)
            {

                if (kmusic / 10 <= x)
                {
                    KEYmusic[x].sprite = KEYAddLess[0];
                }
                else
                {
                    KEYmusic[x].sprite = KEYAddLess[1];

                }
                if (sound / 10 <= x)
                {
                    KEYsound[x].sprite = KEYAddLess[0];
                }
                else
                {
                    KEYsound[x].sprite = KEYAddLess[1];

                }

            }
        }
        
    }
}
