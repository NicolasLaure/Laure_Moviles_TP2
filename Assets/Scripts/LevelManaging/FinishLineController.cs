using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO onFinishLineTouch;

    [SerializeField] private float contactTimeToWin = 0;
    [SerializeField] private float touchDuration = 0;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PieceController>().IsFalling)
        {
            Debug.Log("Piece was Falling");
            return;
        }

        touchDuration += Time.deltaTime;
        if (touchDuration >= contactTimeToWin)
            onFinishLineTouch?.RaiseEvent();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        touchDuration = 0;
    }
}