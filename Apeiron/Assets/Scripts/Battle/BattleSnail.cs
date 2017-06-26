using UnityEngine;
using System.Collections;

public class BattleSnail : MonoBehaviour {
    private BattleManagerScript BattleManager;
    private GameManager gameManagerScript;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip SnailDie;

    [SerializeField]
    private SpriteRenderer QuestionMark;

    Animator anim;
    private int HPCount;
    [SerializeField]
    private int DamageCount;

    private bool CanDamage;
    private bool SnailWeek;

    private bool Die;

    private bool AtkTimer;
    private bool HideTimer;

    private float HideTimer_f;
    private int HideTimer_i;

    [SerializeField]
    private Transform SnailAtk;

    private bool SnailIsHide;
    private bool StartBattle;

    private int atk;
    private float WeekTime;
    [SerializeField]
    private int EnemyPosX;
    [SerializeField]
    private int EnemyPosY;
    [SerializeField]
    private int EnemyPosXY;
    // Use this for initialization
    void Awake() {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        HPCount = 5;
        if (gameManagerScript.ApeironMode == true) {
            HPCount += gameManagerScript.round * 1;

        }
        DamageCount = 0;
        EnemyPosX = 2;
        EnemyPosY = 2;
        EnemyPosXY = 4;
        CanDamage = true;
        SnailIsHide = false;
        AtkTimer = true;
        HideTimer = true;
        SnailWeek = false;
        StartBattle = false;
        Die = false;
        HideTimer_f = 0;
        WeekTime = 0;
    }

   

    void Start () {
        anim = GetComponent<Animator>();
       
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        

        if (SnailWeek == true)
        {
            WeekTime += Time.fixedDeltaTime;
            if (WeekTime >= 3f) {
                QuestionMark.enabled = false;
                SnailWeek = false;
            }
        }
        else if(WeekTime !=0)
        {
            WeekTime = 0;
        }

        
        EnemyGetGroundPos(EnemyPosY, EnemyPosX);
        
        if (HPCount <= 0 && Die==false) {
            Die = true;
            audioSource.PlayOneShot(SnailDie);
            //BattleManager.GetMoney = 1000;
            //BattleManager.BattleEnd(true);

        }
        if (BattleManager.BattleStop == false && BattleManager.EnemyBattleStop == false) {
            transform.localPosition = BattleManager.EnemygroundPos[EnemyPosXY].localPosition;
            if (StartBattle == false) {
                anim.SetTrigger("StartBattle");
                StartBattle = true;
            }
            if (DamageCount >= 5)
            {
                anim.SetBool("IsHide", true);
                HideTimer_f = 0;
                SnailIsHide = true;
                CanDamage = false;
                DamageCount = 0;
            }

            if (SnailIsHide == false && CanDamage == true && AtkTimer && SnailWeek == false)
            {
                StartCoroutine(SnailAtKKKKKK());
                AtkTimer = false;
            }

            if (SnailIsHide == false && CanDamage == true && HideTimer && SnailWeek == false)
            {
                StartCoroutine(SnailHideeeeeee());
                HideTimer = false;
            }

            
            if (BattleManager.EnemyDamagePos[EnemyPosXY] != 0 && CanDamage == true)
            {
                anim.SetBool("TakeDamageTrue", true);

            }
            else
            {
                anim.SetBool("TakeDamageTrue", false);
            }

            if (SnailIsHide)
            {
                HideTimer_f += Time.fixedDeltaTime;

                if (HideTimer_f >= 3)
                {
                    SnailIsHide = false;
                    anim.SetBool("IsHide", false);
                    CanDamage = true;
                    SnailWeek = true;
                    QuestionMark.enabled = true;
                }
                if (BattleManager.EnemyDamagePos[EnemyPosXY] !=0 )
                {
                    anim.SetBool("IsAtk", true);
                    anim.SetBool("IsHide", false);
                    SnailIsHide = false;
                }

            }


        }
        

    }

    private IEnumerator SnailAtKKKKKK()
    {
        yield return new WaitForSeconds(1);
        AtkTimer = true;
        if (Random.Range(0, 100) >= 95 && SnailIsHide == false) {
            anim.SetBool("IsAtk", true);
            CanDamage = false;
        }
    }

    private IEnumerator SnailHideeeeeee()
    {
        yield return new WaitForSeconds(1);
        HideTimer = true;
        if (Random.Range(0, 100) >= 95 && SnailIsHide == false)
        {
            anim.SetBool("IsHide", true);
            HideTimer_f = 0;
            SnailIsHide = true;
            CanDamage = false;
        }
        else {
            if (SnailIsHide == false)
                DamageCount += 1;
        }
    }

    public void SnailTakeDamage() {
        HPCount -= (BattleManager.PlayerDamage + BattleManager.PlayerDamageBuff);
        SnailWeek = false;
        QuestionMark.enabled = false;
        if (HPCount <= 0)
            anim.SetBool("die", true);
        //DamageCount += (BattleManager.PlayerDamage + BattleManager.PlayerDamageBuff);
    }

    public void SnailStartAtk() {
        atk = Random.Range(0, 3);
        atk = 2 + (atk * 3);
        BattleManager.GroundAtk(true, atk, 0, true, 0);
        BattleManager.GroundAtk(true, atk-1, 0, true, 0);
        BattleManager.GroundAtk(true, atk-2, 0, true, 0);
    }

    public void Atk() {
        BattleManager.GroundAtk(true, atk, 0, false, 0);
        BattleManager.GroundAtk(true, atk - 1, 0, false, 0);
        BattleManager.GroundAtk(true, atk - 2, 0, false, 0);
        Instantiate(SnailAtk, BattleManager.groundPos[atk].position, SnailAtk.rotation);
        StartCoroutine(BattleManager.Damage(true, atk, 0.3f,1));
        StartCoroutine(BattleManager.Damage(true, atk - 1, 0.3f,1));
        StartCoroutine(BattleManager.Damage(true, atk - 2, 0.3f,1));
        anim.SetBool("IsAtk", false);
        CanDamage = true;
        EnemyPosX = Random.Range(1, 4);
        EnemyPosY = Random.Range(1, 4);
    }

    void EnemyGetGroundPos(int y, int x)
    {
        if (y == 1)
        {
            if (x == 1) EnemyPosXY=0;
            if (x == 2) EnemyPosXY =1;
            if (x == 3) EnemyPosXY =2;
        }
        else if (y == 2)
        {
            if (x == 1) EnemyPosXY =3;
            if (x == 2) EnemyPosXY =4;
            if (x == 3) EnemyPosXY =5;
        }
        else if (y == 3)
        {
            if (x == 1) EnemyPosXY =6;
            if (x == 2) EnemyPosXY =7;
            if (x == 3) EnemyPosXY =8;
        }
    }
    public void SnailDieeeeeee()
    {
        if (gameManagerScript.ApeironMode == false)
        {

            BattleManager.GetMoney = 1000;
            BattleManager.BattleEnd(true);

        }
        else {
            ApeironCreatMobScript A = GameObject.FindWithTag("ApeironModeManager").GetComponent<ApeironCreatMobScript>();
            gameManagerScript.Score += gameManagerScript.round * 100;
            Destroy(this.gameObject, 0.5f);
            A.CreatMob();


        }
       
    }
}
