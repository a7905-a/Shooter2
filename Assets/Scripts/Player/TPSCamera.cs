using UnityEngine;
using ProjectTwo.Manager;

namespace ProjectTwo.Player
{
    public class TPSCamera : MonoBehaviour
    {
        public static TPSCamera Instance;
        public bool updatingRotation;

        [SerializeField] private Transform cameraFocus;
        [SerializeField, Range(0, 100f)] private float yawSpeed = 50f;
        [SerializeField, Range(0, 100f)] private float pitchSpeed = 50f;
        [SerializeField] private Inputs input;
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
