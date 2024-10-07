using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntEventChannel", menuName = "Events/Int", order = 0)]
public class IntEventChannelSO : ScriptableObject
{
    public event UnityAction<int> onIntEvent;

    public void RaiseEvent(int value)
    {
        onIntEvent?.Invoke(value);
    }
}