using UnityEngine;
using System.Collections;

public class BattleUIScript : MonoBehaviour {
    [SerializeField]
    private Transform UIManager;
    [SerializeField]
    private GameObject WatchUI;

    private BattleManagerScript BattleManager;

    [SerializeField]
    private GameObject ZeroUI;

    private bool UIisOpen = false;

    private Transform player;
    // Use this for initialization
    void Start()
    {
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
        UIManager = GameObject.FindWithTag("UIManager").transform;
        UIisOpen = false;
        ZeroUI = GameObject.FindWithTag("ZeroUI");

    }

    // Update is called once per frame
    
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && UIisOpen == false)
        {
            BattleManager.BattleStop = true;
            BattleManager.EnemyBattleStop = true;
            BattleManager.PlayerCanCantroller = false;
            UIManager.transform.GetComponent<UIManagerScript>().ToScreen(WatchUI);
            UIisOpen = true;
        }
        if (ZeroUI != null)
        {
            //BattleManager.EnemyBattleStop = false;
            //BattleManager.PlayerCanCantroller = true;
            UIisOpen = false;

        }


    }
    
}
