using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace CellSystem.Common
{
    public class DoubleBuffer
    {
        public RenderTexture Read { get; set; }
        public RenderTexture Write { get; set; }
        
        public DoubleBuffer(int width, int height, int depth, GraphicsFormat format, FilterMode filterMode = FilterMode.Bilinear, TextureWrapMode wrapMode = TextureWrapMode.Clamp)
        {
            Read = new RenderTexture(width, height, depth, format)
            {
                filterMode = filterMode,
                wrapMode = wrapMode
            };
            
            Write = new RenderTexture(width, height, depth, format)
            {
                filterMode = filterMode,
                wrapMode = wrapMode
            };
        }

        ~DoubleBuffer()
        {
            Read.Release();
            Write.Release();
            Read = null;
            Write = null;
        }

        /// <summary>
        /// Swap Buffers
        /// </summary>
        public void Swap()
        {
            (Read, Write) = (Write, Read);
        }
    }
}
