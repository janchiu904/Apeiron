using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionUIScript : MonoBehaviour {
    private Transform gamemanager;
    [SerializeField]
    private Transform UIManager;
    [SerializeField]
    private Image[] OptionUISelect;
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
    [SerializeField]
    private int musicTextNow = 3;
    [SerializeField]
    private string[] musicTextALL;
    [SerializeField]
    private Text musicText;
    [SerializeField]
    private Text LeangeText;
    private AudioSource audioSource;
    private int batten = 1;
    [SerializeField]
    private AudioClip music;
    // Use this for initialization
    void Awake() {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        sound = PlayerPrefs.GetInt("KEY_SOUND", 100);
        kmusic = PlayerPrefs.GetInt("KEY_MUSIC", 100);
        audioSource = GetComponent<AudioSource>();
        OptionSelectEnabled();
    }
	
	// Update is called once per frame
	void Update () {
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        float move = 0;
        move = Input.GetAxisRaw("Vertical");
        float AddSound = 0;
        AddSound = Input.GetAxisRaw("Horizontal");
        if (AddSound != 0)
        {
            PlayerPrefs.SetInt("KEY_MUSIC", kmusic);
            PlayerPrefs.SetInt("KEY_SOUND", sound);

        }
        if (Input.anyKeyDown)
        {
            batten -= (int)move;

            if (move != 0)
            {
                if (batten > OptionUISelect.Length) batten = 1;
                if (batten < 1) batten = OptionUISelect.Length;
                OptionSelectEnabled();
                audioSource.PlayOneShot(music);
                //audioSource.clip = Choose;
                //audioSource.PlayOneShot(Choose);
            }

            if (batten == 1)
            {
                if (AddSound == 1)
                {
                    musicTextNow += 1;
                    if (musicTextNow >= musicTextALL.Length) musicTextNow = 0;
                }
                if (AddSound == -1)
                {
                    musicTextNow -= 1;
                    if (musicTextNow < 0) musicTextNow = musicTextALL.Length - 1;
                }
                musicText.text = musicTextALL[musicTextNow];
            }
            else if (batten == 2)
            {
                if (AddSound == 1 || AddSound == -1)
                {
                    if (LeangeText.text == "中")
                        LeangeText.text = "英";
                    else
                        LeangeText.text = "中";
                }
            }
            else if (batten == 3)
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
            else if (batten == 4)
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
        }
        

        if (Input.GetButtonDown("Fire2"))
        {
            UIManager.transform.GetComponent<UIManagerScript>().GoBack();
           
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

    void OptionSelectEnabled()
    {
        for (int x = 0; x < OptionUISelect.Length; x++)
        {

            if (x == batten - 1)
                OptionUISelect[x].enabled = true;
            else
                OptionUISelect[x].enabled = false;

        }

    }
}
