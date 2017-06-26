using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {
    private BattleManagerScript BattleManager;
    private bool CanDamage;
    Animator anim;
    private float DieTimer = 0;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip TakeDamageAudio;
    // Use this for initialization
    void Awake() {
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        anim = GetComponent<Animator>();
        CanDamage = true;
        DieTimer = 0;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

        if(Input.GetKeyDown("q")) audioSource.PlayOneShot(TakeDamageAudio);
        if (DieTimer<5f) DieTimer += Time.deltaTime;

        if (BattleManager.PlayerCanMove == true) {
            anim.SetInteger("MagicType", 0);

        }
        if (BattleManager.PlayerHPCount <= 0 && DieTimer >4f) {
            
            anim.SetTrigger("Die");

        }

        if (BattleManager.MagicType == 1)
        {
            anim.SetInteger("MagicType", 1);
        }
        if (BattleManager.MagicType == 2)
        {
            anim.SetInteger("MagicType", 2);
        }

        if (BattleManager.PlayerDamagePos[BattleManager.playerPosXY] != 0 && CanDamage == true)
        {
            if(BattleManager.PlayerDamagePos[BattleManager.playerPosXY] != 11) anim.SetBool("TakeDamageTrue", true);

        }
        else
        {
            anim.SetBool("TakeDamageTrue", false);
        }

    }

    public void PlayerIdleStart() {
        CanDamage = true;
    }

    public void PlayerTakeDamage() {
        BattleManager.PlayerHPCount -= 1;
        BattleManager.MagicType = 0;
        CanDamage = false;
        audioSource.PlayOneShot(TakeDamageAudio);
    }
    public void PlayerDieEnd() {
        BattleManager.BattleEnd(false);
    }

}
