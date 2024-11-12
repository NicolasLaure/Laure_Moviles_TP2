using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FinishLineController : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onLevelWin;
    [SerializeField] private BoolEventChannelSO onTouch;

    [SerializeField] private float contactTimeToWin = 0;
    [SerializeField] private float touchDuration = 0;

    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private float resetWait = 1;
    private Coroutine _waitCoroutine;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PieceController>().IsFalling)
            return;

        onTouch.RaiseEvent(true);

        touchDuration += Time.deltaTime;
        HandleTimer();
        if (touchDuration >= contactTimeToWin)
        {
            onLevelWin?.RaiseEvent();
            Time.timeScale = 1;
        }

        TimerText.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (touchDuration == 0)
            return;

        if (_waitCoroutine != null)
            StopCoroutine(_waitCoroutine);

        _waitCoroutine = StartCoroutine(ResetPlayerSpawn());
        touchDuration = 0;
        Time.timeScale = 1;
        TimerText.gameObject.SetActive(false);
    }

    private IEnumerator ResetPlayerSpawn()
    {
        yield return new WaitForSeconds(resetWait);
        onTouch.RaiseEvent(false);
    }

    private void HandleTimer()
    {
        int timer = (int)(contactTimeToWin - touchDuration);
        TimerText.text = timer.ToString();

        Time.timeScale = Mathf.Clamp(1f - touchDuration / contactTimeToWin, 0.5f, 1);
        if (!TimerText.gameObject.activeInHierarchy)
            TimerText.gameObject.SetActive(true);
    }
}