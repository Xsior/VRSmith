using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Smithable : MonoBehaviour, IPointerClickHandler
    {
        public ParticleSystem sparks;

        private Transform current;
        private Transform parent;

        private void OnCollisionEnter (Collision col)
        {
            if (!col.gameObject.CompareTag("Hammer")) {
                return;
            }

            Debug.Log(col.contacts.Length);

            OnHit(col.contacts[0].point);
        }

        public void OnPointerClick (PointerEventData eventData)
        {
            OnHit(eventData.pointerCurrentRaycast.worldPosition);
        }

        private void OnHit (Vector3 hitPoint)
        {
            var currentPosition = current.position;

            var parentPos = parent.position;
            parentPos.x = hitPoint.x;
            parent.position = parentPos;

            sparks.transform.position = hitPoint;
            sparks.Play();

            current.position = currentPosition;

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