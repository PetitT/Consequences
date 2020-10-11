using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LowTeeGames
{
    [AddComponentMenu("LowTee/Collision Callback")]
    public class LTCollisionCallback : MonoBehaviour
    {
        [System.Serializable] public class OnTriggerEvent : UnityEvent<Collider2D> { }
        public OnTriggerEvent onTriggerEnterEvent;

        public OnTriggerEvent onTriggerExitEvent;

        public OnTriggerEvent onTriggerStayEvent;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            onTriggerEnterEvent?.Invoke(collider);
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            onTriggerStayEvent?.Invoke(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            onTriggerExitEvent?.Invoke(collider);
        }
    }
}