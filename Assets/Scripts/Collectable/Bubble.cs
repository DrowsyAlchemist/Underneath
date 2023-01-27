using UnityEngine;

public abstract class Bubble : Collectable, ISaveable
{
    [SerializeField] private string _id;

    private const string SavesFolderName = "Bubbles";
    private bool _isCollected;

    protected sealed override void Start()
    {
        base.Start();
        _isCollected = SaveLoadManager.GetLoadOrDefault<bool>(SavesFolderName, _id);

        if (_isCollected)
            Destroy(gameObject);
    }

    public void Save()
    {
        SaveLoadManager.Save(SavesFolderName, _id, _isCollected);
    }

    protected sealed override void CollectByPlayer(Player player)
    {
        _isCollected = true;
        Collect(player);
    }

    protected abstract void Collect(Player player);
}