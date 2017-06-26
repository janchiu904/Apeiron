using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    private float maxSpeed = 0.1f;
    public bool facingRight = true;
    float takenDamageTime = 0.2f;
    public bool takenDamage = false;

    private bool canJump = true;
    private bool canChangeMac = true;

    Animator anim;

    bool grounded = false;

    [SerializeField]
    private GameManager gamemanager;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask whatIsGround;

    private float jumpForce = 300f;
    float groundRadius = 0.05f;
    [SerializeField]
    private AudioSource Sound;
    [SerializeField]
    private AudioSource Jump;
    [SerializeField]
    private AudioSource JumpDoen;
    private bool RunSoundIsPlay = false;

    [SerializeField]
    private AudioClip Walk;

    private bool IsJump = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        Transform ins = GameObject.FindWithTag("player").transform;
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (ins == null)
        {

            ins = this.transform;


        }
        else if (ins != this.transform)
        {

            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void FixedUpdate() {

        PlayerSound();
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("ground", grounded);

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        float move = 0;

        if (transform.GetComponent<Rigidbody2D>().isKinematic == false) { move = Input.GetAxisRaw("Horizontal"); }
            
        


        anim.SetFloat("speed", Mathf.Abs(move));


        gameObject.transform.position = new Vector2(gameObject.transform.position.x + move*maxSpeed, gameObject.transform.position.y );
        //GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

	}

    void Update() {

        if (grounded && IsJump == true)
        {
            JumpDoen.Play();
            IsJump = false;
        }
        if (grounded && Input.GetButtonDown("Jump") && canJump) {
            anim.SetBool("ground", false);

            Jump.Play();
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            
        }
        
    }

    void PlayerSound() {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal"))> 0 && grounded && transform.GetComponent<Rigidbody2D>().isKinematic == false)
        {
            if (RunSoundIsPlay == false) {
                Sound.clip = Walk;
                Sound.Play();
                RunSoundIsPlay = true;
            }
        }
        else {
            Sound.Stop();
            RunSoundIsPlay = false;
        }

    }


    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



    public IEnumerator movestop() {
            yield return new WaitForSeconds(0.3f);
            //gamemanager.CanGUI = false;
            transform.GetComponent<Rigidbody2D>().isKinematic = true;
            canJump = false;

            //gamemanager.CanGUI = true;
            yield return new WaitForSeconds(5f);
            transform.GetComponent<Rigidbody2D>().isKinematic = false;
            canJump = true;


    }
    public IEnumerator stop(bool playerstop)
    {
        if (playerstop)
        {
            //anim.enabled = false;
            transform.GetComponent<Rigidbody2D>().isKinematic = true;
            canJump = false;
        }
        else if (!playerstop)
        {
            yield return new WaitForSeconds(0.2f);
            //anim.enabled = true;
            transform.GetComponent<Rigidbody2D>().isKinematic = false;
            canJump = true;
        }


    }

    public void IsJumping() {
        IsJump = true;

    }

    public void StageGameover(float x, float y) {
        gamemanager.PlayerHP = gamemanager.PlayerMAXHP;
        gameObject.transform.position = new Vector2(x, y);
    }

   
    public IEnumerator TakenDamage()
    {
        if (takenDamage == false) gamemanager.PlayerHP -= 1;
        takenDamage = true;
        
        //StartCoroutine(TakenDamage());
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(takenDamageTime);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(takenDamageTime);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(takenDamageTime);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(takenDamageTime);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(takenDamageTime);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yield return new WaitForSeconds(takenDamageTime);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        takenDamage = false;
    }
}
