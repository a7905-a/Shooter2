using UnityEngine;

public class LookUI : MonoBehaviour
{
    Camera uiCamera;
    const string MAINCAMERA_STRING = "MainCamera";

    void Start()
    {
        if (uiCamera == null)
        {
            uiCamera = GameObject.FindGameObjectWithTag(MAINCAMERA_STRING).GetComponent<Camera>();
        }
    }

    void Update()
    {
        if (uiCamera!= null)
        {
            transform.LookAt(transform.position + uiCamera.transform.rotation * Vector3.forward, uiCamera.transform.rotation * Vector3.up);
        } 
    }
}
