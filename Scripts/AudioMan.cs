using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMan : MonoBehaviour
{
    public static AudioMan instan { get; private set; }

    AudioSource ads;
    // Start is called before the first frame update
    void Start()
    {
        instan = this;
        ads = GetComponent<AudioSource>();
    }

    public void call(AudioClip clip)
    {
        ads.PlayOneShot(clip);
    }
}
