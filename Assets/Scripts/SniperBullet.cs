using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    float damage;
    public GunMode currentMode;
    public GameObject effectSniperOnEnemy;
    public GameObject effectSniperOnGround;
    int penetrateCount = 0;
    int maximumPenetrate = 2;
    // Start is called before the first frame update
    void Start()
    {
        damage = currentMode.damage;
        StartCoroutine(DeleteBullet(2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>())
            {
                other.GetComponent<Enemy>().DecreaseHp(damage);
            }
            else if(other.GetComponent<DragonScript>()) {
                other.GetComponent<DragonScript>().DecreaseHp(damage);
            }
            GameObject effect = Instantiate(effectSniperOnEnemy, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            penetrateCount++;
            if (penetrateCount >= maximumPenetrate)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            GameObject effect = Instantiate(effectSniperOnGround, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
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
