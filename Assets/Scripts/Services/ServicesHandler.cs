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
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(bool succeded)
    {
        
        Loader.ChangeScene(Loader.currentSceneIndex + 1);
    }
#endif
}