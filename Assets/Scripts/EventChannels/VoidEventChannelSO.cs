using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Events/Void", order = 0)]
public class VoidEventChannelSO : ScriptableObject
{
    public event UnityAction onVoidEvent;

    public void RaiseEvent()
    {
        onVoidEvent?.Invoke();
    }
}