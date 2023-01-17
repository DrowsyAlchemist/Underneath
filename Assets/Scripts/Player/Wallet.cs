using System;

public class Wallet
{
    private const string SavesFolderName = "Player";
    private const string SaveFileName = "Wallet";

    private static Wallet _instance;
    private int _money;

    public int Money => _money;

    public event Action<int> MoneyChanged;


    private Wallet()
    {
        
    }

    public static Wallet GetLoadOrDefault()
    {
        if (_instance == null)
            _instance = new Wallet();

        _instance.Reset();
        return _instance;
    }

    private void Reset()
    {
        _money = SaveLoadManager.GetLoadOrDefault<int>(SavesFolderName, SaveFileName);
        MoneyChanged?.Invoke(_money);
    }

    public void Save()
    {
        SaveLoadManager.Save(SavesFolderName, SaveFileName, _money);
    }

    public void TakeMoney(int money)
    {
        if (money < 0)
            throw new System.ArgumentOutOfRangeException();

        _money += money;
        MoneyChanged?.Invoke(Money);
    }

    public int GiveMoney(int money)
    {
        if (money < 0)
            throw new System.ArgumentOutOfRangeException();

        if (money > Money)
            throw new System.InvalidOperationException("Not enough money. Check money count first.");

        _money -= money;
        MoneyChanged?.Invoke(Money);
        return money;
    }
}