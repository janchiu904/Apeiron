using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Transform screenmanager;
    

    private Transform player;

    public int PlayerHP;
    public int PlayerMAXHP;
    public int PlayerMAXMove;
    public int PlayerMoney;
    public float PlayerMoveAddTime;
    public float PlayerMagicAddTime;
    public int PlayerHaveMineMagic;
    public int PlayerHaveEarthMagic;

    public bool ApeironMode = false;
    public int round;
    public float Score;

    // Use this for initialization
    void Awake() {
        //Screen.SetResolution(1024, 768, false);
        Transform ins = GameObject.FindWithTag("GameManager").transform;
        if (ins == null)
        {

            ins = this.transform;
            PlayerHP = PlayerPrefs.GetInt("PlayerHP", 3);
            PlayerMAXHP = PlayerPrefs.GetInt("PlayerMAXHP", 3);
            PlayerMAXMove = PlayerPrefs.GetInt("PlayerMAXMove", 2);
            PlayerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
            PlayerMoveAddTime = PlayerPrefs.GetFloat("PlayerMoveAddTime", 2);
            PlayerMagicAddTime = PlayerPrefs.GetFloat("PlayerMagicAddTime", 3);
            PlayerHaveMineMagic = PlayerPrefs.GetInt("PlayerHaveMineMagic", 0);
            PlayerHaveEarthMagic = PlayerPrefs.GetInt("PlayerHaveEarthMagic", 0);


        }
        else if (ins != this.transform)
        {

            Destroy(gameObject);
        }

        int appLaunchedFirstTime = PlayerPrefs.GetInt("KEY_IS_APP_LAUNCHED_FIRST_TIME", 100);
        if (appLaunchedFirstTime == 1)
        {
            PlayerPrefs.SetInt("KEY_IS_APP_LAUNCHED_FIRST_TIME", 0);
            PlayerPrefs.SetInt("KEY_MUSIC", 100);
            PlayerPrefs.SetInt("KEY_SOUND", 100);
            
        }
        PlayerHP = PlayerPrefs.GetInt("PlayerHP", 3);
        PlayerMAXHP = PlayerPrefs.GetInt("PlayerMAXHP", 3);
        PlayerMAXMove = PlayerPrefs.GetInt("PlayerMAXMove", 2);
        PlayerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
        PlayerMoveAddTime = PlayerPrefs.GetFloat("PlayerMoveAddTime", 2);
        PlayerMagicAddTime = PlayerPrefs.GetFloat("PlayerMagicAddTime", 3);
        PlayerHaveMineMagic = PlayerPrefs.GetInt("PlayerHaveMineMagic", 0);
        PlayerHaveEarthMagic = PlayerPrefs.GetInt("PlayerHaveEarthMagic", 0);
    }
    void Start()
    {
        
    }

    void OnGUI()
    {
        
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("h")) {

            PlayerHP += 1;
        }
        if (Input.GetKeyDown("m"))
        {

            PlayerMoney += 100;
        }
    }

    public IEnumerator ChangeScreen(string screen , Vector2 playerpos , AudioClip musicname , bool CanMove , bool IsBattle) {
        player = GameObject.FindWithTag("player").transform;
        if (player != null)
        {
            player = GameObject.FindWithTag("player").transform;

            //player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
        }
        StartCoroutine(screenmanager.transform.GetComponent<ScreenManager>().LoadSceneAsync(screen , musicname , CanMove, IsBattle));
       
        yield return new WaitForSeconds(1f);
        if (player != null) {

            player.transform.position = playerpos;

        }

    }

    
}
