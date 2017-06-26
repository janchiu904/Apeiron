using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleManagerScript : MonoBehaviour {
    public Transform[] groundPos;
    public Transform[] EnemygroundPos;

    public int[] PlayerDamagePos;
    public int[] EnemyDamagePos;

    public int PlayerDamage;

    public bool CanMagic;
    public bool MagicCoolDown;

    public int GetMoney;

    private float DamegeBuffTime;
    public int PlayerDamageBuff;

    [SerializeField]
    private Transform[] ALLMagicPos;

    public Transform PlayerPos;

    [SerializeField]
    private Transform gamemanager;

    public int MagicType = 0;

    public int playerPosX;
    public int playerPosY;
    public int playerPosXY;

    public int PlayerMoveCount=2;
    public int PlayerMagicCount=4;
    public int PlayerHPCount = 3;

    public int playerMoveCountMax=2;
    public int playerMagicCountMax=5;
    public int PlayerHPCountMax=3;
    //public int[] playerMagicList;

    public float PlayerMoveAddTime;
    public bool playerMoveCanAdd;

    public float PlayerMagicAddTime;
    public bool playerMagicCanAdd;

    public bool PlayerCanMove;
    public bool PlayerCanMagic;

    public int[] MagicArray;
    public Sprite[] AllMagicCardArray;
    public Sprite[] AllAdvancedMagicCardArray;
    public int[] ChooseMagicCard;

    public int WitchUserMagic;

    GameObject SmokeObject;
    [SerializeField]
    private AudioClip[] music;

    private AudioClip MusicName;

    public bool PlayerCanCantroller = false;

    public bool BattleStop = false;
    public bool EnemyBattleStop = false;
    public bool isBattleEnd = false;

    public bool Win = false;
    public bool Die = false;
    GameObject WinAndLoseUI;
    private GameManager gameManagerScript;

    private MusicManager MusicManager;
    [SerializeField]
    private AudioClip WinAudio;
    [SerializeField]
    private AudioClip LoseAudio;
    // Use this for initialization
    void Awake () {

        MusicManager = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
        if (gameObject.GetComponent<BattleManagerScript>().enabled == true)BattleStart();
        
    }

    public void BattleEnd(bool IsWin) {

        if (IsWin == true)
        {
            //PlayerHPCount = 1;
            WinAndLoseUI = GameObject.FindGameObjectWithTag("WinUI");
            isBattleEnd = true;
            CanMagic = false;
            Win = true;
            WinAndLoseUI.GetComponent<Animator>().SetTrigger("Win");
            MusicManager.PlayMusic(WinAudio);
        }
        else {
            //PlayerHPCount = 1;
            MusicManager.PlayMusic(LoseAudio);
            if (gameManagerScript.ApeironMode == false)
            {
                WinAndLoseUI = GameObject.FindGameObjectWithTag("WinUI");
                isBattleEnd = true;
                CanMagic = false;
                Die = true;
                WinAndLoseUI.GetComponent<Animator>().SetTrigger("Die");

            }
            else if (gameManagerScript.ApeironMode == true)
            {
                WinAndLoseUI = GameObject.FindGameObjectWithTag("WinUI");
                isBattleEnd = true;
                CanMagic = false;
                Win = true;
                WinAndLoseUI.GetComponent<Animator>().SetTrigger("ApeironModeUI");

            }
           

        }




    }

    public void BattleEndToStage()
    {
        if (gamemanager.transform.GetComponent<GameManager>().ApeironMode == true)
        {
            ApeironModeManager ApeironModeManagerscript = GameObject.FindWithTag("ApeironModeManager").GetComponent<ApeironModeManager>();
            ApeironModeManagerscript.UpdateRank();
            gamemanager.transform.GetComponent<GameManager>().ApeironMode = false;

            if (gameManagerScript.ApeironMode == false) PlayerPrefs.SetInt("IsBattleEnd", 1);
            gameManagerScript.PlayerHP = PlayerHPCount;
            gamemanager = GameObject.FindWithTag("GameManager").transform;

            string BattleBackScence =  "StartMenu";
            //print(BattleBackScence);
            float IsBattlePosX;
            float IsBattlePosY;
            IsBattlePosX = 500f;
            IsBattlePosY = 500f;
            PlayerHPCount = 3;
            MusicName = music[2];
            Vector2 playerpos = new Vector2(IsBattlePosX, IsBattlePosY);

            CanMagic = false;
            MagicType = 0;
            PlayerCanCantroller = false;



            StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(BattleBackScence, playerpos, MusicName, false, false));
        }
        else {
            if (gameManagerScript.ApeironMode == false) PlayerPrefs.SetInt("IsBattleEnd", 1);
            gameManagerScript.PlayerHP = PlayerHPCount;
            gamemanager = GameObject.FindWithTag("GameManager").transform;

            string BattleBackScence = PlayerPrefs.GetString("BattleBackScence", "StartMenu");
            //print(BattleBackScence);
            float IsBattlePosX;
            float IsBattlePosY;
            IsBattlePosX = PlayerPrefs.GetFloat("IsBattlePosX", 0);
            IsBattlePosY = PlayerPrefs.GetFloat("IsBattlePosY", 0);
            PlayerHPCount = 3;
            if (PlayerPrefs.GetString("BattleBackAudio", "forest") == "forest") MusicName = music[0];
            if (PlayerPrefs.GetString("BattleBackAudio", "forest") == "cave") MusicName = music[1];
            if (PlayerPrefs.GetString("BattleBackAudio", "forest") == "StartMenu") MusicName = music[2];
            Vector2 playerpos = new Vector2(IsBattlePosX, IsBattlePosY);

            CanMagic = false;
            MagicType = 0;
            PlayerCanCantroller = false;



            StartCoroutine(gamemanager.transform.GetComponent<GameManager>().ChangeScreen(BattleBackScence, playerpos, MusicName, true, false));
        }
        
    }

    public void BattleStart() {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        PlayerPos = GameObject.FindWithTag("playerPos").transform;
        for (int x = 0; x < 9; x++)
        {
            groundPos[x] = GameObject.FindWithTag("BattleType").GetComponent<BattleTypeScript>().groundPos[x];
            EnemygroundPos[x] = GameObject.FindWithTag("BattleType").GetComponent<BattleTypeScript>().EnemygroundPos[x];
        }
        WinAndLoseUI = GameObject.FindGameObjectWithTag("WinUI");

        for (int y = 0; y < PlayerDamagePos.Length; y++) {
            PlayerDamagePos[y] = 0;
            EnemyDamagePos[y] = 0;
        }

        Win = false;
        Die = false;
        PlayerCanCantroller = true;

        MagicCoolDown = false;
        CanMagic = true;

        playerPosX = 2;
        playerPosY = 2;
        MagicType = 0;
        PlayerCanMove = true;
        PlayerCanMagic = true;

        playerMoveCountMax = gameManagerScript.PlayerMAXMove;
        playerMagicCountMax = 5;
        PlayerHPCountMax = gameManagerScript.PlayerMAXHP;

        PlayerMoveCount = gameManagerScript.PlayerMAXMove;
        PlayerMagicCount = 4;
        PlayerHPCount = gameManagerScript.PlayerHP;

        PlayerMoveAddTime = gameManagerScript.PlayerMoveAddTime;
        playerMoveCanAdd = false;

        PlayerMagicAddTime = gameManagerScript.PlayerMagicAddTime;
        playerMagicCanAdd = false;

        RandomALLMagic();

        PlayerDamage = 1;
        PlayerDamageBuff = 0;
        DamegeBuffTime = 0;

        //BattleStop = false;
        //EnemyBattleStop = false;
        isBattleEnd = false;

        StartCoroutine(AddPlayerMoveCount(PlayerMoveAddTime));
        StartCoroutine(AddPlayerMagicCount(PlayerMagicAddTime));
    }

   
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("k"))
        {
            CanMagic = false;
            PlayerCanMove = true;
            MagicType = 0;
        }

        AddPlayerMove();
        AddPlayerMagic();


        if (PlayerDamageBuff != 0 ) LessDamegeBuffTime();


        if (Input.GetKeyDown(KeyCode.Q)) {
            //BattleEnd();
            CreatMagic(0, 01);
        }

        if (PlayerHPCount < 0) {
            PlayerCanMove = false;
            PlayerCanMagic = false;
        }

        if (isBattleEnd == true) {
            if (Input.GetButtonDown("Fire1"))
            {
                if (Win == true) {
                    if (gameManagerScript.ApeironMode == false)
                    {

                        gameManagerScript.PlayerMoney += GetMoney;
                        //WinAndLoseUI.GetComponent<Animator>().SetTrigger("Over");
                        BattleEndToStage();
                    }
                    else {

                        BattleEndToStage();

                    }
                   
                }
                else if (Die == true)
                {                 
                    //WinAndLoseUI.GetComponent<Animator>().SetTrigger("Over");
                }
            }
        }

    }

    public void GetGroundPos(int y,int x) {
        if (y == 1) {
            if (x == 1) playerPosXY = 0;
            if (x == 2) playerPosXY = 1;
            if (x == 3) playerPosXY = 2;
        }else if (y == 2)
        {
            if (x == 1) playerPosXY = 3;
            if (x == 2) playerPosXY = 4;
            if (x == 3) playerPosXY = 5;
        }else if (y == 3)
        {
            if (x == 1) playerPosXY = 6;
            if (x == 2) playerPosXY = 7;
            if (x == 3) playerPosXY = 8;
        }
    }

    void AddPlayerMove() {
        
        if (playerMoveCountMax > PlayerMoveCount && playerMoveCanAdd == true)
        {
            PlayerMoveCount += 1;
            playerMoveCanAdd = false;
            StartCoroutine(AddPlayerMoveCount(PlayerMoveAddTime));
        }

    }

    void AddPlayerHP() {
        if (PlayerHPCountMax > PlayerHPCount)
        {
            PlayerHPCount += 1;
        }

    }


    void AddPlayerMagic()
    {
        if (playerMagicCountMax > PlayerMagicCount && playerMagicCanAdd == true)
        {
            PlayerMagicCount += 1;
            playerMagicCanAdd = false;
            //MagicArray[PlayerMagicCount-1] = Random.Range(1, 3);
            MagicArray[PlayerMagicCount - 1] = RandomMagic();
            StartCoroutine(AddPlayerMagicCount(PlayerMagicAddTime));
        }

    }

    private IEnumerator AddPlayerMoveCount(float x)
    {
        yield return new WaitForSeconds(x);
        playerMoveCanAdd = true;

    }

    private IEnumerator AddPlayerMagicCount(float x)
    {
        yield return new WaitForSeconds(x);
        playerMagicCanAdd = true;

    }

    void RandomALLMagic()
    {
        for(int x =0;x < playerMagicCountMax; x++)
        {
            //MagicArray[x] = Random.Range(1, 3);
            MagicArray[x] = RandomMagic();
        }
        

    }

    private int RandomMagic()
    {
        int playerHaveMagic = Random.Range(1, 5);
        bool HaveMagic = false;
        while (HaveMagic == false) {
            playerHaveMagic = Random.Range(1, 5);
            if (gameManagerScript.PlayerHaveEarthMagic == 1 && playerHaveMagic == 3)
            {
                HaveMagic = true;
            }
            if (gameManagerScript.PlayerHaveMineMagic == 1 && playerHaveMagic == 4)
            {
                HaveMagic = true;
            }
            if(playerHaveMagic == 1 || playerHaveMagic == 2) HaveMagic = true;

        }


        return playerHaveMagic;
    }

    public void CreatMagic(int HowUse,int Magic) {
        if (Magic == 11)
        {
            if (HowUse == 0)
            {
                Instantiate(ALLMagicPos[0], EnemygroundPos[playerPosXY].position, ALLMagicPos[0].rotation);
                StartCoroutine(Damage(false, playerPosXY, 0.3f,1));
            }
            else
            {
                Instantiate(ALLMagicPos[2], PlayerPos.position, ALLMagicPos[2].rotation);
                StareDamegeBuff(30, 1);

            }

        }
        else if (Magic == 21)
        {
            if (HowUse == 0)
            {
                Instantiate(ALLMagicPos[1], EnemygroundPos[playerPosXY].position, ALLMagicPos[1].rotation);
                StartCoroutine(Damage(false, playerPosXY, 0.3f,2));
            }
            else
            {
                Instantiate(ALLMagicPos[3], PlayerPos.position, ALLMagicPos[3].rotation);
                AddPlayerHP();
            }

        }
        else if (Magic == 41)
        {
            if (HowUse == 0)
            {
                Instantiate(ALLMagicPos[4], EnemygroundPos[playerPosXY].position, ALLMagicPos[4].rotation);
                StartCoroutine(Damage(false, playerPosXY, 0.3f,4));
            }
            else
            {
                Instantiate(ALLMagicPos[9], PlayerPos.position, ALLMagicPos[9].rotation);
                if(PlayerMoveCount<5)PlayerMoveCount += 1;
            }

        }
        else if (Magic == 31)
        {
            if (HowUse == 0)
            {
                Instantiate(ALLMagicPos[5], EnemygroundPos[playerPosXY].position, ALLMagicPos[5].rotation);
                StartCoroutine(Damage(false, playerPosXY, 0.3f,3));
            }
            else
            {
                Instantiate(ALLMagicPos[10], PlayerPos.position, ALLMagicPos[10].rotation);
            }

        }
        else if (Magic == 01)
        {
            Instantiate(ALLMagicPos[6], EnemygroundPos[4].position, ALLMagicPos[6].rotation);
            StartCoroutine(Damage(false, 9, 10f, 10));

        }
        else if (Magic == 12)
        {

            StartCoroutine(Fire2MagicPos2());
        }
        else if (Magic == 02)
        {
            Instantiate(ALLMagicPos[11], PlayerPos.position, ALLMagicPos[11].rotation);
            StartCoroutine(Damage(true, playerPosXY, 10f, 11));
            //StartCoroutine(Fire2MagicPos2());
        }
    }

    public void GroundAtk(bool Player, int posXY, int watchAtk , bool Open ,int HowUse)
    {

        if (Player == false)
        {
            if (watchAtk == 11 || watchAtk == 21 || watchAtk == 31 || watchAtk == 41)
            {
                if (HowUse == 0)
                {
                    GroundAtkAnim(EnemygroundPos[playerPosXY].gameObject.GetComponent<Animator>(), Open);


                }
                else if (HowUse == 1)
                {
                    GroundAtkAnim(groundPos[playerPosXY].gameObject.GetComponent<Animator>(), Open);

                }

                //EnemygroundPos[]
            }

            if (watchAtk == 01) {
                GroundAtkAnim(EnemygroundPos[0].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[1].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[2].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[3].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[4].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[5].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[6].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[7].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[8].gameObject.GetComponent<Animator>(), Open);
            }
            if (watchAtk == 12)
            {
                GroundAtkAnim(EnemygroundPos[1].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[3].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[4].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[5].gameObject.GetComponent<Animator>(), Open);
                GroundAtkAnim(EnemygroundPos[7].gameObject.GetComponent<Animator>(), Open);
            }
            if (watchAtk == 02)
            {
                GroundAtkAnim(groundPos[playerPosXY].gameObject.GetComponent<Animator>(), Open);
                
            }
        }
        else {
            if (watchAtk == 0) {//怪物X排攻擊

                GroundAtkAnim(groundPos[posXY].gameObject.GetComponent<Animator>(), Open);
            }

        }

    }

    public IEnumerator Damage(bool Player,int posXY,float time,int AtkType)
    {
        yield return new WaitForSeconds(0.3f);
        if (Player == false) {
            EnemyDamagePos[posXY] = AtkType;
            yield return new WaitForSeconds(time);
            EnemyDamagePos[posXY] = 0;
           
            

        }
        if (Player == true)
        {
            PlayerDamagePos[posXY] = AtkType;
            yield return new WaitForSeconds(time);
            PlayerDamagePos[posXY] = 0;



        }

    }

    private void StareDamegeBuff(int time, int BuffCount) {
       if(BuffCount> PlayerDamageBuff) PlayerDamageBuff = BuffCount;
        DamegeBuffTime = time;
    }

    private void LessDamegeBuffTime() {
        if(DamegeBuffTime>0)
            DamegeBuffTime -= Time.deltaTime;
        else
            PlayerDamageBuff = 0;

    }

    private void GroundAtkAnim(Animator ground , bool Open)
    {
        ground.SetBool("GroundAtk", Open);
    }

    private IEnumerator Fire2MagicPos2() {
        PlayerDamage = 2;
        Instantiate(ALLMagicPos[7], EnemygroundPos[4].position, ALLMagicPos[7].rotation);
        StartCoroutine(Damage(false, 4, 0.3f,1));
        yield return new WaitForSeconds(0.2f);
        Instantiate(ALLMagicPos[8], EnemygroundPos[1].position, ALLMagicPos[8].rotation);
        StartCoroutine(Damage(false, 1, 0.3f,1));
        yield return new WaitForSeconds(0.2f);
        Instantiate(ALLMagicPos[8], EnemygroundPos[3].position, ALLMagicPos[8].rotation);
        StartCoroutine(Damage(false, 3, 0.3f,1));
        yield return new WaitForSeconds(0.2f);
        Instantiate(ALLMagicPos[8], EnemygroundPos[5].position, ALLMagicPos[8].rotation);
        StartCoroutine(Damage(false, 5, 0.3f,1));
        yield return new WaitForSeconds(0.2f);
        Instantiate(ALLMagicPos[8], EnemygroundPos[7].position, ALLMagicPos[8].rotation);
        StartCoroutine(Damage(false, 7, 0.3f,1));
        PlayerDamage = 1;
    }

    public IEnumerator CanMagicCoolDown()
    {
        MagicCoolDown = true;
        yield return new WaitForSeconds(0.3f);
        MagicCoolDown = false;
    }
}
