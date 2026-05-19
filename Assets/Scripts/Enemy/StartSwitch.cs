using UnityEngine;

namespace ProjectTwo.Enemy
{
    public class StartSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject stageDoor;
        const string PLAYER_STRING = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PLAYER_STRING))
            {
                stageDoor.SetActive(false);
                Destroy(this.gameObject);
            }
        }
    }
}

