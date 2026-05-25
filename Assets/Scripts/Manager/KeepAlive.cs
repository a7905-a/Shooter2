using UnityEngine;

namespace ProjectTwo.Manager
{
    public class KeepAlive : MonoBehaviour
    {
        private void Awake()
        {
            if (GameObject.Find(gameObject.name) != gameObject)
            {
                Destroy(gameObject);
                return;
            }
                DontDestroyOnLoad(gameObject);
        }
    }

}


