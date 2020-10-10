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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out LTDamageDealer damageDealer)) { return; }

            float damage = damageDealer.damage;
            onDamageTaken?.Invoke(damage);
        }
    }
}