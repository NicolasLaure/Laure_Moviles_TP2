using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private IntEventChannelSO onCoinsAdd;
    [SerializeField] private VoidEventChannelSO onCoinsPicked;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PieceController>().IsFalling)
            return;

        onCoinsAdd.RaiseEvent(value);
        onCoinsPicked.RaiseEvent();
        gameObject.SetActive(false);
    }
}