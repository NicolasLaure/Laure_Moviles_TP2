using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievements/New Achievement", order = 0)]
public class AchievementSO : ScriptableObject
{
    public Achievements achievement;
    public string playStoreId;
}
