using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ApeironUIScript : MonoBehaviour {
    private Transform gamemanager;
    [SerializeField]
    private Transform UIManager;
    private Transform player;
    [SerializeField]
    private Image[] EscUISelect;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private AudioClip BattleMusic;
    private bool DescriptionUIOpen = false;
    private bool ScoreUIOpen = false;


    [SerializeField]
    private GameObject DescriptionUI;
    [SerializeField]
    private GameObject ScoreUI;
    private AudioSource audioSource;
    private int batten = 1;

    private StageUIScript StageUI;
    // Use this for initialization
    void Start()
    {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        audioSource = GetComponent<AudioSource>();
        EscSelectEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        move = Input.GetAxisRaw("Vertical");
        if (Input.anyKeyDown)
        {
            batten -= (int)move;

            if (move != 0)
            {
                if (batten > EscUISelect.Length) batten = 1;
                if (batten < 1) batten = EscUISelect.Length;
                EscSelectEnabled();
                //audioSource.clip = music;
                audioSource.PlayOneShot(music);
                //audioSource.clip = Choose;
                //audioSource.PlayOneShot(Choose);
            }
        }
        if (DescriptionUIOpen == false && ScoreUIOpen == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (batten == 1)
                {

                    gamemanager = GameObject.FindWithTag("GameManager").transform;
                    gamemanager.GetComponent<GameManager>().ApeironMode = true;
                    gamemanager.GetComponent<GameManager>().round = 1;
                    gamemanager.GetComponent<GameManager>().Score = 0;
                    UIManager.transform.GetComponent<UIManagerScript>().GoBack();
                    PlayerPrefs.SetInt("LoadingBlack", 1);
                    if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
                    if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));

                    //PlayerPrefs.SetFloat("IsSavePosX", 500f);
                    //PlayerPrefs.SetFloat("IsSavePosY", 500f);
                    //PlayerPrefs.SetString("SaveBackScence", "StartMenu");
                   // PlayerPrefs.SetString("SaveBackAudio", "StartMenu");

                    gamemanager.transform.GetComponent<GameManager>().PlayerMAXHP = 5;
                    gamemanager.transform.GetComponent<GameManager>().PlayerMAXMove = 4;
                    gamemanager.transform.GetComponent<GameManager>().PlayerHP = 5;
                    gamemanager.transform.GetComponent<GameManager>().PlayerHaveEarthMagic = 1;
                    gamemanager.transform.GetComponent<GameManager>().PlayerHaveMineMagic = 1;

                    UIManager.transform.GetComponent<UIManagerScript>().GoBack();


                    StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen("ApeironMode", new Vector2(100f, 100f), BattleMusic, false, true));

                }
                else if (batten == 2)
                {
                    ScoreUI.SetActive(true);
                    ScoreUIOpen = true;
                    //UIManager.transform.GetComponent<UIManagerScript>().ToScreen(BookCardUI);
                }
                else if (batten == 3)
                {
                    DescriptionUI.SetActive(true);
                    DescriptionUIOpen = true;
                }
                else if (batten == 4)
                {
                    UIManager.transform.GetComponent<UIManagerScript>().GoBack();
                }

            }

            if (Input.GetButtonDown("Fire2"))
            {
                UIManager.transform.GetComponent<UIManagerScript>().GoBack();
            }




        }
        else if (DescriptionUIOpen == true)
        {
            if (Input.anyKeyDown)
            {
                //if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2")) {
                    DescriptionUIOpen = false;
                    DescriptionUI.SetActive(false);
                //}

            }
        }
        else if (ScoreUIOpen == true)
        {
            if (Input.anyKeyDown)
            {
                //if (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2")) {
                ScoreUI.SetActive(false);
                ScoreUIOpen = false;
                //}

            }
        }

    }
        

    void EscSelectEnabled()
    {
        for (int x = 0; x < EscUISelect.Length; x++)
        {

            if (x == batten - 1)
                EscUISelect[x].enabled = true;
            else
                EscUISelect[x].enabled = false;

        }

    }
}
