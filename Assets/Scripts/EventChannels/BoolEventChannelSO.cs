using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Events/Bool", order = 0)]
public class BoolEventChannelSO : ScriptableObject
{
    public event UnityAction<bool> onBoolEvent;

    public void RaiseEvent(bool value)
    {
        onBoolEvent?.Invoke(value);
    }
}