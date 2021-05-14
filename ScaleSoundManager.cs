using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSoundManager : MonoBehaviour
{

    private AudioSource audio;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void SetPlaying(bool state) {
        if (state) {
            audio.Play();
        } else {
            audio.Stop();
        }
    }

    public void SetPitch(float p) {
        audio.pitch = p;
    }
}
