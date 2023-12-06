using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    LineRenderer lineRenderer; // LineRenderer ������Ʈ ����
    public Transform shootPoint; // �ѱ��� ��ġ
    public Transform pointEnd;

    private void Start()
    {
        lineRenderer=GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, shootPoint.position); // ������ ����
        lineRenderer.SetPosition(1, pointEnd.position); // ���� ����
        lineRenderer.enabled = true; // LineRenderer Ȱ��ȭ
    }
}
