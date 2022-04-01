using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirection : MonoBehaviour
{

    public Quaternion targetRot;    // ARī�޶��� �����̼ǰ�. Quaternion�� ȸ���� ǥ����

    public GameObject arrow;        // Indicator�� ȭ��ǥ
    private float rotationSmoothingSpeed = 1.0f; // ------------------��ũ�� ���°ǵ� �Ⱦ��� ������ ������ �׳� �����!

    private void LateUpdate()
    {
        Vector3 targetEulerAngles = targetRot.eulerAngles;      // ȸ���� ���Ϸ����� Vectoer3(,,,)�� ��ȯ
        float rotationToApplyAroundY = targetEulerAngles.y;     // y�� ������ ȸ�� ����
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
