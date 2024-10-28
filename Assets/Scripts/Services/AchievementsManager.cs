using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
#endif
using UnityEngine.SocialPlatforms;

public enum Achievements
{
    TEST_01
}

public class AchievementsManager : MonoBehaviour
{
    public static void UnlockAchievement(AchievementSO achievements)
    {
#if UNITY_ANDROID
        Social.ReportProgress(achievements.playStoreId, 100.0f, (bool success) => { });
#endif
    }
}