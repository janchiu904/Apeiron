using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinUIMoneyText : MonoBehaviour {
    private BattleManagerScript BattleManager;
    // Use this for initialization
    void Start()
    {
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text =""+ BattleManager.GetMoney+"";
    }
}
