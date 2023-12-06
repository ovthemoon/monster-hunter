using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackMode
{
    CANNON,MACHINEGUN,SNIPER
}
public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f; // 회전 속도
    public GameObject upperBody;
    public GameObject[] gunMode;
    public float changingTime=2f;
    public float minYRotation = 0f; // 최소 Y축 회전 각도
    public float maxYRotation = 30f; // 최대 Y축 회전 각도
    public float maxHp = 100f;
    
    public float currentHp { get; private set; }

    private float currentYRotation = 0f; // 현재 Y축 회전 각도
    public GameObject currentMode { get; private set; }
    public int currentModeIndex { get; private set; } = 0;
    public bool isChanging { get; private set; } = false;
    public bool isDead { get; private set; } = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentMode = gunMode[currentModeIndex];
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChanging)
        {
            MoveTurret();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackModeChange());
            }
        }
        
    }
    void MoveTurret()
    {
        float inputX = Input.GetAxis("Mouse X");
        float inputY = Input.GetAxis("Mouse Y");
        upperBody.transform.Rotate(0, inputX * rotationSpeed * Time.deltaTime, 0);
        currentYRotation -= inputY * rotationSpeed * Time.deltaTime;
        currentYRotation = Mathf.Clamp(currentYRotation, minYRotation, maxYRotation);

        // 수직 회전 적용
        upperBody.transform.localEulerAngles = new Vector3(currentYRotation, upperBody.transform.localEulerAngles.y, 0);
    }
    public void DecreaseHp(float damage)
    {
        currentHp -= damage;
        GameManager.instance.IncreaseDefeatedEnemyCount();
        if (currentHp <= 0)
        {
            currentHp = 0;
            isDead = true;
        }
    }
   
    IEnumerator AttackModeChange()
    {
        isChanging = true;
        gunMode[currentModeIndex].GetComponent<Animator>().SetTrigger("ModeChange");
        gameObject.GetComponent<ShootGunScript>().ResetCooltime();
        yield return new WaitForSeconds(1);
        swapGunMode();
        yield return new WaitForSeconds(1);
        isChanging = false;
    }
    void swapGunMode()
    {

        for(int i=0;i<gunMode.Length; i++)
        {
            gunMode[i].SetActive(false);
        }
        currentModeIndex=(currentModeIndex+1)%gunMode.Length;
        gunMode[currentModeIndex].SetActive(true);
    }
}
