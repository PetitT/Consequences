using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
    [AddComponentMenu("LowTee/Health Manager")]
    public class LTHealthManager : MonoBehaviour
    {
        [SerializeField] private float maxHealth;
        private float currentHealth;

        [System.Serializable] public class OnModifyHealth : UnityEvent<float> { }
        public OnModifyHealth onDamageTaken;
        public OnModifyHealth onHealthRegained;

        [System.Serializable] public class OnHealthChanged : UnityEvent<float, float> { }
        public OnHealthChanged onHealthChanged;

        public UnityEvent onDeath;

        public static event Action onStaticDamageTaken;

        private void Awake()
        {
            ResetHealth();
        }

        private void ResetHealth()
        {
            currentHealth = maxHealth;
            onHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            onDamageTaken?.Invoke(damage);
            onStaticDamageTaken?.Invoke();
            onHealthChanged?.Invoke(currentHealth, maxHealth);
            if (currentHealth > 0) { return; }
            onDeath?.Invoke();
        }

        public void RegenerateHealth(float health)
        {
            currentHealth += health;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            onHealthRegained?.Invoke(health);
            onHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void IncreaseMaxHealth(float healthToAdd)
        {
            maxHealth += healthToAdd;
            RegenerateHealth(healthToAdd);
            onHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void DecreaseMaxHealth(float healthToRemove)
        {
            maxHealth -= healthToRemove;

            float surplus = currentHealth - maxHealth;
            if (surplus > 0) { TakeDamage(surplus); }
            onHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}