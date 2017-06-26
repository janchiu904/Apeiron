using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LondingTextScript : MonoBehaviour {
    [SerializeField]
    private string[] LoadingText;

    // Use this for initialization
    void Awake () {
        Text loadText = GameObject.FindWithTag("LoadText").GetComponent<Text>();
        loadText.text = LoadingText[Random.Range(0, LoadingText.Length)];
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
