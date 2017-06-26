using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour {
    private AudioSource audioSource;

    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update () {
        audioSource.volume = PlayerPrefs.GetInt("KEY_SOUND", 100)* 0.01f;
    }
}
