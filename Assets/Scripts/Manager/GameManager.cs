using UnityEngine;

namespace ProjectTwo.Manager
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}
