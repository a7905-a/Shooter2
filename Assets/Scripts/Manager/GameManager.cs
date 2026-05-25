using UnityEngine;

namespace ProjectTwo.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject player;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }
}
