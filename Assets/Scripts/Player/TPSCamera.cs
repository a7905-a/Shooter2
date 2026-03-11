using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public static TPSCamera Instance;
    public bool updatingRotation;

    [SerializeField] Transform cameraFocus;
    [SerializeField, Range(0, 100f)] float yawSpeed = 50f;
    [SerializeField, Range(0, 100f)] float pitchSpeed = 50f;
    [SerializeField] Inputs input;
    float yaw = 0;
    float pitch = 0;
    float maxPitch = 35f;
    
    void Awake()
    {
        Instance = this;
    }

    void LateUpdate()
    {
        LookAround();
    }

    void LookAround()
    {
        if (updatingRotation) return;

        yaw += input.look.x * yawSpeed * Time.deltaTime;
        pitch += input.look.y * pitchSpeed *  Time.deltaTime;
        
        //위, 아래의 각도를 제한해서 뒤로 넘어가지 않도록 설정
        float clampPitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

        cameraFocus.transform.rotation = Quaternion.Euler(clampPitch, yaw, 0f);
    }
}
