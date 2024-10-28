using UnityEngine;
#if UNITY_ANDROID
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif

public class ServicesHandler : MonoBehaviour
{
#if UNITY_ANDROID
    public void Start()
    {
        if (Loader.currentSceneIndex != 1)
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            Loader.ChangeScene(2);
        }
        else
        {
            if (Loader.currentSceneIndex != 1)
                Loader.ChangeScene(1);
        }
    }

    public void ManualLog()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }
#endif
}