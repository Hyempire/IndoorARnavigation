using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// EmptyGameObject1�� �־��
// MiniMapCamera�� Indicator�� ���󰡰� ����� ��ũ��Ʈ

public class HelloARController : MonoBehaviour
{
    public Camera FirstPersonCamera;        // 1��Ī ī�޶�(ARī�޶�?)
    public GameObject CameraTarget;         // ī�޶� ��ġ ���� Ÿ�� (Indicator)
    private Vector3 PrevARPosePosition;     // ARī�޶� ��ġ
    private bool Tracking = false;

    // Start is called before the first frame update
    void Start()
    {
        // �ʱ� ������ ���
        PrevARPosePosition = Vector3.zero;
    }

    // Update���� ī�޶��� ���� ��ġ�� ���� ��ġ ���� ���̸� ����Ͽ� Indicator ��ġ�� ������Ʈ �ϴ� ���� ���
    void Update()
    {
        // UpdateApplicationLifecycle(); -> �̰� ���� �ϰ͵� ���ٴµ�;;

        // Indicator�� ARī�޶� �����ǿ� ���� �����̰� �����
        Vector3 currentARPosition = FirstPersonCamera.transform.position;     // --------------Frame.Pose.position �ؾߵǴµ� Frame�̶�°� ����
        if (!Tracking)
        {
            Tracking = true;
            PrevARPosePosition = FirstPersonCamera.transform.position;      // --------------Frame.Pose.position �ؾߵǴµ� Frame�̶�°� ����
        }
        // Remember the previous position so we can apply deltas
        Vector3 deltaPosition = currentARPosition - PrevARPosePosition;
        PrevARPosePosition = currentARPosition;
        if (CameraTarget != null)
        {
            // The initial forward vector of the sphere must be aligned with the initial camera direction in the XZ plane.
            // We apply translation only in the XZ plane.
            CameraTarget.transform.Translate(deltaPosition.x, 0.0f, deltaPosition.z);

            // Set the pose rotation to be used in the CameraFollow script : ȭ��ǥ ����
            FirstPersonCamera.GetComponent<ArrowDirection>().targetRot = FirstPersonCamera.transform.rotation;
        }
    }
}
