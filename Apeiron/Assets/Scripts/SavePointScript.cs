using UnityEngine;
using System.Collections;

public class SavePointScript : MonoBehaviour {
    [SerializeField]
    private string SaveBackScenceName;
    [SerializeField]
    private string SaveBackAudio;
    Animator anim;

    private GameManager gameManagerScript;
    // Use this for initialization
    void Awake () {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "player")
        {
            anim.SetTrigger("Save");

           PlayerPrefs.SetInt("HaveSave", 1);
            PlayerPrefs.SetFloat("IsSavePosX", obj.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("IsSavePosY", obj.gameObject.transform.position.y);
            PlayerPrefs.SetString("SaveBackScence", SaveBackScenceName);
            PlayerPrefs.SetString("SaveBackAudio", SaveBackAudio);

            gameManagerScript.PlayerHP = gameManagerScript.PlayerMAXHP;

            PlayerPrefs.SetInt("PlayerHP", gameManagerScript.PlayerHP);
            PlayerPrefs.SetInt("PlayerMAXHP", gameManagerScript.PlayerMAXHP);
            PlayerPrefs.SetInt("PlayerMAXMove", gameManagerScript.PlayerMAXMove);
            PlayerPrefs.SetInt("PlayerMoney", gameManagerScript.PlayerMoney);
            PlayerPrefs.SetFloat("PlayerMoveAddTime", gameManagerScript.PlayerMoveAddTime);
            PlayerPrefs.SetFloat("PlayerMagicAddTime", gameManagerScript.PlayerMagicAddTime);
            PlayerPrefs.SetInt("PlayerHaveMineMagic", gameManagerScript.PlayerHaveMineMagic);
            PlayerPrefs.SetInt("PlayerHaveEarthMagic", gameManagerScript.PlayerHaveEarthMagic);
}

    }
}
