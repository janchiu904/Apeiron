using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour {


    [SerializeField]
    private string[] text;

    private PlayerControl PlayerScript;

    private TypewriterText TypewrScript;

    private Transform dialog;
    private Transform dialogblack;
    private Transform dialogImger;
    private bool stare = false;
    private int textNow = 0;
    [SerializeField]
    private Sprite Imger;
    [SerializeField]
    private bool hitStare = true;
    [SerializeField]
    private bool OverDistore = false;

    private AudioSource audioSource;

    private float mTime = 0;
    private bool nTime = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stare = false;
        textNow = 0;
        PlayerScript = GameObject.FindWithTag("player").GetComponent<PlayerControl>();
        TypewrScript = GameObject.FindWithTag("UIRoot").GetComponent<TypewriterText>();
    }

    void OnTriggerStay2D(Collider2D obj) {

        if (hitStare == false && stare == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                mTime = 0;
                nTime = false;
                audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                audioSource.PlayOneShot(audioSource.clip);


                dialog.GetComponent<Image>().enabled = true;
                dialogblack.GetComponent<Image>().enabled = true;
                dialogImger.GetComponent<Image>().sprite = Imger;
                dialogImger.GetComponent<Image>().enabled = true;

                StartCoroutine(PlayerScript.stop(true));
                textNow = 0;
                
                StartCoroutine(TypewrScript.display(text[textNow]));
                stare = true;
            }

        }
    }

        void OnTriggerEnter2D(Collider2D obj)
    {
        if (!stare)
        {
            if (obj.gameObject.tag == "player")
            {
                


                dialog = GameObject.FindWithTag("Dialog").transform;
                dialogblack = GameObject.FindWithTag("DialogBlack").transform;
                dialogImger = GameObject.FindWithTag("DialogImage").transform;
                if (dialog != null)
                {
                    // Object dialogPrefab = Resources.Load("Prefabs/Dialog") as Object;

                    if (hitStare == true)
                    {
                        mTime = 0;
                        nTime = false;
                        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
                        audioSource.PlayOneShot(audioSource.clip);


                        dialog.GetComponent<Image>().enabled = true;
                        dialogblack.GetComponent<Image>().enabled = true;
                        dialogImger.GetComponent<Image>().sprite = Imger;
                        dialogImger.GetComponent<Image>().enabled = true;

                        StartCoroutine(PlayerScript.stop(true));
                        textNow = 0;
                        stare = true;
                        StartCoroutine(TypewrScript.display(text[textNow]));

                    } 
                        
                   
                }
                else
                {
                    Debug.LogError("There is not a GameObject with tag UIRoot");
                }
            }

        }
        
    }

    void Update() {
        if (stare == true) {

            if (Input.GetButtonDown("Fire1"))
            {
                if (hitStare == true) textNow += 1;
                if (textNow != text.Length) TypewrScript.ClearText();
                if (textNow == text.Length)
                {
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
                if (hitStare == false) textNow += 1;
            }


        }


        if (nTime == true) {
            mTime += Time.deltaTime;
            
            if (mTime > 0.5f) {
                TypewrScript.ClearText();
                EndDialog();
            }

        }
    }

    void EndDialog() {
        nTime = false;
        dialog.GetComponent<Image>().enabled = false;
        dialogblack.GetComponent<Image>().enabled = false;
        dialogImger.GetComponent<Image>().enabled = false;
        textNow = 0;
        
        StartCoroutine(PlayerScript.stop(false));
        //Destroy(gameObject, 0.5f);
        //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        hitStare = false;
        if (OverDistore == true)
        {

            Destroy(gameObject, 0.5f);

        }

    }
}
