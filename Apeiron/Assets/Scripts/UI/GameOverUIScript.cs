using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverUIScript : MonoBehaviour {
    private Transform gamemanager;

    private BattleManagerScript BattleManager;
    [SerializeField]
    private Image[] GameoverUISelect;
    private int batten = 1;
    private AudioClip MusicName;
    [SerializeField]
    private AudioClip[] Savemusic;
    // Use this for initialization
    void Start () {
        gamemanager = GameObject.FindWithTag("GameManager").transform;
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        GameoverUISelectEnabled();


    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;
        move = Input.GetAxisRaw("Horizontal");
        if (Input.anyKeyDown)
        {
            batten += (int)move;

            if (move != 0)
            {
                if (batten > GameoverUISelect.Length) batten = 1;
                if (batten < 1) batten = GameoverUISelect.Length;
                GameoverUISelectEnabled();
                //audioSource.clip = Choose;
                //audioSource.PlayOneShot(Choose);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                BattleManager.Win = false;
                BattleManager.Die = false;
                BattleManager.PlayerCanCantroller = true;

                BattleManager.CanMagic = true;

                BattleManager.playerPosX = 2;
                BattleManager.playerPosY = 2;
                BattleManager.MagicType = 0;
                BattleManager.PlayerCanMove = true;
                BattleManager.PlayerCanMagic = true;


                if (batten == 1) {
                    if (PlayerPrefs.GetInt("HaveSave", 0) == 1)
                    {
                        PlayerPrefs.SetInt("LoadingBlack", 1);
                        string SaveBackScence = PlayerPrefs.GetString("SaveBackScence", "StartMenu");
                        float SaveIsBattlePosX;
                        float SaveIsBattlePosY;
                        SaveIsBattlePosX = PlayerPrefs.GetFloat("IsSavePosX", 0);
                        SaveIsBattlePosY = PlayerPrefs.GetFloat("IsSavePosY", 0);
                        if (PlayerPrefs.GetString("SaveBackAudio", "forest") == "forest") MusicName = Savemusic[0];
                        Vector2 saveplayerpos = new Vector2(SaveIsBattlePosX, SaveIsBattlePosY);
                        StartCoroutine(gamemanager.GetComponent<GameManager>().ChangeScreen(SaveBackScence, saveplayerpos, MusicName, true, false));

                    }

                }
                else if (batten == 2)
                {
                    MusicName = Savemusic[1];
                    Vector2 playerpos = new Vector2(100, 100);
                    StartCoroutine(gamemanager.GetComponent<GameManager>().ChangeScreen("StartMenu", playerpos, MusicName, false, false));
                }

            }
        }
    }
    void GameoverUISelectEnabled()
    {
        for (int x = 0; x < GameoverUISelect.Length; x++)
        {

            if (x == batten - 1)
                GameoverUISelect[x].enabled = true;
            else
                GameoverUISelect[x].enabled = false;

        }

    }

}
