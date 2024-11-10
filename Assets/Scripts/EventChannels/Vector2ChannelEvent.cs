using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Vector2EventChannel", menuName = "Events/Vector2", order = 0)]
public class Vector2ChannelEvent : VoidEventChannelSO
{
    public event UnityAction<Vector2> onVector2Event;

    public void RaiseEvent(Vector2 position)
    {
        onVector2Event?.Invoke(position);
    }
}