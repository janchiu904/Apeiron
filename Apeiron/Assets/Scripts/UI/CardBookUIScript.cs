using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardBookUIScript : MonoBehaviour {
    private Transform gamemanager;
    [SerializeField]
    private Transform UIManager;
    private Transform player;
    [SerializeField]
    private Image[] CardBookUISelect;

    [SerializeField]
    private Image[] CardBookUICard;
    [SerializeField]
    private Sprite[] ALLCard;
    [SerializeField]
    private int[] ShowCard;

    [SerializeField]
    private AudioClip music;
    private AudioSource audioSource;
    [SerializeField]
    private int batten = 1;

    [SerializeField]
    private int Showbatten = 1;

    [SerializeField]
    private Text Card;
    [SerializeField]
    private Text AtkCard;
    [SerializeField]
    private Text BuffCard;
    private StageUIScript StageUI;
    // Use this for initialization
    void Awake () {
        UIManager = GameObject.FindWithTag("UIManager").transform;
        audioSource = GetComponent<AudioSource>();
        CardBookUISelectEnabled();
        CardBookUICardSprite();
        batten = 1;
        Showbatten = 1;
    }
	
	// Update is called once per frame
	void Update () {
        CardText();
        float moveUPDown = 0;
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        moveUPDown = Input.GetAxisRaw("Vertical");
        if (Input.anyKeyDown)
        {
            batten -= (int)moveUPDown*2;
            
            if (moveUPDown != 0)
            {
                if (moveUPDown == 1) {
                    if (Showbatten == 3) Showbatten = 1;
                    if (Showbatten == 4) Showbatten = 2;
                }else if (moveUPDown == -1)
                {
                    if (Showbatten == 1) Showbatten = 3;
                    if (Showbatten == 2) Showbatten = 4;
                }
                if (batten > ALLCard.Length && batten % 2 == 1) {
                    batten = 1;
                    Showbatten = 1;
                }
                if (batten > ALLCard.Length && batten % 2 == 0) {
                    batten = 2;
                    Showbatten = 2;
                }
                if (batten == -1) {
                    batten = 7;
                    Showbatten = 3;
                }
                if (batten == 0) {
                    batten = 8;
                    Showbatten = 4;
                }
                CardBookUISelectEnabled();
                CardBookUICardSprite();
                //audioSource.clip = music;
                audioSource.PlayOneShot(music);
                //audioSource.clip = Choose;
                //audioSource.PlayOneShot(Choose);
            }
        }
        float moveRL = 0;
        moveRL = Input.GetAxisRaw("Horizontal");
        if (Input.anyKeyDown)
        {
            if (moveRL == 1 && batten % 2 == 1) {
                batten += (int)moveRL;
                Showbatten += (int)moveRL;
            }
            else if (moveRL == 1 && batten % 2 == 0)
            {
                 batten -= (int)moveRL;
                Showbatten -= (int)moveRL;
            }
            else if (moveRL == -1 && batten % 2 == 1)
            {
                 batten -= (int)moveRL;
                Showbatten -= (int)moveRL;
            }
            else if (moveRL == -1 && batten % 2 == 0)
            { 
                 batten += (int)moveRL;
                Showbatten += (int)moveRL;
            }
            if (moveRL != 0)
            {
                //if (batten%2 !=1 ) batten = 1;
                //if (batten < 1) batten = CardBookUISelect.Length;
                CardBookUISelectEnabled();
                CardBookUICardSprite();
                //audioSource.clip = music;
                audioSource.PlayOneShot(music);
                //audioSource.clip = Choose;
                //audioSource.PlayOneShot(Choose);
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            UIManager.transform.GetComponent<UIManagerScript>().GoBack();
        }
    }
    void CardText() {
        if (batten == 1) {
            Card.text = "火元素，用於攻擊或提升自身傷害。";
            AtkCard.text = "造成前方固定距離1點的火屬性傷害。";
            BuffCard.text = "使自身攻擊提升1點。";
        }
        if (batten == 2)
        {
            Card.text = "水元素，用於攻擊或恢復自己血量。";
            AtkCard.text = "造成前方固定距離1點的水屬性傷害。";
            BuffCard.text = "恢復自身1點血量。";
        }
        if (batten == 3)
        {
            Card.text = "氣元素，用於遮蔽敵人視線，也可以讓自己藏在煙霧當中。";
            AtkCard.text = "遮蔽敵方場地，使敵方移動速度下降。";
            BuffCard.text = "無。";
        }
        if (batten == 4)
        {
            Card.text = "炎元素，兩張火所合成的進化元素，使用上比火元素更為強大。";
            AtkCard.text = "造成十字範圍2點的火屬性傷害。";
            BuffCard.text = "無。";
        }
        if (batten == 5)
        {
            Card.text = "土元素，用於攻擊或者製造一面可抵擋衝撞的牆。";
            AtkCard.text = "造成前方固定距離1點的土屬性傷害。";
            BuffCard.text = "在自身位置製造一面土牆，可以抵擋衝撞攻擊。";
        }
        if (batten == 6)
        {
            Card.text = "雷元素，用於攻擊或提升進牌速度。";
            AtkCard.text = "造成前方固定距離2點的雷屬性傷害。";
            BuffCard.text = "移動卡牌增加1張，可增加至上限。";
        }
        if (batten == 7)
        {
            Card.text = "盾元素，用於攻擊或抵擋傷害。";
            AtkCard.text = "造成前方固定距離1點的普通屬性傷害，並附加暈眩3秒。";
            BuffCard.text = "使敵方下三次攻擊對自身造成0點傷害。";
        }
        if (batten == 8)
        {
            Card.text = "";
            AtkCard.text = "";
            BuffCard.text = " ";
        }
    }

    void CardBookUISelectEnabled()
    {
        for (int x = 0; x < CardBookUISelect.Length; x++)
        {

            if (x == Showbatten - 1)
                CardBookUISelect[x].enabled = true;
            else
                CardBookUISelect[x].enabled = false;

        }

    }
    void CardBookUICardSprite()
    {
        if (Showbatten == 1) {
            CardBookUICard[0].sprite = ALLCard[batten - 1];
            CardBookUICard[1].sprite = ALLCard[batten];
            CardBookUICard[2].sprite = ALLCard[batten + 1];
            CardBookUICard[3].sprite = ALLCard[batten + 2];
        }else if (Showbatten == 2) {
            CardBookUICard[0].sprite = ALLCard[batten - 2];
            CardBookUICard[1].sprite = ALLCard[batten - 1];
            CardBookUICard[2].sprite = ALLCard[batten];
            CardBookUICard[3].sprite = ALLCard[batten + 1];            
        }
        else if (Showbatten == 3)
        {
            CardBookUICard[0].sprite = ALLCard[batten - 3];           
            CardBookUICard[1].sprite = ALLCard[batten-2];
            CardBookUICard[2].sprite = ALLCard[batten - 1];
            CardBookUICard[3].sprite = ALLCard[batten];
        }
        else if (Showbatten == 4)
        {
            CardBookUICard[0].sprite = ALLCard[batten - 4];
            CardBookUICard[1].sprite = ALLCard[batten - 3];            
            CardBookUICard[2].sprite = ALLCard[batten-2];
            CardBookUICard[3].sprite = ALLCard[batten - 1];
        }



    }
}
