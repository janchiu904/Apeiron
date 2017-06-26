using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour {

    private float letterPause = 0.05f;
    [SerializeField]
    private AudioClip typeSound;
    [SerializeField]
    private Text txtDisplay;

    //[SerializeField]
   // private Image Dialog;

    private string words;
    private string text = "安安安安安安安安!!";

    public bool over = false;

    void Awake()
    {
        over = false;
        Transform ins = GameObject.FindWithTag("UIRoot").transform;
        if (ins == null)
        {

            ins = this.transform;


        }
        else if (ins != this.transform)
        {

            Destroy(gameObject);
        }

    }
    public IEnumerator display(string displayStr)
    {
        over = false;
        words = displayStr;
        text = "";
        yield return new WaitForSeconds(0.1f);
        txtDisplay.text = words;
        //StartCoroutine(TypeText());
    }

    public void ClearText() {
        txtDisplay.text = "";
    }

    void Start()
    {
        //StartCoroutine(display(text));

    }

    void Update() {



    }
    // 開啟打字效果
    private IEnumerator TypeText()
    {
        if(over == false)
        {
           
            foreach (var word in words)
            {

                txtDisplay.text += word;
               

                yield return new WaitForSeconds(letterPause);
            }

        }
       

        over = true;

    }
}
