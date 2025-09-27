using UnityEngine;

public class PoolParticle : MonoBehaviour
{
    public void ReturnToPool()
    {
        PoolManager.instance.DeactivateObject(this.gameObject);
    }
    
}
