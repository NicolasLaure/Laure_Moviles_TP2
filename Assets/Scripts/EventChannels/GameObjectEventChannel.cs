using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameObjectEventChannel", menuName = "Events/GameObject", order = 0)]
public class GameObjectEventChannel : ScriptableObject
{
    public event UnityAction<GameObject> onGameObjectEvent;

    public void RaiseEvent(GameObject value)
    {
        onGameObjectEvent?.Invoke(value);
    }
}