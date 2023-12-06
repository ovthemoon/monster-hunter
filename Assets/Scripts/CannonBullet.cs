using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public float splashRadius = 5f; // ���÷��� �������� ����� �ݰ�
    public float damage = 10f; // ���÷��� ������ ��
    public GunMode currentMode;

    public AudioClip boomSound;
    public GameObject effectCannon;
    private bool soundPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        damage = currentMode.damage;
        StartCoroutine(DeleteBullet(4f));
    }

    void OnTriggerEnter(Collider other)
    {
        if (!soundPlayed) // ���尡 ���� ������� �ʾҴٸ�
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(boomSound);
            
            StartCoroutine(DestroyAfterSound(boomSound.length));
            soundPlayed = true; // ���尡 ����Ǿ��ٰ� ǥ��
        }
        GameObject effect = Instantiate(effectCannon,transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        // ���÷��� ������ ����
        ApplySplashDamage(transform.position, splashRadius, damage);

    }
    IEnumerator DestroyAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void ApplySplashDamage(Vector3 center, float radius, float damage)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DecreaseHp(damage);
            }
        }
    }
    IEnumerator DeleteBullet(float aliveTime)
    {
        yield return new WaitForSeconds(aliveTime);
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
