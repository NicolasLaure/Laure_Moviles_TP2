public class PiecesPool : ObjectPool.ObjectPool
{
    public static PiecesPool instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
}