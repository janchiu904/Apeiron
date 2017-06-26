using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {
    Animator anim;
    private bool isopen;
    private float XTime;
    private float WTime;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip Jump;
    [SerializeField]
    private AudioClip Hurt;

    private GameManager gamemanagerscript;
    private GameObject PlayerControl;
    [SerializeField]
    private Transform posBack;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        PlayerControl = GameObject.FindWithTag("player");
        gamemanagerscript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        isopen = false;
        WTime = Random.Range(0, 100);
    }
	
	// Update is called once per frame
	void Update () {
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100) * 0.01f;
        XTime += Time.deltaTime;
        if (isopen == false && XTime >= WTime/50) {
            int x= Random.Range(0, 10);
            if (x <= 5) {
                audioSource.PlayOneShot(Jump);
                anim.SetTrigger("FireBall");
                isopen = true;
                WTime = Random.Range(0, 100);

            }
            XTime = 0;
        }
	
	}
    public void EndFireBall() {
        isopen = false;

    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "player")
        {
            audioSource.PlayOneShot(Hurt);
            StartCoroutine(PlayerControl.GetComponent<PlayerControl>().TakenDamage());
            if (gamemanagerscript.PlayerHP <= 0) PlayerControl.GetComponent<PlayerControl>().StageGameover(posBack.position.x, posBack.position.y);
            if (PlayerControl.GetComponent<PlayerControl>().takenDamage == false) gamemanagerscript.PlayerHP -= 1;
            
            if (obj.gameObject.transform.position.x > gameObject.transform.position.x)
                PlayerControl.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f, 0));
            else
                PlayerControl.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100f, 0));
        }

    }
}
