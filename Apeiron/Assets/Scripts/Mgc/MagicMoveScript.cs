using UnityEngine;
using System.Collections;

public class MagicMoveScript : MonoBehaviour
{
    Animator anim;

    private BattleManagerScript BattleManager;
    [SerializeField]
    private SpriteRenderer BattlePlayerSprite;
    private AudioSource audioSource;
    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        BattlePlayerSprite = GameObject.FindWithTag("BattlePlayer").GetComponent<SpriteRenderer>();
    }

    void Start() {
        //if(BattleManager.PlayerCanCantroller == true)PlayerPosMove();
    }

    public void PlayerEnabled()
    {
        BattlePlayerSprite.enabled = true;


   
    }

    public void PlayerReEnabled()
    {
        
        BattlePlayerSprite.enabled = false;
    }


    public void PlayerPosMove() {
        audioSource.PlayOneShot(audioSource.clip);
        BattleManager.GetGroundPos(BattleManager.playerPosY, BattleManager.playerPosX);
        BattleManager.PlayerPos.position = BattleManager.groundPos[BattleManager.playerPosXY].position;
        
        anim.SetBool("Move", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.PlayerCanCantroller == true) {
        if (Input.anyKeyDown && BattleManager.PlayerMoveCount > 0)
        {
            if (BattleManager.PlayerCanMove == true)
            {
                if (Input.GetAxisRaw("Horizontal") == 1 && BattleManager.playerPosX != 3)
                {
                    anim.SetBool("Move",true);
                    BattleManager.playerPosX += 1;
                    BattleManager.PlayerMoveCount -= 1;
                }
                if (Input.GetAxisRaw("Horizontal") == -1 && BattleManager.playerPosX != 1)
                {
                    anim.SetBool("Move", true);
                    BattleManager.playerPosX -= 1;
                    BattleManager.PlayerMoveCount -= 1;
                }

                if (Input.GetAxisRaw("Vertical") == 1 && BattleManager.playerPosY != 1)
                {
                    anim.SetBool("Move", true);
                    BattleManager.playerPosY -= 1;
                    BattleManager.PlayerMoveCount -= 1;
                }
                if (Input.GetAxisRaw("Vertical") == -1 && BattleManager.playerPosY != 3)
                {
                    anim.SetBool("Move", true);
                    BattleManager.playerPosY += 1;
                    BattleManager.PlayerMoveCount -= 1;
                }


            }

        }
        }
    }
}