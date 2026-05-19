using UnityEngine;
using ProjectTwo.Player;

namespace ProjectTwo.Enemy
{
    public class Projectille : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private GameObject projectileHitVFX;
        private int damage;
        private Rigidbody rb;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.linearVelocity = transform.forward * speed;
        }

        public void Init(int damage)
        {
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(damage);

            Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
