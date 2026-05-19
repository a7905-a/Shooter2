using UnityEngine;

namespace ProjectTwo.Manager
{
    public class LookUI : MonoBehaviour
    {
        private Camera uiCamera;
        const string MAINCAMERA_STRING = "MainCamera";

        private void Start()
        {
            if (uiCamera == null)
            {
                uiCamera = GameObject.FindGameObjectWithTag(MAINCAMERA_STRING).GetComponent<Camera>();
            }
        }

        private void Update()
        {
            if (uiCamera!= null)
            {
                transform.LookAt(transform.position + uiCamera.transform.rotation * Vector3.forward, uiCamera.transform.rotation * Vector3.up);
            } 
        }
    }
}

