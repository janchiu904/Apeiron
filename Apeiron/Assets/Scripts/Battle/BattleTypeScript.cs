using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleTypeScript : MonoBehaviour {
    private BattleManagerScript BattleManager;
    public Transform[] groundPos;
    public Transform[] EnemygroundPos;

    [SerializeField]
    private string[] text;
    [SerializeField]
    private string[] text2;
    [SerializeField]
    private string[] text3;
    [SerializeField]
    private string[] text4;
    [SerializeField]
    private string[] text5;

    private PlayerControl PlayerScript;

    private TypewriterText TypewrScript;

    private Transform dialog;
    private Transform dialogblack;
    private Transform dialogImger;
    private bool stare = false;
    private int textNow = 0;
    [SerializeField]
    private Sprite[] Imger;
    [SerializeField]
    private bool Teach = true;
    private int TeachCount = 0;

    private AudioSource audioSource;
    private float mTime = 0;
    private bool nTime = false;

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        stare = false;
        textNow = 0;
        TypewrScript = GameObject.FindWithTag("UIRoot").GetComponent<TypewriterText>();
        BattleManager.BattleStop = true;
        // BattleStare();
    }
	
	// Update is called once per frame
	public void BattleStare () {
        if (Teach == true)
        {
            BattleManager.BattleStart();
            BattleManager.BattleStop = true;
            BattleManager.BattleStop = false;
            TeachCount = 1;
            
        }
        else {
            BattleManager.PlayerCanCantroller = true;
            BattleManager.BattleStop = false;
            BattleManager.BattleStart();
        }
        

    }

    void Update() {
        if (TeachCount == 1)
        {
            TeachCount1();
        }
        else if (TeachCount == 2)
        {
            TeachCount2();
        }
        else if (TeachCount == 3)
        {
           if(BattleManager.playerPosXY == 1 || BattleManager.playerPosXY == 3 || BattleManager.playerPosXY == 5 || BattleManager.playerPosXY == 7) TeachCount3();
        }
        else if (TeachCount == 4)
        {
            TeachCount4();
        }
        else if (TeachCount == 5 && BattleManager.MagicType == 1)
        {
            TeachCount5();
        }
        else if (TeachCount == 6)
        {
            TeachCount6();
        }
        else if (TeachCount == 7 && BattleManager.MagicType == 2)
        {
            TeachCount7();
        }
        else if (TeachCount == 8)
        {
            TeachCount8();
        }
        else if (TeachCount == 9 && BattleManager.MagicType == 0)
        {
            TeachCount9();
        }
        else if (TeachCount == 10) {
            TeachCount10();
        }

        if (nTime == true)
        {
            mTime += Time.deltaTime;

            if (mTime > 0.5f)
            {
                TypewrScript.ClearText();
                EndDialog();
            }

        }
    }

    void TeachCount10()
    {
        if (stare == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                textNow += 1;
                if (textNow != text5.Length) TypewrScript.ClearText();
                if (textNow == text5.Length)
                {
                    TeachCount = 11;


                    stare = false;
                    BattleManager.BattleStop = false;
                    BattleManager.EnemyBattleStop = false;
                    audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                    audioSource.PlayOneShot(audioSource.clip);
                    stare = false;
                    nTime = true;
                    //StartCoroutine(TypewrScript.display(text5[textNow - 1]));
                }
                else
                {
                    StartCoroutine(TypewrScript.display(text5[textNow]));
                }
            }
        }

    }

    void TeachCount9()
    {
        BattleManager.BattleStop = true;

        TeachCount = 10;
        dialog = GameObject.FindWithTag("Dialog").transform;
        dialogblack = GameObject.FindWithTag("DialogBlack").transform;
        dialogImger = GameObject.FindWithTag("DialogImage").transform;
        if (dialog != null)
        {
            // Object dialogPrefab = Resources.Load("Prefabs/Dialog") as Object;
            mTime = 0;
            nTime = false;
            audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
            audioSource.PlayOneShot(audioSource.clip);

            dialog.GetComponent<Image>().enabled = true;
            dialogblack.GetComponent<Image>().enabled = true;
            dialogImger.GetComponent<Image>().sprite = Imger[7];
            dialogImger.GetComponent<Image>().enabled = true;

            BattleManager.PlayerCanCantroller = false;
            textNow = 0;
            stare = true;
            StartCoroutine(TypewrScript.display(text5[textNow]));

        }

    }

    void TeachCount8()
    {
        if (stare == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                textNow += 1;
                if (textNow != text4.Length) TypewrScript.ClearText();
                if (textNow == text4.Length)
                {
                    TeachCount = 9;

                    stare = false;
                    BattleManager.PlayerCanCantroller = true;
                    BattleManager.BattleStop = false;
                    audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                    audioSource.PlayOneShot(audioSource.clip);
                    stare = false;
                    nTime = true;
                    //StartCoroutine(TypewrScript.display(text4[textNow - 1]));
                }
                else
                {
                    dialogImger.GetComponent<Image>().sprite = Imger[textNow+4];
                    StartCoroutine(TypewrScript.display(text4[textNow]));
                }
            }
        }

    }

    void TeachCount7()
    {
        BattleManager.BattleStop = true;

        TeachCount = 8;
        dialog = GameObject.FindWithTag("Dialog").transform;
        dialogblack = GameObject.FindWithTag("DialogBlack").transform;
        dialogImger = GameObject.FindWithTag("DialogImage").transform;
        if (dialog != null)
        {
            // Object dialogPrefab = Resources.Load("Prefabs/Dialog") as Object;
            mTime = 0;
            nTime = false;
            audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
            audioSource.PlayOneShot(audioSource.clip);

            dialog.GetComponent<Image>().enabled = true;
            dialogblack.GetComponent<Image>().enabled = true;
            dialogImger.GetComponent<Image>().sprite = Imger[4];
            dialogImger.GetComponent<Image>().enabled = true;

            BattleManager.PlayerCanCantroller = false;
            textNow = 0;
            stare = true;
            StartCoroutine(TypewrScript.display(text4[textNow]));

        }

    }

    void TeachCount6()
    {
        if (stare == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                textNow += 1;
                if (textNow != text3.Length) TypewrScript.ClearText();
                if (textNow == text3.Length)
                {
                    TeachCount = 7;

                    stare = false;
                    BattleManager.PlayerCanCantroller = true;
                    BattleManager.BattleStop = false;
                    audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                    audioSource.PlayOneShot(audioSource.clip);
                    stare = false;
                    nTime = true;
                    //StartCoroutine(TypewrScript.display(text3[textNow - 1]));
                }
                else
                {
                    dialogImger.GetComponent<Image>().sprite = Imger[3];
                    StartCoroutine(TypewrScript.display(text3[textNow]));
                }
            }
        }

    }

    void TeachCount5() {
        BattleManager.BattleStop = true;

        TeachCount = 6;
        dialog = GameObject.FindWithTag("Dialog").transform;
        dialogblack = GameObject.FindWithTag("DialogBlack").transform;
        dialogImger = GameObject.FindWithTag("DialogImage").transform;
        if (dialog != null)
        {
            // Object dialogPrefab = Resources.Load("Prefabs/Dialog") as Object;
            mTime = 0;
            nTime = false;
            audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
            audioSource.PlayOneShot(audioSource.clip);

            dialog.GetComponent<Image>().enabled = true;
            dialogblack.GetComponent<Image>().enabled = true;
            dialogImger.GetComponent<Image>().sprite = Imger[2];
            dialogImger.GetComponent<Image>().enabled = true;

            BattleManager.PlayerCanCantroller = false;
            textNow = 0;
            stare = true;
            StartCoroutine(TypewrScript.display(text3[textNow]));

        }

    }
    void TeachCount4()
    {
        if (stare == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                textNow += 1;
                if (textNow != text2.Length) TypewrScript.ClearText();
                if (textNow == text2.Length)
                {
                    TeachCount = 5;

                    stare = false;
                    BattleManager.PlayerCanCantroller = true;
                    BattleManager.BattleStop = false;
                    audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                    audioSource.PlayOneShot(audioSource.clip);
                    stare = false;
                    nTime = true;
                    //StartCoroutine(TypewrScript.display(text2[textNow - 1]));
                }
                else
                {
                    StartCoroutine(TypewrScript.display(text2[textNow]));
                }
            }
        }

    }

    void TeachCount3() {
        BattleManager.BattleStop = true;
        TeachCount = 4;

        dialog = GameObject.FindWithTag("Dialog").transform;
        dialogblack = GameObject.FindWithTag("DialogBlack").transform;
        dialogImger = GameObject.FindWithTag("DialogImage").transform;
        if (dialog != null)
        {
            // Object dialogPrefab = Resources.Load("Prefabs/Dialog") as Object;
            mTime = 0;
            nTime = false;
            audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
            audioSource.PlayOneShot(audioSource.clip);

            dialog.GetComponent<Image>().enabled = true;
            dialogblack.GetComponent<Image>().enabled = true;
            dialogImger.GetComponent<Image>().sprite = Imger[1];
            dialogImger.GetComponent<Image>().enabled = true;

            BattleManager.PlayerCanCantroller = false;
            textNow = 0;
            stare = true;
            StartCoroutine(TypewrScript.display(text2[textNow]));

        }
    }

    void TeachCount2() {
        if (stare == true)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                textNow += 1;
                if (textNow != text.Length) TypewrScript.ClearText();
                if (textNow == text.Length)
                {
                    TeachCount = 3;
                    stare = false;
                    BattleManager.PlayerCanCantroller = true;
                    BattleManager.BattleStop = false;
                    audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                    audioSource.PlayOneShot(audioSource.clip);
                    stare = false;
                    nTime = true;
                   // StartCoroutine(TypewrScript.display(text[textNow - 1]));

                }
                else
                {
                    StartCoroutine(TypewrScript.display(text[textNow]));
                }
            }
        }

    }

    void TeachCount1() {
        BattleManager.BattleStop = true;
        BattleManager.EnemyBattleStop = true;
        TeachCount = 2;
        dialog = GameObject.FindWithTag("Dialog").transform;
        dialogblack = GameObject.FindWithTag("DialogBlack").transform;
        dialogImger = GameObject.FindWithTag("DialogImage").transform;
        if (dialog != null)
        {
            // Object dialogPrefab = Resources.Load("Prefabs/Dialog") as Object;
            mTime = 0;
            nTime = false;
            audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
            audioSource.PlayOneShot(audioSource.clip);

            dialog.GetComponent<Image>().enabled = true;
            dialogblack.GetComponent<Image>().enabled = true;
            dialogImger.GetComponent<Image>().sprite = Imger[0];
            dialogImger.GetComponent<Image>().enabled = true;

            BattleManager.PlayerCanCantroller = false;
            textNow = 0;
            stare = true;
            StartCoroutine(TypewrScript.display(text[textNow]));

        }

    }

    void EndDialog()
    {
        dialog.GetComponent<Image>().enabled = false;
        dialogblack.GetComponent<Image>().enabled = false;
        dialogImger.GetComponent<Image>().enabled = false;
        textNow = 0;
        stare = false;
        BattleManager.PlayerCanCantroller = true;
        BattleManager.BattleStop = false;


        nTime = false;

    }
}
