using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EscUIScript : MonoBehaviour {
    private Transform gamemanager;
    [SerializeField]
    private Transform UIManager;
    private Transform player;
    [SerializeField]
    private Image[] EscUISelect;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private AudioClip StartMusic;
    [SerializeField]
    private GameObject OptionUI;
    [SerializeField]
    private GameObject BookCardUI;
    private AudioSource audioSource;
    private int batten = 1;

    private StageUIScript StageUI;
    // Use this for initialization
    void Start () {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        audioSource = GetComponent<AudioSource>();
        EscSelectEnabled();
    }
	
	// Update is called once per frame
	void Update () {
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
        if (Input.GetButtonDown("Fire1"))
        {
            if (batten == 1)
            {
                UIManager.transform.GetComponent<UIManagerScript>().GoBack();
                if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
                if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));

            }
            else if (batten == 2) {
                UIManager.transform.GetComponent<UIManagerScript>().ToScreen(BookCardUI);
            }
            else if (batten == 3)
            {
                UIManager.transform.GetComponent<UIManagerScript>().ToScreen(OptionUI);
            }
            else if (batten == 4)
            {
                gamemanager = GameObject.FindWithTag("GameManager").transform;
                UIManager.transform.GetComponent<UIManagerScript>().GoBack();
                if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
                if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));
                StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen("StartMenu", new Vector2(100f,100f), StartMusic, false, false));

            }

        }

        if (Input.GetButtonDown("Fire2"))
        {
            UIManager.transform.GetComponent<UIManagerScript>().GoBack();
            if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
            if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));
        }




    }

    void EscSelectEnabled() {
        for (int x = 0; x < EscUISelect.Length; x++) {

            if (x == batten - 1)
                EscUISelect[x].enabled = true;
            else
                EscUISelect[x].enabled = false;

        }
        
    }
}
