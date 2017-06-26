using UnityEngine;
using System.Collections;

public class BattleCowScript : MonoBehaviour {
    private BattleManagerScript BattleManager;
    private GameManager gameManagerScript;
    private AudioSource audioSource;

    Animator anim;

    [SerializeField]
    private Transform CowAtkPos;
    [SerializeField]
    private Transform LittleCowAtkPos;

    private bool CanDamage;
    private bool CowWeek;
    [SerializeField]
    private int EnemyPosX;
    [SerializeField]
    private int EnemyPosY;
    [SerializeField]
    private int EnemyPosXY;

    [SerializeField]
    private int HPCount;
    [SerializeField]
    private int EnemyType;

    private bool AtkTimer;
    private bool ShoutTimer;

    private int AtkCount = 0;
    private int ShoutCount = 0;

    private int AtkPosXY;
    private bool CowShout;
    private bool CowAtk;
    private bool CowDie;
    [SerializeField]
    private AudioClip Die;
    [SerializeField]
    private AudioClip Week;
    [SerializeField]
    private AudioClip Shout;
    [SerializeField]
    private AudioClip PreAtk;
    [SerializeField]
    private AudioClip Bump;

    private float CowShoutTimer;
    private float LittleCowATKTimer;
    private bool StartBattle;

    private float WeekTime;

    private bool DamageCoolDown;

    
    // Use this for initialization
    void Awake()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        if (gameManagerScript.ApeironMode == true)
        {
            HPCount += gameManagerScript.round * 10;

        }
        HPCount = 300;
        EnemyType = 0;
        EnemyPosX = 2;
        EnemyPosY = 2;
        EnemyPosXY = 4;
        CanDamage = true;
        AtkTimer = true;
        ShoutTimer = true;
        CowShout = false;
        CowDie = false;
        CowShoutTimer = 0;
        LittleCowATKTimer = 0;
        StartBattle = false;
        WeekTime = 0;
        CowWeek = false;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyGetGroundPos(EnemyPosY, EnemyPosX);
        if (CowWeek == true)
        {
           //audioSource.PlayOneShot(Week);
            anim.SetBool("CowWeek", true);
            WeekTime += Time.fixedDeltaTime;
            if (WeekTime >= 7f)
            {
                anim.SetBool("CowWeek", false);
                CowWeek = false;
            }

        }
        else if (WeekTime != 0)
        {
            WeekTime = 0;
        }

        if (HPCount <= 200 && EnemyType == 0) {
            StartCoroutine(LittleCowATK(10f));
            EnemyType = 1;

        }
        if (HPCount <= 100 && EnemyType == 1)
        {
            StartCoroutine(LittleCowATK(20f));
            EnemyType = 2;

        }
        if (HPCount <= 0 && CowDie == false)
        {
            anim.SetBool("Die", true);
            CowDie = true;
            //audioSource.PlayOneShot(SnailDie);
            

        }
        if (BattleManager.BattleStop == false && BattleManager.EnemyBattleStop == false)
        {
            transform.localPosition = BattleManager.EnemygroundPos[EnemyPosXY].localPosition;
            if (StartBattle == false)
            {
                anim.SetTrigger("StartBattle");
                StartBattle = true;
            }
             if (CowDie == false)
            {
                if (CowAtk == false && AtkTimer)
                {
                    StartCoroutine(CowAtKKKKKK());
                    AtkTimer = false;
                }
                if (CowShout == false && ShoutTimer)
                {
                    StartCoroutine(CowNourShouttttt());
                    ShoutTimer = false;
                }


            }
            if (CanDamage == true) {
                if (BattleManager.EnemyDamagePos[EnemyPosXY] != 0 || BattleManager.EnemyDamagePos[EnemyPosXY + 1] != 0)
                {
                    if (DamageCoolDown == false)
                    {
                        if (CowWeek == true)
                        {
                            DamageCoolDown = true;
                            anim.SetBool("CowWeek", false);
                            anim.SetBool("Demage", true);
                            CowWeek = false;
                            HPCount -= (BattleManager.PlayerDamage + BattleManager.PlayerDamageBuff) * 50;
                        }
                        else
                        {
                            DamageCoolDown = true;
                            HPCount -= (BattleManager.PlayerDamage + BattleManager.PlayerDamageBuff);
                            anim.SetBool("Demage", true);

                        }
                    }                   
                }
                else {
                    anim.SetBool("Demage", false);
                    DamageCoolDown = false;
                }
                

            }
        }
    }

    private IEnumerator LittleCowATK(float Time)
    {
        while (LittleCowATKTimer < Time)
        {
            CanDamage = false;
            StartCoroutine(LittleCowATKCreat());
            anim.SetBool("CowShout", true);
            yield return new WaitForSeconds(0.5f);
            LittleCowATKTimer += 0.5f;
            
        }
        anim.SetBool("CowShout", false);
        CowShout = false;
        CanDamage = true;
    }

    private IEnumerator LittleCowATKCreat()
    {
        int atk = Random.Range(0, 3);
        atk = 2 + (atk * 3);
        BattleManager.GroundAtk(true, atk, 0, true, 0);
        BattleManager.GroundAtk(true, atk - 1, 0, true, 0);
        BattleManager.GroundAtk(true, atk - 2, 0, true, 0);
        yield return new WaitForSeconds(0.3f);
        BattleManager.GroundAtk(true, atk, 0, false, 0);
        BattleManager.GroundAtk(true, atk - 1, 0, false, 0);
        BattleManager.GroundAtk(true, atk - 2, 0, false, 0);
        Instantiate(LittleCowAtkPos, BattleManager.groundPos[atk].position, CowAtkPos.rotation);
        if (BattleManager.PlayerDamagePos[atk] == 11 || BattleManager.PlayerDamagePos[atk - 1] == 11 || BattleManager.PlayerDamagePos[atk - 2] == 11)
        {
            
        }
        else
        {
            StartCoroutine(BattleManager.Damage(true, atk, 0.3f, 1));
            StartCoroutine(BattleManager.Damage(true, atk - 1, 0.3f, 1));
            StartCoroutine(BattleManager.Damage(true, atk - 2, 0.3f, 1));

        }

    }

    private IEnumerator CowShoutttttt(float Time)
    {
        while (CowShoutTimer < Time)
        {
            CanDamage = false;
            anim.SetBool("CowShout", true);
            yield return new WaitForSeconds(0.5f);
            
            CowShoutTimer += 0.5f;
        }
        anim.SetBool("CowShout", false);
        CowShout = false;
        CanDamage = true;
    }
    private IEnumerator CowNourShouttttt()
    {
        yield return new WaitForSeconds(5);
        ShoutTimer = true;
        if (Random.Range(0, 100) <= 50 && CowShout == false && CowAtk == false && CowWeek == false)
        {
            CowShout = true;
            StartCoroutine(CowShoutttttt(2f));
            //if (BattleManager.PlayerMagicCount > 0) BattleManager.PlayerMagicCount -= 1;
            //BattleManager.MagicType = 0;
            //BattleManager.CanMagic = false;
           // BattleManager.PlayerCanMove = true;
           // BattleManager.MagicType = 0;
        }
        else
        {
            if (CowShout == false && ShoutCount < 2)
            {
                ShoutCount += 1;
            }
            else if (CowWeek == false && CowShout == false && CowAtk == false)
            {
                ShoutCount = 0;
                CowShout = true;
                StartCoroutine(CowShoutttttt(2f));
                //if (BattleManager.PlayerMagicCount > 0) BattleManager.PlayerMagicCount -= 1;
                //BattleManager.MagicType = 0;
               // BattleManager.CanMagic = false;
              //  BattleManager.PlayerCanMove = true;
               // BattleManager.MagicType = 0;
            }

        }
    }


    private IEnumerator CowAtKKKKKK()
    {
        yield return new WaitForSeconds(2);
        AtkTimer = true;
        if (Random.Range(0, 100) <= 25 && CowShout == false && CowAtk == false && CowWeek == false)
        {
            anim.SetBool("CowAtk", true);
            //AtkPosXY = BattleManager.playerPosXY;
            //BattleManager.GroundAtk(true, AtkPosXY, 0, true, 0);
            AtkCount = 0;
        }
        else
        {
            if (CowShout == false && AtkCount < 4)
            {
                AtkCount += 1;
            }
            else if (CowShout == false && CowAtk == false && CowWeek == false)
            {
                anim.SetBool("CowAtk", true);
                AtkCount = 0;
            }

        }
    }

    public void CowStartAtk()
    {
        CanDamage = false;
        CowAtk = true;
        AtkPosXY = Random.Range(0, 3);
        AtkPosXY = 2 + (AtkPosXY * 3);
        BattleManager.GroundAtk(true, AtkPosXY, 0, true, 0);
        BattleManager.GroundAtk(true, AtkPosXY - 1, 0, true, 0);
        BattleManager.GroundAtk(true, AtkPosXY - 2, 0, true, 0);
    }
    public void PreAtkAudio(){
        
        audioSource.PlayOneShot(PreAtk);
        }
    public void WeekAudio()
    {

        if(WeekTime == 0) audioSource.PlayOneShot(Week);
    }
    public void ShotAudio()
    {
        audioSource.PlayOneShot(Shout);
    }
    public void Atk()
    {
        BattleManager.GroundAtk(true, AtkPosXY, 0, false, 0);
        BattleManager.GroundAtk(true, AtkPosXY - 1, 0, false, 0);
        BattleManager.GroundAtk(true, AtkPosXY - 2, 0, false, 0);
        Instantiate(CowAtkPos, BattleManager.groundPos[AtkPosXY].position, CowAtkPos.rotation);
        if (BattleManager.PlayerDamagePos[AtkPosXY] == 11 || BattleManager.PlayerDamagePos[AtkPosXY -1] == 11 || BattleManager.PlayerDamagePos[AtkPosXY - 2] == 11) {
            CowWeek = true;
            audioSource.PlayOneShot(Bump);
        }
        else{
            StartCoroutine(BattleManager.Damage(true, AtkPosXY, 0.3f, 1));
            StartCoroutine(BattleManager.Damage(true, AtkPosXY - 1, 0.3f, 1));
            StartCoroutine(BattleManager.Damage(true, AtkPosXY - 2, 0.3f, 1));

        }
        
        anim.SetBool("CowAtk", false);
        CowAtk = false;
        CanDamage = true;
        EnemyPosX = Random.Range(1, 3);
        EnemyPosY = Random.Range(1, 4);
    }

    public void CowIdle() {

        anim.SetBool("Demage", false);
        DamageCoolDown = false;
    }

    void EnemyGetGroundPos(int y, int x)
    {
        if (y == 1)
        {
            if (x == 1) EnemyPosXY = 0;
            if (x == 2) EnemyPosXY = 1;
            if (x == 3) EnemyPosXY = 2;
        }
        else if (y == 2)
        {
            if (x == 1) EnemyPosXY = 3;
            if (x == 2) EnemyPosXY = 4;
            if (x == 3) EnemyPosXY = 5;
        }
        else if (y == 3)
        {
            if (x == 1) EnemyPosXY = 6;
            if (x == 2) EnemyPosXY = 7;
            if (x == 3) EnemyPosXY = 8;
        }
    }

    public void DieAudio()
    {
        audioSource.PlayOneShot(Die);
    }

    public void CowDieeeeeee() {
        
        if (gameManagerScript.ApeironMode == false)
        {
            BattleManager.GetMoney = 3000;
            BattleManager.BattleEnd(true);
        }
        else {

            ApeironCreatMobScript A = GameObject.FindWithTag("ApeironModeManager").GetComponent<ApeironCreatMobScript>();
            gameManagerScript.Score += gameManagerScript.round * 300;
            Destroy(this.gameObject, 0.5f);
            A.CreatMob();
        }
            
    }
}
