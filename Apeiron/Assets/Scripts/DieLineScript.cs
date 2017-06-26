using UnityEngine;
using System.Collections;

public class DieLineScript : MonoBehaviour {
    private Transform player;
    [SerializeField]
    private Vector2 playerpos;
    // Use this for initialization
    void Start () {
        
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D obj)
    {
        player = GameObject.FindWithTag("player").transform;
        GameManager GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (obj.gameObject.tag == "player")
        {
            GameManager.SendMessage("PlayerDamaged", 1, SendMessageOptions.DontRequireReceiver);
            player.transform.position = playerpos;
            StartCoroutine(player.transform.GetComponent<PlayerControl>().TakenDamage());
        }
    }
}
