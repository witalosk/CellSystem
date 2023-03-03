using System;
using CellSystem.Common;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace CellSystem
{
    public class ReactionDiffusion : MonoBehaviour, IRenderTexture
    {
        public RenderTexture Texture => _materialBuffer.Read;

        [SerializeField]
        private Vector2Int _resolution = new(512, 512);

        [SerializeField]
        private int _iterationNum = 10;

        [SerializeField]
        private float _diffusionU = 1.0f;

        [SerializeField]
        private float _diffusionV = 0.5f;

        [SerializeField]
        private float _feed = 0.037f;

        [SerializeField]
        private float _kill = 0.062f;

        [SerializeField]
        private Shader _initShader;

        [SerializeField]
        private Shader _updateShader;

        private DoubleBuffer _materialBuffer;
        private Material _initMat;
        private Material _updateMat;
     
        private void Start()
        {
            _materialBuffer = new DoubleBuffer(_resolution.x, _resolution.y, 0, GraphicsFormat.R32G32_SFloat, FilterMode.Point);
            _initMat = new Material(_initShader);
            _updateMat = new Material(_updateShader);
            
            // Init Buffer
            Graphics.Blit(_materialBuffer.Read, _materialBuffer.Write, _initMat);
            _materialBuffer.Swap();
        }

        private void OnDestroy()
        {
            _materialBuffer = null;
            Destroy(_initMat);
            Destroy(_updateMat);
        }

        private void Update()
        {
            _updateMat.SetFloat("_DiffusionU", _diffusionU);
            _updateMat.SetFloat("_DiffusionV", _diffusionV);
            _updateMat.SetFloat("_Feed", _feed);
            _updateMat.SetFloat("_Kill", _kill);

            for (int i = 0; i < _iterationNum; i++)
            {
                Graphics.Blit(_materialBuffer.Read, _materialBuffer.Write, _updateMat);
                _materialBuffer.Swap();
            }
        }

    }

}

