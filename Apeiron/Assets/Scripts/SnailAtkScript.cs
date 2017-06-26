using UnityEngine;
using System.Collections;

public class SnailAtkScript : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //transform.localPosition = new Vector3(0, 0.39f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndAnim()
    {

        Destroy(this.gameObject, 0.5f);
    }
}
