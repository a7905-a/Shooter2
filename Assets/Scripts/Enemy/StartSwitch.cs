using UnityEngine;

public class StartSwitch : MonoBehaviour
{
    [SerializeField] GameObject stageDoor;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageDoor.SetActive(false);
        }
    }
}
