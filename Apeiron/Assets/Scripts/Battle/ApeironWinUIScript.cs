using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ApeironWinUIScript : MonoBehaviour {

    private GameManager BattleManager;
    // Use this for initialization
    void Start()
    {
        BattleManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "" + BattleManager.Score + "";
    }
}
