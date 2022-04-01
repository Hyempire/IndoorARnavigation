using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// EmptyGameObject1에 넣어둠
// MiniMapCamera가 Indicator를 따라가게 만드는 스크립트

public class HelloARController : MonoBehaviour
{
    public Camera FirstPersonCamera;        // 1인칭 카메라(AR카메라?)
    public GameObject CameraTarget;         // 카메라 위치 따라갈 타깃 (Indicator)
    private Vector3 PrevARPosePosition;     // AR카메라 위치
    private bool Tracking = false;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 포지션 잡기
        PrevARPosePosition = Vector3.zero;
    }

    // Update에서 카메라의 이전 위치와 현재 위치 간의 차이를 계산하여 Indicator 위치를 업데이트 하는 데에 사용
    void Update()
    {
        // UpdateApplicationLifecycle(); -> 이거 뭐야 암것도 없다는데;;

        // Indicator를 AR카메라 포지션에 따라 움직이게 만들기
        Vector3 currentARPosition = FirstPersonCamera.transform.position;     // --------------Frame.Pose.position 해야되는데 Frame이라는게 없음
        if (!Tracking)
        {
            Tracking = true;
            PrevARPosePosition = FirstPersonCamera.transform.position;      // --------------Frame.Pose.position 해야되는데 Frame이라는게 없음
        }
        // Remember the previous position so we can apply deltas
        Vector3 deltaPosition = currentARPosition - PrevARPosePosition;
        PrevARPosePosition = currentARPosition;
        if (CameraTarget != null)
        {
            // The initial forward vector of the sphere must be aligned with the initial camera direction in the XZ plane.
            // We apply translation only in the XZ plane.
            CameraTarget.transform.Translate(deltaPosition.x, 0.0f, deltaPosition.z);

            // Set the pose rotation to be used in the CameraFollow script : 화살표 방향
            FirstPersonCamera.GetComponent<ArrowDirection>().targetRot = FirstPersonCamera.transform.rotation;
        }
    }
}
