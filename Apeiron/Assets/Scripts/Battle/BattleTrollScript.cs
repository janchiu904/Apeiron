using UnityEngine;
using System.Collections;

public class BattleTrollScript : MonoBehaviour {
    private BattleManagerScript BattleManager;
    private GameManager gameManagerScript;
    Animator anim;
    private AudioSource audioSource;
    private bool CanDamage;
    [SerializeField]
    private int EnemyPosX;
    [SerializeField]
    private int EnemyPosY;
    [SerializeField]
    private int EnemyPosXY;

    private int HPCount;
    private int EnemyType;

    private bool AtkTimer;
    private bool WalkTimer;

    private bool TrollWalk;

    private int AtkCount=0;
    private int WalkCount=0;

    private bool TrollDie;
    private bool StartBattle;

    private int AtkPosXY;
    [SerializeField]
    private Transform TrollAtk;

    [SerializeField]
    private SpriteRenderer QuestionMark;
    [SerializeField]
    private SpriteRenderer TrollSprite;
    [SerializeField]
    private Transform TrollTransform;
    [SerializeField]
    private AudioClip Die;
    [SerializeField]
    private AudioClip Walk;
    
    private float TrollAtkTimer;
    // Use this for initialization
    void Awake()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        
        HPCount = 30;
        if (gameManagerScript.ApeironMode == true)
        {
            HPCount += gameManagerScript.round * 2;

        }
        EnemyType = 0;
        EnemyPosX = 2;
        EnemyPosY = 2;
        EnemyPosXY = 4;
        CanDamage = true;
        AtkTimer = true;
        WalkTimer = true;
        TrollWalk = false;
        TrollDie = false;
        TrollAtkTimer = 0;
        StartBattle = false;
    }
    void Start () {
        anim = GetComponent<Animator>();
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        EnemyGetGroundPos(EnemyPosY, EnemyPosX);
        if (TrollWalk == true)
        {
            audioSource.clip = Walk;
            if(audioSource.isPlaying == false) audioSource.Play();
            TrolWalkEnd();
        }
        else {
            TrollSprite.flipX = false;
            TrollTransform.position = new Vector2(0.43f, TrollTransform.position.y);
        }
        if (HPCount <= 0 && TrollDie == false)
        {
            TrollDie = true;

            audioSource.PlayOneShot(Die);
            //BattleManager.BattleEnd(true);

        }

        if (BattleManager.BattleStop == false && BattleManager.EnemyBattleStop == false)
        {
            if (StartBattle == false)
            {
                anim.SetTrigger("Start");
                StartBattle = true;
            }

            if (EnemyType != 1 || EnemyType != 2 || EnemyType != 4 || EnemyType != 5) {
                if (TrollWalk == false) transform.localPosition = BattleManager.EnemygroundPos[EnemyPosXY].localPosition;
                if (BattleManager.EnemyDamagePos[9] == 10 && TrollWalk == false)
                {
                    CanDamage = true;
                    QuestionMark.enabled = true;
                }
                else if (TrollDie == false)
                {
                    CanDamage = false;
                    QuestionMark.enabled = false;
                    if (TrollWalk == false && AtkTimer)
                    {
                        StartCoroutine(TrollAtKKKKKK());
                        AtkTimer = false;
                    }
                    if (TrollWalk == false && WalkTimer)
                    {
                        StartCoroutine(TrollWalkkkkkk());
                        WalkTimer = false;
                    }
                }
                if (CanDamage == true)
                {
                    if (BattleManager.EnemyDamagePos[EnemyPosXY] != 0 || BattleManager.EnemyDamagePos[EnemyPosXY + 1] != 0)
                    {
                        anim.SetBool("Demage", true);

                    }
                    else
                    {
                        anim.SetBool("Demage", false);
                    }

                }
                else
                {
                    anim.SetBool("Demage", false);
                }
                if (EnemyType == 1 && CanDamage == false)
                {
                    anim.SetBool("Omgeye", true);
                    EnemyType += 1;
                    StartCoroutine(TrollOmgeyeAtKKKKKK(5f));
                }
                if (EnemyType == 4 && CanDamage == false)
                {
                    anim.SetBool("Omgeye", true);
                    EnemyType += 1;
                    StartCoroutine(TrollOmgeyeAtKKKKKK(10f));
                }

            }
            

        }
    }

    private IEnumerator TrollOmgeyeAtKKKKKK(float Time)
    {
        while (TrollAtkTimer < Time)
        {
            StartCoroutine(TrollOmgeyeAtKKKKKKCreat());

            //TrollAtkTimer += 0.3f;
            yield return new WaitForSeconds(0.5f);
            TrollAtkTimer += 0.5f;
        }
        anim.SetBool("Omgeye", false);
        EnemyType += 1;
    }

    private IEnumerator TrollOmgeyeAtKKKKKKCreat()
    {
        int atk = Random.Range(0, 9);
        BattleManager.GroundAtk(true, atk, 0, true, 0);
        yield return new WaitForSeconds(0.3f);
        BattleManager.GroundAtk(true, atk, 0, false, 0);
        Instantiate(TrollAtk, BattleManager.groundPos[atk].position, TrollAtk.rotation);
        StartCoroutine(BattleManager.Damage(true, atk, 0.3f, 1));

    }


    public void SnailTakeDamage()
    {
        HPCount -= (BattleManager.PlayerDamage + BattleManager.PlayerDamageBuff);
        if (HPCount <= 15) EnemyType = 1;
        if (HPCount <= 5) EnemyType = 4;
        if (HPCount <= 0) 
            anim.SetBool("Die", true);
        //DamageCount += (BattleManager.PlayerDamage + BattleManager.PlayerDamageBuff);
    }

    private IEnumerator TrollAtKKKKKK()
    {
        yield return new WaitForSeconds(2);
        AtkTimer = true;
        if (Random.Range(0, 100) <= 25 && TrollWalk == false && CanDamage == false)
        {
            anim.SetTrigger("Atk");
            AtkPosXY = BattleManager.playerPosXY;
            BattleManager.GroundAtk(true, AtkPosXY, 0, true, 0);
        }
        else
        {
            if (TrollWalk == false && CanDamage == false && AtkCount < 4)
            {
                AtkCount += 1;
            }
            else if(CanDamage == false && TrollWalk == false) {
                anim.SetTrigger("Atk");
                AtkPosXY = BattleManager.playerPosXY;
                BattleManager.GroundAtk(true, AtkPosXY, 0, true, 0);
                AtkCount = 0;
            }
                
        }
    }

    public void TrollCreatAtk() {
        BattleManager.GroundAtk(true, AtkPosXY, 0, false, 0);
        Instantiate(TrollAtk, BattleManager.groundPos[AtkPosXY].position, TrollAtk.rotation);
        StartCoroutine(BattleManager.Damage(true, AtkPosXY, 0.3f, 1));
    }

    private IEnumerator TrollWalkkkkkk()
    {
        yield return new WaitForSeconds(5);
        WalkTimer = true;
        if (Random.Range(0, 100) <= 50 && TrollWalk == false && CanDamage == false)
        {
            anim.SetBool("Walk",true);
            TrollWalk = true;
            TrollHowWalk();
        }
        else
        {
            if (TrollWalk == false && CanDamage == false && WalkCount < 2)
            {
                WalkCount += 1;
            }
            else if (CanDamage == false)
            {
                anim.SetBool("Walk", true);
                WalkCount = 0;
                TrollWalk = true;
                TrollHowWalk();
            }

        }
    }


    void TrollHowWalk() {
        
        if (Random.Range(0, 2) <= 0)
        {
            if (Random.Range(0, 2) <= 0)
            {
                if (EnemyPosX != 2)
                {
                    EnemyPosX += 1;
                    TrollSprite.flipX = true;
                    TrollTransform.position = new Vector2(TrollTransform.position.x+0.66f, TrollTransform.position.y);
                }
                else {
                    EnemyPosX -= 1;
                }
                    
            }
            else
            {
                if (EnemyPosX != 1) {
                    EnemyPosX -= 1;
                }else {
                    EnemyPosX += 1;
                    TrollSprite.flipX = true;
                    TrollTransform.position = new Vector2(TrollTransform.position.x + 0.66f, TrollTransform.position.y);
                }             
            }
        }
        else {
            if (Random.Range(0, 2) <= 0)
            {
                if (EnemyPosY != 3)
                {
                    EnemyPosY += 1;
                    
                }
                else {
                    EnemyPosY -= 1;
                    TrollSprite.flipX = true;
                    TrollTransform.position = new Vector2(TrollTransform.position.x + 0.66f, TrollTransform.position.y);
                }              
            }
            else
            {
                if (EnemyPosY != 1)
                {
                    EnemyPosY -= 1;
                    TrollSprite.flipX = true;
                    TrollTransform.position = new Vector2(TrollTransform.position.x + 0.66f, TrollTransform.position.y);
                }
                else {
                    EnemyPosY += 1;
                    
                }
                    
            }


        }
        
    }
    void TrolWalkEnd() {
        EnemyGetGroundPos(EnemyPosY, EnemyPosX);
        
        Vector2 pos1 = gameObject.transform.localPosition;
        Vector2 pos2 = BattleManager.EnemygroundPos[EnemyPosXY].localPosition;

        // 移動速度
        float speed = 0.02f;

        // 目標位置 - 玩家位置 = 玩家到目標點之間的方向向量
        Vector2 directionVector = pos2 - pos1;

        // 取得距離長度, 這邊要將方向向量調整到跟移動速度一樣, 
        // 最後的 directionVector 為我們需要的方向向量, 可以直接將該值累加到玩家位置上
        float distance = directionVector.magnitude;
        directionVector *= (distance == 0.0f ? 0.0f : speed / distance);
        gameObject.transform.localPosition = pos1+directionVector;
        if (distance <= 0.01f) {
            TrollWalk = false;
            audioSource.Stop();
            anim.SetBool("Walk", false);
        }
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
    public void TrollDieeeeeee()
    {
        if (gameManagerScript.ApeironMode == false) {
            BattleManager.GetMoney = 2000;
            BattleManager.BattleEnd(true);
        }
        else
        {
            ApeironCreatMobScript A = GameObject.FindWithTag("ApeironModeManager").GetComponent<ApeironCreatMobScript>();
            gameManagerScript.Score += gameManagerScript.round * 200;
            Destroy(this.gameObject, 0.5f);
            A.CreatMob();


        }

    }
}
