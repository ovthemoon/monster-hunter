using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public GunMode currentMode;
    public GameObject effectMachinegun;
    // Start is called before the first frame update
    void Start()
    {
        damage = currentMode.damage;
        StartCoroutine(DeleteBullet(2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) {
            if (other.GetComponent<Enemy>())
            {
                other.GetComponent<Enemy>().DecreaseHp(damage);
            }
            else if (other.GetComponent<DragonScript>())
            {
                other.GetComponent<DragonScript>().DecreaseHp(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GameObject effect = Instantiate(effectMachinegun, transform.position, Quaternion.identity);
        Destroy(effect, 1f);


    }
    IEnumerator DeleteBullet(float aliveTime)
    {
        yield return new WaitForSeconds(aliveTime);
        if(gameObject!=null)
        {
            Destroy(gameObject);
        }
    }
}
