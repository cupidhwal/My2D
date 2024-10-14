using UnityEngine;

namespace My2D
{
    // 플레이어 이동에 따른 시차효과 거리 구하기
    public class ParallaxEffect : MonoBehaviour
    {
        // 필드
        #region Variables
        public Camera mainCamera;               // 카메라
        public Transform followTarget;      // 플레이어

        // 시작 위치
        private Vector2 startingPosition;   // 시작 위치 (배경, 카메라)
        private float startingZ;            // 시작할 때 배경의 Z축 위치값
        #endregion

        // 속성
        #region Properties
        // 시작지점으로부터 카메라가 있는 위치까지의 거리
        private Vector2 CamMoveSinceStart => startingPosition - (Vector2)mainCamera.transform.position;

        // 배경과 플레이어와의 z축 거리
        private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
        private float ClippingPlane => mainCamera.transform.position.z + (zDistanceFromTarget > 0 ? mainCamera.farClipPlane : mainCamera.nearClipPlane);
        
        // 시차거리 Factor
        private float ParallaxFactor => Mathf.Abs(zDistanceFromTarget) / ClippingPlane;
        #endregion

        // 라이프 사이클
        #region Life Cycle
        private void Start()
        {
            // 초기화
            startingPosition = transform.position;
            startingZ = transform.position.z;
        }

        private void Update()
        {
            Vector2 newPosition = startingPosition + CamMoveSinceStart * ParallaxFactor;
            transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
        }
        #endregion
    }
}