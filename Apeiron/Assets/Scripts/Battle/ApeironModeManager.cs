using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class ApeironModeManager : MonoBehaviour {

    private GameManager gameManagerScript;

    // Use this for initialization
    void Start () {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateRank()
    {
        List<int> rankScores = new List<int>();
        for (int i = 0; i < 20; i++)
        {
            rankScores.Add(PlayerPrefs.GetInt("Score" + i.ToString()));
        }
        rankScores.Add(Int32.Parse(gameManagerScript.Score.ToString()));

        rankScores.Sort();
        rankScores.Reverse();

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), rankScores[i]);
            //transform.GetChild(1).GetComponent<Text>().text = ;
        }
    }
}
