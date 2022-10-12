using UnityEngine;
using UnityEngine.Events;

namespace DodgyOrb.RemoteControls
{
    public class RemoteController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector2> OnDataSent;

        public void SendData(Vector2 data)
        {
            OnDataSent.Invoke(data);
        }
    }
}
