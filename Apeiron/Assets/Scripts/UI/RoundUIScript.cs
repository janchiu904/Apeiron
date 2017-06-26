﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundUIScript : MonoBehaviour {
    private GameManager gameManagerScript;
    // Use this for initialization
    void Start()
    {
        gameManagerScript = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "回合：" + gameManagerScript.round;
    }
}
