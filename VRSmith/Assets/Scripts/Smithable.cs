﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Smithable : MonoBehaviour, IPointerClickHandler
    {
        public ParticleSystem particles;

        private Transform current;
        private Transform parent;

        public void OnPointerClick (PointerEventData eventData)
        {
            OnHit(eventData.pointerCurrentRaycast.worldPosition,1.0f);
        }

        private void OnCollisionEnter (Collision col)
        {
            if (!col.gameObject.CompareTag("Hammer")) {
                return;
            }

            var hammerRb = col.transform.parent.GetComponent<Rigidbody>();
            OnHit(col.contacts[0].point, hammerRb.velocity.magnitude);
        }

        private void OnHit (Vector3 hitPoint,float velocity)
        {
            var currentPosition = current.position;

            var parentPos = parent.position;
            parentPos.x = hitPoint.x;
            parent.position = parentPos;

            current.position = currentPosition;

            particles.transform.position = hitPoint;
            particles.Play();

            Debug.Log("Velocity: " + velocity);
            if (velocity < 1)
            {
                return;
            }

            var parentScale = parent.localScale;

            parentScale.y -= (0.01f*velocity);
            parentScale.x += (0.015f*velocity);
            parent.localScale = parentScale;
        }

        private void Awake()
        {
            current = transform;
            parent = current.parent;
        }
    }
}