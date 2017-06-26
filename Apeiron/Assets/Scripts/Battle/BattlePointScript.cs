using UnityEngine;
using System.Collections;

public class BattlePointScript : MonoBehaviour {
    [SerializeField]
    private Transform gamemanager;
    [SerializeField]
    private Vector2 playerpos;
    [SerializeField]
    private string scencename;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private string BattleBackScenceName;
    [SerializeField]
    private string BattleBackAudioName;
    [SerializeField]
    private SpriteRenderer EnemySprite;
    [SerializeField]
    private bool isBattle = false;

    private int IsBattleEnd;

    private int IsBattlePosX;
    private int IsBattlePosY;

    private bool cantransfer = true;
    private bool StartLoad = false;

    private AudioSource audioSource;
    // Use this for initialization
    void Awake()
    {
        IsBattleEnd = PlayerPrefs.GetInt("IsBattleEnd", 0);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        // screenmanager = GameObject.FindWithTag("screenmanager").transform;
        StartLoad = false;
        if (IsBattleEnd != 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            EnemySprite.enabled = false;
            audioSource.enabled = false;
            PlayerPrefs.DeleteKey("IsBattleEnd");
            


        }

    }

    // Update is called once per frame
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        
        gamemanager = GameObject.FindWithTag("GameManager").transform;
        if (obj.gameObject.tag == "player")
        {

            PlayerPrefs.SetInt("LoadingBlack", 1);
            PlayerPrefs.SetFloat("IsBattlePosX", obj.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("IsBattlePosY", obj.gameObject.transform.position.y);
            PlayerPrefs.SetString("BattleBackScence", BattleBackScenceName);
            PlayerPrefs.SetString("BattleBackAudio", BattleBackAudioName);

            if (StartLoad == false)
            {
                StartLoad = true;
                StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(scencename, playerpos, music, false, isBattle));

            }
        }
    }
}
