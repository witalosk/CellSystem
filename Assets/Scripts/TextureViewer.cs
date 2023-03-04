using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CellSystem
{
    public class TextureViewer : MonoBehaviour, IMousePositionProvider
    {
        public Vector3 MousePosition => _mousePosition;

        private Vector3 _mousePosition = new();
        private IRenderTexture _source;
        private RawImage _rawImage;
        
        private void Start()
        {
            _source = FindObjectsOfType<Component>().FirstOrDefault(c => c is IRenderTexture) as IRenderTexture;
            _rawImage = GetComponentInChildren<RawImage>();
        }

        private void Update()
        {
            if (_rawImage.texture == null)
            {
                _rawImage.texture = _source.Texture;
            }
        }

        public void OnPointerDown(BaseEventData e)
        {
            RectTransform rt = _rawImage.transform as RectTransform;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, (e as PointerEventData).position, Camera.main, out Vector2 lp))
            {
                Vector2 cood = lp / rt.rect.size + rt.pivot;
                _mousePosition.x = cood.x;
                _mousePosition.y = cood.y;
                _mousePosition.z = 1f;
            }
        }

        public void OnPointerMove(BaseEventData e)
        {
            RectTransform rt = _rawImage.transform as RectTransform;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, (e as PointerEventData).position, Camera.main, out Vector2 lp))
            {
                Vector2 cood = lp / rt.rect.size + rt.pivot;
                _mousePosition.x = cood.x;
                _mousePosition.y = cood.y;
            }
        }

        public void OnPointerUp(BaseEventData e)
        {
            RectTransform rt = _rawImage.transform as RectTransform;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, (e as PointerEventData).position, Camera.main, out Vector2 lp))
            {
                Vector2 cood = lp / rt.rect.size + rt.pivot;
                _mousePosition.x = cood.x;
                _mousePosition.y = cood.y;
                _mousePosition.z = 0f;
            }
        }
    }
    
}
