using UnityEngine;
using System.Collections;

public class StageFall : MonoBehaviour {
    private GameManager gamemanagerscript;
    private GameObject PlayerControl;
    [SerializeField]
    private Transform pos;
    [SerializeField]
    private Transform posBack;
    // Use this for initialization
    void Start () {
        PlayerControl = GameObject.FindWithTag("player");
        gamemanagerscript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.tag == "player")
        {

            StartCoroutine(PlayerControl.GetComponent<PlayerControl>().TakenDamage());
            
            if (PlayerControl.GetComponent<PlayerControl>().takenDamage == false) gamemanagerscript.PlayerHP -= 1;
            
            PlayerControl.transform.position = pos.position;
            if (gamemanagerscript.PlayerHP <= 0) PlayerControl.GetComponent<PlayerControl>().StageGameover(posBack.position.x, posBack.position.y);
        }

    }
}
