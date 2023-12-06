using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChangeSound : MonoBehaviour
{
    public AudioClip gunIntro;
    public AudioClip gunOutro;
    
    public void GunSoundIntro()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gunIntro);
    }
    public void GunSoundOutro()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(gunOutro);
    }
}
