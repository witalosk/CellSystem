using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace CellSystem.Common
{
    public class DoubleBuffer
    {
        public RenderTexture Read { get; set; }
        public RenderTexture Write { get; set; }
        
        public DoubleBuffer(int width, int height, int depth, GraphicsFormat format, FilterMode filterMode = FilterMode.Bilinear)
        {
            Read = new RenderTexture(width, height, depth, format)
            {
                filterMode = filterMode
            };
            
            Write = new RenderTexture(width, height, depth, format)
            {
                filterMode = filterMode
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
