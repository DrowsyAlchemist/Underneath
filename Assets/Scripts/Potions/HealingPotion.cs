using UnityEngine;

public class HealingPotion : Potion
{
    [SerializeField] private int _healthPoints = 2;

    private PlayerHealth _playerHealth;

    public override void Init (Player player)
    {
        _playerHealth = player.GetComponent<PlayerHealth>();

        if (_playerHealth == null)
            throw new System.Exception("Player hasn't got a PlayerHealth.");
    }

    public override void Use()
    {
        _playerHealth.RestoreHealth(_healthPoints);
    }
}
