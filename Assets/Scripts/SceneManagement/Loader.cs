using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}