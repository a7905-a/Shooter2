using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    [SerializeField] Transform cameraFocus;
    [SerializeField, Range(0, 100f)] float yawSpeed = 50f;
    [SerializeField, Range(0, 100f)] float pitchSpeed = 50f;
    [SerializeField] Inputs input;
    float yaw = 0;
    float pitch = 0;
    float maxPitch = 35f;
    
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        yaw += input.look.x * yawSpeed * Time.deltaTime;
        pitch += input.look.y * pitchSpeed *  Time.deltaTime;
        float clampPitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

        cameraFocus.transform.rotation = Quaternion.Euler(clampPitch, yaw, 0f);
    }
}
