


using UnityEngine;

namespace DodgyOrb
{
    public interface IDraggable
    {
        public void GetDragged(Vector2 position);
        public void GetReseted();
    }
}
