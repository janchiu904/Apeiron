using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageMoneyTextScript : MonoBehaviour {
    

    private GameManager gameManagerScript;
    // Use this for initialization
    void Start()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "" + gameManagerScript.PlayerMoney;
    }
}
