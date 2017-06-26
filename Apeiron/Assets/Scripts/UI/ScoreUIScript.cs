using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUIScript : MonoBehaviour {
    [SerializeField]
    private Text[] showRank;

    // Use this for initialization
    void Awake() {
        for (int i = 0; i < 5; i++)
        {
            showRank[i].text = PlayerPrefs.GetInt("Score" + i.ToString()).ToString();
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
    /*
    private void UpdateRank()
    {
        List<int> rankScores = new List<int>();
        for (int i = 0; i < 20; i++)
        {
            rankScores.Add(PlayerPrefs.GetInt("Score" + i.ToString()));
        }
        rankScores.Add(Int32.Parse(player1TextScore.text));

        rankScores.Sort();
        rankScores.Reverse();

        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Score" + i.ToString(), rankScores[i]);
            //transform.GetChild(1).GetComponent<Text>().text = ;
        }
    }
    
}*/

}
