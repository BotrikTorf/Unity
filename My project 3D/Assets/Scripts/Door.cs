using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public event UnityAction<bool> EventsAction;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Player _))
        {
            EventsAction?.Invoke(true);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out Player _))
        {
            EventsAction?.Invoke(false);
        }
    }
}
