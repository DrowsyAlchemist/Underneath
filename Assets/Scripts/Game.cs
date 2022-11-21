using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Player Player => _player;
}
