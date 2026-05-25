using UnityEngine;

public enum NodeType { EnemyUnit, ItemLootBox }

public class SpawnBase : MonoBehaviour
{
    public NodeType nodeType = NodeType.EnemyUnit;
    public float spawnRadius = 5f;

    void OnDrawGizmos()
    {
        if (nodeType == NodeType.EnemyUnit)
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        }
        else if (nodeType == NodeType.ItemLootBox)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        }

        Gizmos.DrawSphere(transform.position, spawnRadius);

        Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 1f);
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
