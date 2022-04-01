[Creating an ARCore powered indoor navigation application in Unity | Raccoons](https://blog.raccoons.be/arcore-powered-indoor-navigation-unity)

위 링크 따라해보기 : QR코드로 초기점 인식해서 AR 실내 내비게이션 만드는 과정

### 0. 세팅

- XR 패키지매니저들 깔고, 빌드 설정 하는건 ARfoundation 기초 유튜브 참고
- Plane에 Map 텍스처 넣고
- Sphere로 Indicator 만들고
- MiniMapCamera 하나 만들어서 Indicator에 링크 (카메라 설정은 사진 참고)

![세팅](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/ed4797bf-1995-4563-bb64-beae1eb6db78/Untitled.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=AKIAT73L2G45EIPT3X45%2F20220401%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20220401T071144Z&X-Amz-Expires=86400&X-Amz-Signature=0345adaabe9a056ddeb17280fc66896f6a5ad7d75c1f8c370b8fff3847610b58&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22Untitled.png%22&x-id=GetObject) 

![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/ed4797bf-1995-4563-bb64-beae1eb6db78/Untitled.png)

### 1. 미니맵 내 인디케이터가 AR카메라 위치에 따라 움직이게 만들기

- HelloARController.cs

  - CameraTarget(Indicator)의 초기 위치를 AR카메라의 초기위치로 설정하고, 그 이후부터는 프레임마다(Update) FirstPersonCamera(ARcamera)가 움직이는 정도(deltaPosition)을 구해서 CameraTarget의 위치를 움직여주는 스크립트

  ```csharp
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
              // FirstPersonCamera.GetComponent<ArrowDirection>().targetRot = FirstPersonCamera.transform.rotation;
          }
      }
  }
  ```

  - EmptyGameObject1에 넣어둠

  - FirstPersonCamera와 CameraTarget에는 각각 AR Camera와 Indicator를 넣어줌

    ![HelloARController](README.assets/Untitled2.png) 

  ![Untitled](https://s3-us-west-2.amazonaws.com/secure.notion-static.com/2ca0f1d7-d940-4442-b6c7-5c1d687f1d47/Untitled.png)

- 코드 설명

  - UpdateApplicationLifecycle(); 를 넣으면 오류 떠서 그냥 지움
  - Frame.Pose.position 의 Frame이 옛날 버전 ARCore에만 있던 클래스라 못씀
    - 그냥 transform 써서 AR카메라의 위치를 가져옴

- 결과

  - AR카메라를 움직임에 따라 Indicator와 자식인 MinimapCamera가 움직임을 볼 수 있다.

    ![결과](https://s3.us-west-2.amazonaws.com/secure.notion-static.com/dd004dc3-1a91-4d51-b1ab-3e0ae41bbec8/arNav1_AdobeCreativeCloudExpress.gif?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Content-Sha256=UNSIGNED-PAYLOAD&X-Amz-Credential=AKIAT73L2G45EIPT3X45%2F20220401%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Date=20220401T080130Z&X-Amz-Expires=86400&X-Amz-Signature=fc94466c48d97b0911a7325e5a920bc45c1263eed50bed2299be5f22781aeb4b&X-Amz-SignedHeaders=host&response-content-disposition=filename%20%3D%22arNav1_AdobeCreativeCloudExpress.gif%22&x-id=GetObject){: width="1280" height="720"}
    

    



---



