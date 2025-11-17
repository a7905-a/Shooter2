using UnityEngine;

public class StartSwitch : MonoBehaviour
{
    [SerializeField] GameObject stageDoor;
    const string PLAYER_STRING = "Player";
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            stageDoor.SetActive(false);
        }
    }
}
