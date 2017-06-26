using UnityEngine;
using System.Collections;

public class ApeironCreatMobScript : MonoBehaviour {
    private GameManager gameManagerScript;
    private BattleManagerScript BattleManager;
    [SerializeField]
    private Transform[] Mob;
    [SerializeField]
    private GameObject EneyPos;
    // Use this for initialization
    void Start()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        BattleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void CreatMob() {
        gameManagerScript.round += 1;
        int x = Random.Range(0, 3);
        float IsBattlePosX;
        float IsBattlePosY;
        IsBattlePosX = 500f;
        IsBattlePosY = 500f;
        Vector2 BattlePo = new Vector2(IsBattlePosX, IsBattlePosY);
        Instantiate(Mob[x], BattlePo, Mob[x].rotation, EneyPos.transform);
        


    }
}
