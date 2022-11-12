using UnityEngine;

public class Enemy: MonoBehaviour
{
    [SerializeField] private Player _target;

    public Player Target => _target;
}
