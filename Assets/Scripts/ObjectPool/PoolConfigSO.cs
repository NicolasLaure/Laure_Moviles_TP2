using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolConfig", menuName = "Pool/Config", order = 0)]
public class PoolConfigSO : ScriptableObject
{
    public List<PoolObjectSO> poolObjects;
    public int objectCount;
}