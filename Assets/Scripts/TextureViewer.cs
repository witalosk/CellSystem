using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CellSystem
{
    public class TextureViewer : MonoBehaviour
    {
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
    }
    
}
