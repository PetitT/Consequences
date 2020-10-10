using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
    [AddComponentMenu("LowTee/Damageable")]
    public class LTDamageable : MonoBehaviour
    {
        [System.Serializable] public class OnDamageTakenEvent : UnityEvent<float> { }
        public OnDamageTakenEvent onDamageTaken;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out LTDamageDealer damageDealer)) { return; }
            float damage = damageDealer.damage;
            if (collision.CompareTag("Projectile"))
            {
                collision.gameObject.SetActive(false);
            }
            onDamageTaken?.Invoke(damage);
        }
    }
}