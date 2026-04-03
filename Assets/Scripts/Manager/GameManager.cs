using UnityEngine;

namespace ProjectTwo.Manager
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}
