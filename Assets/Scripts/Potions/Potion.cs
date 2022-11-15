using UnityEngine;

public abstract class Potion : MonoBehaviour
{
    [SerializeField] protected Player Player;

    public virtual void Init(Player player)
    {
        Player = player;
    }

    public abstract void Use();
}
