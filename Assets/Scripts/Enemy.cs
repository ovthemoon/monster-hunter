using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHp = 50;
    public float curHp;
    public Slider hpBar;
    public float damage = 10f;

    private Transform player;
    private bool isDead=false;
    private Animator animator;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        curHp = maxHp;
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        hpBar.maxValue = maxHp;
        hpBar.minValue = 0;
        hpBar.value = curHp;

    }
    private void Update()
    {
        
        if (!isDead)
        {
            agent.SetDestination(player.position);
            // 여기에 애니메이션 상태 업데이트 로직을 추가할 수 있습니다.
        }
        else
        {
            agent.isStopped=true;
        }
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
