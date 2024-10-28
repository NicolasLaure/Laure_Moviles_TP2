using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static int currentSceneIndex = 0;
    public static void ChangeScene(int index)
    {
        currentSceneIndex = index;
        SceneManager.LoadScene(index);
    }
}