using UnityEngine;

namespace CellSystem
{
    public interface IMousePositionProvider
    {
        Vector3 MousePosition { get; }
    }
}