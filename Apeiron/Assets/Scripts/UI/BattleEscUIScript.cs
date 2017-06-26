using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleEscUIScript : MonoBehaviour {
    private Transform gamemanager;
    [SerializeField]
    private Transform UIManager;
    private Transform player;
    [SerializeField]
    private Image[] EscUISelect;
    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private GameObject OptionUI;
    private BattleManagerScript BattleManager;
    private int batten = 1;

    private StageUIScript StageUI;
    // Use this for initialization
    void Start()
    {
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        UIManager = GameObject.FindWithTag("UIManager").transform;
        EscSelectEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0;
        move = Input.GetAxisRaw("Vertical");
        if (Input.anyKeyDown)
        {
            batten -= (int)move;

            if (move != 0)
            {
                if (batten > EscUISelect.Length) batten = 1;
                if (batten < 1) batten = EscUISelect.Length;
                EscSelectEnabled();
                //audioSource.clip = Choose;
                //audioSource.PlayOneShot(Choose);
            }
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (batten == 1)
            {
                UIManager.transform.GetComponent<UIManagerScript>().GoBack();
                BattleManager.BattleStop = false;
                BattleManager.EnemyBattleStop = false;
                BattleManager.PlayerCanCantroller = true;

            }
            else if (batten == 2)
            {

            }
            else if (batten == 3)
            {
                UIManager.transform.GetComponent<UIManagerScript>().ToScreen(OptionUI);
            }
            else if (batten == 4)
            {
                
                gamemanager = GameObject.FindWithTag("GameManager").transform;
                gamemanager.transform.GetComponent<GameManager>().ApeironMode = false;
                UIManager.transform.GetComponent<UIManagerScript>().GoBack();
                if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
                if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));
                StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen("StartMenu", new Vector2(100f, 100f), music, false, false));

            }

        }

        if (Input.GetButtonDown("Fire2"))
        {
            UIManager.transform.GetComponent<UIManagerScript>().GoBack();
            BattleManager.BattleStop = false;
            BattleManager.EnemyBattleStop = false;
            BattleManager.PlayerCanCantroller = true;
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
