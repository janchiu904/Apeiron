using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoreUIScript : MonoBehaviour {
    private GameManager gameManagerScript;
    private Transform player;
    [SerializeField]
    private Transform UIManager;
    [SerializeField]
    private Image[] StoreCardAndAbilityUISelect;
    [SerializeField]
    private GameObject CardUI;
    [SerializeField]
    private Image[] CardUISelect;
    [SerializeField]
    private int[] WitchCard;
    [SerializeField]
    private Sprite[] StoreCardSprite;
    [SerializeField]
    private Image[] StoreCardImage;
    [SerializeField]
    private GameObject AbilityUI;
    private int StoreCardAndAbilityUISelectbatten = 1;
    [SerializeField]
    private int StoreCardUISelectbatten = 1;
    [SerializeField]
    private int StoreType = 0;

    private int CardAndAbilitybatten = 1;

    [SerializeField]
    private GameObject StoreUIConfirm;
    [SerializeField]
    private Text StoreUIConfirmText;
    [SerializeField]
    private Image[] StoreUIConfirmSelect;
    private int StoreUIConfirmbatten = 1;

    [SerializeField]
    private AudioClip music;
    [SerializeField]
    private AudioClip musicMoney;
    private AudioSource audioSource;

    [SerializeField]
    private Image[] AbilityUISelect;
    [SerializeField]
    private Image[] MAXHP;
    [SerializeField]
    private Image[] MAXMOVE;
    [SerializeField]
    private Image[] MAXATK;
    [SerializeField]
    private Sprite[] KEYAddLess;
    private int Abilitybatten = 1;
    // Use this for initialization
    void Awake() {
        audioSource = GetComponent<AudioSource>();
        UIManager = GameObject.FindWithTag("UIManager").transform;
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        CardAndAbilitySelectEnabled();
        CardSelectEnabled();
        StartCoroutine(StartStoreUI());
    }
	
	// Update is called once per frame
	void Update () {
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        if (StoreCardAndAbilityUISelectbatten == 1)
        {
            StoreUICard();
        }
        if (StoreCardAndAbilityUISelectbatten == 2)
        {
            StoreUIAbility();
        }
        StoreUIConfirmEnabled();
        CardSelectEnabled();
        if (StoreType == 3) {
            StoreUIConfirm.SetActive(true);
            if (StoreCardUISelectbatten == 1)
            {
                StoreUIConfirmText.text = "基礎元素「土」，對指定距離造成1點土屬性傷害。(200元)";

            }
            else if (StoreCardUISelectbatten == 2)
            {
                StoreUIConfirmText.text = "基礎元素「雷」，對指定距離造成1點雷屬性傷害。(200元)";
            }
        }
        if (StoreType == 5)
        {

            StoreUIConfirm.SetActive(true);
            StoreUIConfirmEnabled();
            if (Abilitybatten == 1)
            {
                StoreUIConfirmText.text = "擴增HP上限1點。(200元)";

            }
            else if (Abilitybatten == 2)
            {
                StoreUIConfirmText.text = "擴增移動步數上限1點。(200元)";
            }
            else if (Abilitybatten == 3)
            {
                StoreUIConfirmText.text = "減少卡牌回復速度0.2秒。(200元)";
            }
        }
        if (Input.anyKeyDown)
        {
            if (StoreType == 0) {
                StartCoroutine(StartStoreUI());
            }
            if (StoreType == 1)
            {

                float CardAndAbilityMove = 0;
                CardAndAbilityMove = Input.GetAxisRaw("Horizontal");
                if (CardAndAbilityMove != 0)
                {
                    audioSource.PlayOneShot(music);
                    StoreCardAndAbilityUISelectbatten += (int)CardAndAbilityMove;
                    if (StoreCardAndAbilityUISelectbatten > StoreCardAndAbilityUISelect.Length) StoreCardAndAbilityUISelectbatten = 1;
                    if (StoreCardAndAbilityUISelectbatten < 1) StoreCardAndAbilityUISelectbatten = StoreCardAndAbilityUISelect.Length;

                    CardAndAbilitySelectEnabled();
                }

                if (StoreCardAndAbilityUISelectbatten == 1)
                {
                    StoreUICard();
                    // CardUI.enabled = true;
                    //AbilityUI.enabled = false;
                    CardUI.SetActive(true);
                    AbilityUI.SetActive(false);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        StoreType = 2;

                    }
                }
                else if (StoreCardAndAbilityUISelectbatten == 2)
                {
                    //CardUI.enabled = false;
                    //AbilityUI.enabled = true;
                    CardUI.SetActive(false);
                    AbilityUI.SetActive(true);
                    if (Input.GetButtonDown("Fire1"))
                    {
                        StoreType = 4;

                    }
                }
                else if (StoreCardAndAbilityUISelectbatten == 3)
                {

                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
                        if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));
                        StoreType = 0;
                        StoreCardAndAbilityUISelectbatten = 1;
                        UIManager.transform.GetComponent<UIManagerScript>().GoBack();

                    }
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    if (GameObject.FindWithTag("player").transform != null) player = GameObject.FindWithTag("player").transform;
                    if (player != null) StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));
                    StoreType = 0;
                    StoreCardAndAbilityUISelectbatten = 1;
                    UIManager.transform.GetComponent<UIManagerScript>().GoBack();

                }

            }
            else if (StoreType == 3)
            {
                StoreUIConfirmEnabled();
                StoreUIConfirm.SetActive(true);
                float ConfirmMove = 0;
                ConfirmMove = Input.GetAxisRaw("Horizontal");
                if (ConfirmMove != 0)
                {
                    audioSource.PlayOneShot(music);
                    StoreUIConfirmbatten += (int)ConfirmMove;
                    if (StoreUIConfirmbatten > StoreUIConfirmSelect.Length) StoreUIConfirmbatten = 1;
                    if (StoreUIConfirmbatten < 1) StoreUIConfirmbatten = StoreUIConfirmSelect.Length;
                    StoreUIConfirmEnabled();
                }



                if (StoreUIConfirmbatten == 1)
                {
                    if (Input.GetButtonDown("Fire1") && gameManagerScript.PlayerMoney >= 100)
                    {
                        audioSource.PlayOneShot(musicMoney);
                        gameManagerScript.PlayerMoney -= 100;
                        if (StoreCardUISelectbatten == 1) gameManagerScript.PlayerHaveEarthMagic = 1;
                        if (StoreCardUISelectbatten == 2) gameManagerScript.PlayerHaveMineMagic = 1;
                        StoreType = 2;
                        StoreUIConfirmbatten = 1;
                        StoreUIConfirm.SetActive(false);
                    }
                }
                else if (StoreUIConfirmbatten == 2) {
                    if (Input.GetButtonDown("Fire1")) {
                        StoreType = 2;
                        StoreUIConfirmbatten = 1;
                        StoreUIConfirm.SetActive(false);
                    }
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    StoreType = 2;
                    StoreUIConfirmbatten = 1;
                    StoreUIConfirm.SetActive(false);
                }
            }
            else if (StoreType == 2)
            {
                CardSelectEnabled();
                float CardMove = 0;
                CardMove = Input.GetAxisRaw("Horizontal");
                if (CardMove != 0)
                {
                    audioSource.PlayOneShot(music);
                    StoreCardUISelectbatten += (int)CardMove;
                    if (StoreCardUISelectbatten > WitchCard.Length) StoreCardUISelectbatten = 1;
                    if (StoreCardUISelectbatten < 1) StoreCardUISelectbatten = WitchCard.Length;
                    CardSelectEnabled();

                }
                if (Input.GetButtonDown("Fire2"))
                {
                    StoreType = 1;
                    CardSelectEnabled();

                }
                if (Input.GetButtonDown("Fire1"))
                {
                    if (StoreCardUISelectbatten == 1 && gameManagerScript.PlayerHaveEarthMagic != 1) StoreType = 3;
                    if (StoreCardUISelectbatten == 2 && gameManagerScript.PlayerHaveMineMagic != 1) StoreType = 3;
                }
            } else if (StoreType == 4){
                StoreUIAbilityEnabled();
                float AbilityMove = 0;
                AbilityMove = Input.GetAxisRaw("Vertical");
                if (AbilityMove != 0)
                {
                    audioSource.PlayOneShot(music);
                    Abilitybatten -= (int)AbilityMove;
                    if (Abilitybatten > AbilityUISelect.Length) Abilitybatten = 1;
                    if (Abilitybatten < 1) Abilitybatten = AbilityUISelect.Length;
                    StoreUIAbilityEnabled();

                }
                if (Input.GetButtonDown("Fire2"))
                {
                    StoreType = 1;
                    StoreUIAbilityEnabled();

                }
                if (Input.GetButtonDown("Fire1"))
                {
                    
                    if (Abilitybatten == 1 && gameManagerScript.PlayerMAXHP <= 5 ) StoreType = 5;
                    if (Abilitybatten == 2 && gameManagerScript.PlayerMAXMove <= 4) StoreType = 5;
                    if (Abilitybatten == 3 && gameManagerScript.PlayerMagicAddTime >2f) StoreType = 5;
                }
            }
            else if (StoreType == 5)
            {
                StoreUIConfirmEnabled();
                StoreUIConfirm.SetActive(true);
                float ConfirmMove = 0;
                ConfirmMove = Input.GetAxisRaw("Horizontal");
                if (ConfirmMove != 0)
                {
                    audioSource.PlayOneShot(music);
                    StoreUIConfirmbatten += (int)ConfirmMove;
                    if (StoreUIConfirmbatten > StoreUIConfirmSelect.Length) StoreUIConfirmbatten = 1;
                    if (StoreUIConfirmbatten < 1) StoreUIConfirmbatten = StoreUIConfirmSelect.Length;
                    StoreUIConfirmEnabled();
                }



                if (StoreUIConfirmbatten == 1)
                {
                    if (Input.GetButtonDown("Fire1") && gameManagerScript.PlayerMoney >= 200)
                    {
                        audioSource.PlayOneShot(musicMoney);
                        gameManagerScript.PlayerMoney -= 200;
                        if (Abilitybatten == 1) {
                            gameManagerScript.PlayerMAXHP += 1;
                            gameManagerScript.PlayerHP = gameManagerScript.PlayerMAXHP;
                        } 
                        if (Abilitybatten == 2) gameManagerScript.PlayerMAXMove += 1;
                        if (Abilitybatten == 3) gameManagerScript.PlayerMagicAddTime -= 0.2f;
                        StoreType = 4;
                        StoreUIConfirmbatten = 1;
                        StoreUIConfirm.SetActive(false);
                    }
                }
                else if (StoreUIConfirmbatten == 2)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        StoreType = 4;
                        Abilitybatten = 1;
                        StoreUIConfirm.SetActive(false);
                    }
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    StoreType = 4;
                    StoreUIConfirm.SetActive(false);
                }
            }

        }
	}

    void CardAndAbilitySelectEnabled()
    {
        for (int x = 0; x < StoreCardAndAbilityUISelect.Length; x++)
        {

            if (x == StoreCardAndAbilityUISelectbatten - 1  && StoreType == 1)
                StoreCardAndAbilityUISelect[x].enabled = true;
            else
                StoreCardAndAbilityUISelect[x].enabled = false;

        }

    }
    void CardSelectEnabled()
    {
        for (int x = 0; x < CardUISelect.Length; x++)
        {

            if (x == StoreCardUISelectbatten - 1 && StoreType == 2)
                CardUISelect[x].enabled = true;
            else
                CardUISelect[x].enabled = false;

        }

    }

    void StoreUIConfirmEnabled()
    {
        for (int x = 0; x < StoreUIConfirmSelect.Length; x++)
        {

            if (x == StoreUIConfirmbatten - 1)
            {
                if (StoreType == 3 || StoreType == 5) StoreUIConfirmSelect[x].enabled = true;
            }
            else {
                StoreUIConfirmSelect[x].enabled = false;
            }
                

        }

    }

    void WitchCardImage() {
        for (int x = 0; x < WitchCard.Length; x++)
        {

            StoreCardImage[x].sprite = StoreCardSprite[WitchCard[x]];

        }

    }

    void StoreUICard() {
        for (int x = 0; x < StoreCardImage.Length; x++)
        {

           StoreCardImage[x].enabled = true;

        }
        CardUI.SetActive(true);
        AbilityUI.SetActive(false);
        if (gameManagerScript.PlayerHaveEarthMagic == 1) StoreCardImage[0].color= new Color(0.5F, 0.5F, 0.5F);
         if (gameManagerScript.PlayerHaveMineMagic == 1) StoreCardImage[1].color = new Color(0.5F, 0.5F, 0.5F);

        WitchCardImage();

    }

    void StoreUIAbility()
    {
        for (int x = 0; x < AbilityUISelect.Length; x++)
        {

            if (x == Abilitybatten - 1 && StoreType == 4)
                AbilityUISelect[x].enabled = true;
            else
                AbilityUISelect[x].enabled = false;

        }

        for (int h = 0; h < MAXHP.Length; h++) {

            if (gameManagerScript.PlayerMAXHP <= h)
            {
                MAXHP[h].sprite = KEYAddLess[0];
            }
            else
            {
                MAXHP[h].sprite = KEYAddLess[1];

            }

        }

        for (int m = 0; m < MAXMOVE.Length; m++)
        {

            if (gameManagerScript.PlayerMAXMove <= m)
            {
                MAXMOVE[m].sprite = KEYAddLess[0];
            }
            else
            {
                MAXMOVE[m].sprite = KEYAddLess[1];

            }

        }
        for (int a = 0; a < MAXATK.Length; a++)
        {

            if (gameManagerScript.PlayerMagicAddTime*5-10 >= a)
            {
                MAXATK[a].sprite = KEYAddLess[0];
            }
            else
            {
                MAXATK[a].sprite = KEYAddLess[1];

            }

        }
        CardUI.SetActive(false);
        AbilityUI.SetActive(true);
    }

    void StoreUIAbilityEnabled()
    {
        for (int x = 0; x < AbilityUISelect.Length; x++)
        {

            if (x == Abilitybatten - 1 && StoreType == 4)
                AbilityUISelect[x].enabled = true;
            else
                AbilityUISelect[x].enabled = false;

        }
        for (int h = 0; h < MAXHP.Length; h++)
        {

            if (gameManagerScript.PlayerMAXHP <= h)
            {
                MAXHP[h].sprite = KEYAddLess[0];
            }
            else
            {
                MAXHP[h].sprite = KEYAddLess[1];

            }

        }

        for (int m = 0; m < MAXMOVE.Length; m++)
        {

            if (gameManagerScript.PlayerMAXMove <= m)
            {
                MAXMOVE[m].sprite = KEYAddLess[0];
            }
            else
            {
                MAXMOVE[m].sprite = KEYAddLess[1];

            }

        }
        for (int a = 0; a < MAXATK.Length; a++)
        {

            if (gameManagerScript.PlayerMagicAddTime * 5 - 10 >= a)
            {
                MAXATK[a].sprite = KEYAddLess[0];
            }
            else
            {
                MAXATK[a].sprite = KEYAddLess[1];

            }

        }
    }

    private IEnumerator StartStoreUI() {
        StoreCardAndAbilityUISelectbatten = 1;
        yield return new WaitForSeconds(0.1f);
        StoreType = 1;
        CardAndAbilitySelectEnabled();
        CardSelectEnabled();

    }
}
