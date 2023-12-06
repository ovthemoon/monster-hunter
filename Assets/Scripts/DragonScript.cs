using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DragonScript : MonoBehaviour
{
    public float maxHp = 50;
    public float curHp;
    public Slider hpBar;
    public float damage = 10f;

    private Transform player;
    private bool isDead=false;
    private Animator animator;

    [Header("Sound")]
    public AudioClip flySound;
    public AudioClip deathSound;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
        curHp = maxHp;
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        hpBar.maxValue = maxHp;
        hpBar.minValue = 0;
        hpBar.value = curHp;
        audioSource=GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.LookAt(player);
    }
    public void DecreaseHp(float damage)
    {
        curHp -= damage;
        hpBar.value = curHp;
        if (curHp <= 0)
        {
            isDead = true;
            animator.SetBool("IsDead",isDead);
            GameManager.instance.IncreaseDefeatedEnemyCount();
            audioSource.Stop();
            audioSource.PlayOneShot(deathSound);
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().DecreaseHp(damage);
            GameManager.instance.GameOverCheck();
            Destroy(gameObject);
        }
    }
    
}
