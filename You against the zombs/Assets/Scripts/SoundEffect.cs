using UnityEngine;
using System.Collections;

public class SoundEffect : MonoBehaviour {

    public AudioClip Chug;
    public AudioClip Aftertaste;

    private AudioSource audioSource;
    
    public void PlayDrinkSound()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Chug;
        audioSource.Play();
        Invoke("PlayAfterTaste", 1.8f);
    }

    void PlayAfterTaste()
    {
        audioSource.clip = Aftertaste;
        audioSource.Play();
    }
}


