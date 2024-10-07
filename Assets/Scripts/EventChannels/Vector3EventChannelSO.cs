using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Vector3EventChannel", menuName = "Events/Vector3", order = 0)]
public class Vector3EventChannelSO : ScriptableObject
{
    public event UnityAction<Vector3> onVector3Event;

    public void RaiseEvent(Vector3 point)
    {
        onVector3Event?.Invoke(point);
    }
}