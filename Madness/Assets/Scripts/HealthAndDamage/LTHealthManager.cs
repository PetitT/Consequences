﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace LowTeeGames
{
    [AddComponentMenu("LowTee/Health Manager")]
    public class LTHealthManager : MonoBehaviour
    {
        public SpriteRenderer sprite;
        private bool isInvulnerable = false;
        public float invulnerabilityTimer = 1f;

        [SerializeField] private float maxHealth;
        private float currentHealth;

        [System.Serializable] public class OnModifyHealth : UnityEvent<float> { }
        public OnModifyHealth onDamageTaken;
        public OnModifyHealth onHealthRegained;

        private AudioSource src;
        public AudioClip damaged;

        public ParticleSystem particle;

        [System.Serializable]
        public class OnHealthChanged : UnityEvent<float, float>
        {
            internal void AddListener()
            {
                throw new NotImplementedException();
            }
        }
        public OnHealthChanged onHealthChanged;

        public UnityEvent onDeath;

        public static event Action onStaticDamageTaken;

        private void Awake()
        {
            ResetHealth();
            src = GetComponent<AudioSource>();
        }

        private void Start()
        {
            EnemyTether.onStaticKill += GetHealth;
        }

        private void GetHealth(bool heals)
        {
            particle.Play();
            if (heals)
            {
                RegenerateHealth(1);
            }
        }

        private void ResetHealth()
        {
            currentHealth = maxHealth;
            onHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(float damage)
        {
            if (isInvulnerable) { return; }
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);
            onDamageTaken?.Invoke(damage);
            onStaticDamageTaken?.Invoke();
            onHealthChanged?.Invoke(currentHealth, maxHealth);
            StartCoroutine("Flash");
            src.PlayOneShot(damaged);
            if (currentHealth > 0) { return; }
            onDeath?.Invoke();
            StartCoroutine("ResetGame");
        }

        private IEnumerator ResetGame()
        {
            yield return new WaitForSeconds(1.75f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

        private IEnumerator Flash()
        {
            isInvulnerable = true;
            sprite.material.SetFloat("_HitEffectBlend", 1);
            yield return new WaitForSeconds(0.1f);
            sprite.material.SetFloat("_HitEffectBlend", 0);
            yield return new WaitForSeconds(0.75f);
            isInvulnerable = false;
        }
    }
}