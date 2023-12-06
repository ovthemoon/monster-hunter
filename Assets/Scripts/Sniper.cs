using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    LineRenderer lineRenderer; // LineRenderer 컴포넌트 참조
    public Transform shootPoint; // 총구의 위치
    public Transform pointEnd;

    private void Start()
    {
        lineRenderer=GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, shootPoint.position); // 시작점 설정
        lineRenderer.SetPosition(1, pointEnd.position); // 끝점 설정
        lineRenderer.enabled = true; // LineRenderer 활성화
    }
}
