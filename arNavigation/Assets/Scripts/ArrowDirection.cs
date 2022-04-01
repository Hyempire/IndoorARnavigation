using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{

    public Quaternion targetRot;    // AR카메라의 로테이션값. Quaternion은 회전을 표현함

    public GameObject arrow;        // Indicator의 화살표
    private float rotationSmoothingSpeed = 1.0f; // ------------------링크에 없는건데 안쓰면 빨간줄 쳐져서 그냥 써놓음!

    private void LateUpdate()
    {
        Vector3 targetEulerAngles = targetRot.eulerAngles;      // 회전의 오일러각도 Vectoer3(,,,)로 반환
        float rotationToApplyAroundY = targetEulerAngles.y;     // y축 기준의 회전 각도
        float newCamRotAngleY = Mathf.LerpAngle(arrow.transform.eulerAngles.y, rotationToApplyAroundY, rotationSmoothingSpeed * Time.deltaTime);
        Quaternion newCamRotYQuat = Quaternion.Euler(0, newCamRotAngleY, 0);
        arrow.transform.rotation = newCamRotYQuat;
    }


    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
