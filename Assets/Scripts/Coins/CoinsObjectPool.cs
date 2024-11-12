public class CoinsObjectPool : ObjectPool.ObjectPool
{
    public static CoinsObjectPool instance;

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
