using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public enum Achievements
{
    TEST_01
}

public class AchievementsManager : MonoBehaviour
{
    public static void UnlockAchievement(AchievementSO achievement)
    {
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.ReportProgress(achievement.playStoreId, 100.0f, (bool success) => { });
#endif
    }
}