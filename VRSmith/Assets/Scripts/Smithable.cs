using UnityEngine;
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
            OnHit(eventData.pointerCurrentRaycast.worldPosition);
        }

        private void OnCollisionEnter (Collision col)
        {
            if (!col.gameObject.CompareTag("Hammer")) {
                return;
            }

            OnHit(col.contacts[0].point);
        }

        private void OnHit (Vector3 hitPoint)
        {
            var currentPosition = current.position;

            var parentPos = parent.position;
            parentPos.x = hitPoint.x;
            parent.position = parentPos;

            current.position = currentPosition;

            particles.transform.position = hitPoint;
            particles.Play();

            var parentScale = parent.localScale;
            parentScale.y *= 0.95f;
            parentScale.x *= 1.11f;
            parent.localScale = parentScale;
        }

        private void Awake()
        {
            current = transform;
            parent = current.parent;
        }
    }
}