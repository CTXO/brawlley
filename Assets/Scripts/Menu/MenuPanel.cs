using UnityEngine;
using UnityEngine.Events;

namespace Brawlley
{
    public class MenuPanel : MonoBehaviour
    {
        public UnityEvent onOpen = new();
        public UnityEvent onClose = new();

        public void Open() => onOpen.Invoke();
        public void Close() => onClose.Invoke();
    }
}
