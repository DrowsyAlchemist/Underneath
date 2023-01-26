using System;
using System.Collections.Generic;
using UnityEngine;

public class ActivePotions
{
    private const string SaveFolderName = "Player";
    private const string SaveFileName = "ActivePotionsList";
    private const string PotionsFolder = "Items";

    private static ActivePotions _instance;
    private readonly Player _player;
    private readonly List<Potion> _potions = new();

    public event Action<Potion> PotionAdded;
    public event Action Cleared;

    private ActivePotions(Player player)
    {
        _player = player;
    }

    public void Save()
    {
        if (_potions.Count > 0)
        {
            List<string> activePotions = new();

            foreach (var activePotion in _potions)
                activePotions.Add(activePotion.Data.SaveFileName);

            SaveLoadManager.Save(SaveFolderName, SaveFileName, activePotions);
        }
    }

    public static ActivePotions LoadLastSaveOrDefault(Player player)
    {
        if (player == null)
            throw new ArgumentNullException();

        if (_instance == null)
            _instance = new ActivePotions(player);

        _instance.Reset();
        return _instance;
    }

    private void Reset()
    {
        Clear();
        List<string> activePotions = SaveLoadManager.GetLoadOrDefault<List<string>>(SaveFolderName, SaveFileName);

        if (activePotions == null)
            return;

        foreach (string potionName in activePotions)
        {
            string localPath = PotionsFolder + "/" + potionName;
            Potion potionTemplate = Resources.Load<Potion>(localPath);

            if (potionTemplate == null)
                throw new Exception($"Can't find valid item on path: " + localPath);

            var potion = UnityEngine.Object.Instantiate(potionTemplate, _player.transform);

            if (potion is ExtraHeartPotion)
                potion.SetAffecting();
            else
                potion.ApplyEffect(AccessPoint.Player);

            AddPotion(potion);
        }
    }

    private void Clear()
    {
        int i = 0;

        while (i < _potions.Count)
        {
            if (_potions[i] is not ExtraHeartPotion)
                _potions[i].CancelEffect(_player);
            else
                i++;
        }
        _potions.Clear();
        Cleared?.Invoke();
    }

    public Potion[] GetActivePotions()
    {
        return _potions.ToArray();
    }

    public void AddPotion(Potion potion)
    {
        _potions.Add(potion);
        potion.AffectingFinished += OnAffectingFinished;
        PotionAdded?.Invoke(potion);
    }

    private void OnAffectingFinished(AffectingItem affectingItem)
    {
        _potions.Remove(affectingItem as Potion);
    }
}