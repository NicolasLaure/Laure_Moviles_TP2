using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Vec2FloatEvent", menuName = "Events/Vec2Float", order = 0)]
public class Vec2FloatEvent : VoidEventChannelSO
{
    public event UnityAction<Vector2, float> onVector2FloatEvent;

    public void RaiseEvent(Vector2 position, float time)
    {
        onVector2FloatEvent?.Invoke(position, time);
    }
}