using UnityEngine;

public class DamageMesh : MonoBehaviour {
    [SerializeField]
    private int damage = 1;
    private void OnCollisionEnter(Collision collision) {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null) {
            damageable.Damage(damage);
        }
    }
}
