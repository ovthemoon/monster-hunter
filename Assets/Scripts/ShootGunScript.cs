using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ShootGunScript : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject effectShoot;

    private float shootCooltime;
    private PlayerMove playerMove;
    private bool isCooltime=false;
    private GunModeControl gunModeControl;
    private Coroutine shootCoroutine;
    private AudioSource shootAudioSource;
    
    private float originalRecoil;
    private void Start()
    {
        playerMove=GetComponent<PlayerMove>();
        shootAudioSource=GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
        gunModeControl = playerMove.gunMode[playerMove.currentModeIndex].GetComponent<GunModeControl>();
        shootCooltime = gunModeControl.currentMode.coolTime;
        shootAudioSource= playerMove.gunMode[playerMove.currentModeIndex].GetComponent<AudioSource>();
        if (!playerMove.isChanging)
        {
            if (Input.GetMouseButtonDown(0)&&gunModeControl.currentMode.mode==(AttackMode.MACHINEGUN))
            {
                playerMove.gunMode[playerMove.currentModeIndex].GetComponent<Animator>().SetBool("IsAttacking", true);
            }
            else if (Input.GetMouseButtonUp(0) && gunModeControl.currentMode.mode == (AttackMode.MACHINEGUN))
            {
                playerMove.gunMode[playerMove.currentModeIndex].GetComponent<Animator>().SetBool("IsAttacking", false);
            }
            if (Input.GetMouseButton(0)&&!isCooltime)
            {
                
                shootCoroutine =StartCoroutine(ShootBullet());
            }
        }
    }
   
    IEnumerator ShootBullet()
    {
        Debug.Log("Shooting");
        isCooltime = true;
        shootAudioSource.Play();
        
        GameObject effect = Instantiate(effectShoot, shootPoint.position, shootPoint.rotation);
        Destroy(effect,1f);
        GameObject bullet =Instantiate(gunModeControl.bulletPrefab, shootPoint.position,shootPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootPoint.forward * gunModeControl.bulletSpeed; // 여기서 bulletSpeed는 총알의 속도입니다.
        }
        yield return new WaitForSeconds(shootCooltime);
        isCooltime = false;
    }
    public void ResetCooltime()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            isCooltime = false;
        }
        
    }
}
