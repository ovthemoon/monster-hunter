using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModeControl : MonoBehaviour
{
    public GunMode currentMode;
    public GameObject bulletPrefab;
    public float bulletSpeed=15f;
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
