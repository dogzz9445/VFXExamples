using UnityEngine;
using UnityEngine.Events;

namespace Mini.Common.Trigger
{
    public class Trigger_SceneLoaded : MonoBehaviour
    {
        public UnityEvent SceneLoaded;
        public UnityEvent SceneUnloaded;

        private void OnEnable()
        {
            SceneLoaded?.Invoke();
        }

        private void OnDisable()
        {
            SceneUnloaded?.Invoke();
        }
    }
}
