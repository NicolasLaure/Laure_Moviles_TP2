using System;
using System.Collections;
using UnityEngine;

namespace SplashScreen
{
    public class SplashScreenAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationClip splashAnimation;

        private void Start()
        {
            StartCoroutine(WaitAnimationFinishCoroutine());
        }

        private IEnumerator WaitAnimationFinishCoroutine()
        {
            yield return new WaitForSeconds(splashAnimation.length);
            Loader.ChangeScene(1);
        }
    }
}