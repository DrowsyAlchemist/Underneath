using UnityEngine;
using UnityEngine.Events;

public class DestroyTrigger : MonoBehaviour
{
    public UnityEvent Destroyed;

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }
}
