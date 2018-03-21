﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class Smithable : MonoBehaviour, IPointerClickHandler
    {
        private Transform current;
        private Transform parent;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var currentPosition = current.position;

            var parentPos = parent.position;
            parentPos.x = eventData.pointerCurrentRaycast.worldPosition.x;
            parent.position = parentPos;

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