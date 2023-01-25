using System.Collections.Generic;
using UnityEngine;

public class ActivePotionsView : MonoBehaviour, ISaveable
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private ActivePotionRenderer _activePotionRendererTemplate;

    private const string SaveFolderName = "Player";
    private const string SaveFileName = "ActivePotionsList";
    private const string PotionsFolder = "Items";

    private void OnEnable()
    {
        List<string> activePotions = SaveLoadManager.GetLoadOrDefault<List<string>>(SaveFolderName, SaveFileName);

        if (activePotions != null)
        {
            foreach (string potionName in activePotions)
            {
                string localPath = PotionsFolder + "/" + potionName;
                Potion potionTemplate = Resources.Load<Potion>(localPath);

                if (potionTemplate == null)
                    throw new System.Exception($"Can't find valid item on path: " + localPath);

                var potion = Instantiate(potionTemplate);

                if (potion is ExtraHeartPotion)
                    potion.SetAffecting();
                else
                    potion.ApplyEffect(AccessPoint.Player);

                SetPotion(potion);
            }
        }
    }

    public void Save()
    {
        if (_container.childCount > 0)
        {
            List<string> activePotions = new();

            foreach (var activePotion in _container.GetComponentsInChildren<ActivePotionRenderer>())
                activePotions.Add(activePotion.Potion.Data.SaveFileName);

            SaveLoadManager.Save(SaveFolderName, SaveFileName, activePotions);
        }
    }

    public void SetPotion(Potion potion)
    {
        var activePotionRenderer = Instantiate(_activePotionRendererTemplate, _container);
        activePotionRenderer.Render(potion);
    }
}