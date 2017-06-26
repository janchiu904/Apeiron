using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip Music;
    // Use this for initialization
    void Awake() {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.clip == null) {
            audioSource.clip = Music;
        }

    }
    public void PlayMusic(AudioClip musicname) {
        audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != musicname) {
            audioSource.clip = musicname;
            audioSource.Play();
        }
    }
    // Update is called once per frame
    void Update () {
        audioSource.volume = PlayerPrefs.GetInt("KEY_MUSIC", 100)*0.01f;

    }
}
