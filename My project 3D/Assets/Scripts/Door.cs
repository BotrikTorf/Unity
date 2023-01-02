using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public event UnityAction<bool> PassedThief;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player _))
            PassedThief?.Invoke(true);
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Player _))
            PassedThief?.Invoke(false);
    }
}
