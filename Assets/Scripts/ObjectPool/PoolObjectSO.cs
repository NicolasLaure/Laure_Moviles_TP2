using UnityEngine;

[CreateAssetMenu(fileName = "PoolObject", menuName = "Pool/Object", order = 0)]
public class PoolObjectSO : ScriptableObject
{
    public GameObject prefab;
}