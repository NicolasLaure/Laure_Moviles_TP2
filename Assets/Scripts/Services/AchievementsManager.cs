using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
#endif

public class AchievementsManager : MonoBehaviour
{
    public static void UnlockAchievement(AchievementSO achievement)
    {
#if UNITY_ANDROID
        Social.ReportProgress(achievement.playStoreId, 100.0f, (bool success) => { });
#endif
    }

    public static void ResetAchievement(AchievementSO achievement)
    {
#if UNITY_ANDROID
        Social.ReportProgress(achievement.playStoreId, 0.0f, (bool success) => { });
#endif
    }
}