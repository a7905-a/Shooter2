using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Player
{
    public class TPSCamera : MonoBehaviour
    {
        public static TPSCamera Instance;


        [Header("카메라 참조")]
        [SerializeField] private Transform cameraFocus;


        [Header("스크립트 참조")]
        [SerializeField] private Inputs input;


        [Header("카메라 회전 속도")]
        [SerializeField, Range(0, 100f)] private float yawSpeed = 50f;
        [SerializeField, Range(0, 100f)] private float pitchSpeed = 50f;


        [Header("카메라 상태")]
        public bool updatingRotation;


        // ==========================================
        // 카메라 회전 변수
        // ==========================================
        private float yaw = 0;
        private float pitch = 0;
        private float maxPitch = 35f;
        
        
        private void Awake()
        {
            Instance = this;
        }

        private void LateUpdate()
        {
            LookAround();
        }

        private void LookAround()
        {
            if (updatingRotation) return;

            yaw += input.look.x * yawSpeed * Time.deltaTime;
            pitch += input.look.y * pitchSpeed *  Time.deltaTime;
            
            //위, 아래의 각도를 제한해서 뒤로 넘어가지 않도록 설정
            pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

            cameraFocus.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
