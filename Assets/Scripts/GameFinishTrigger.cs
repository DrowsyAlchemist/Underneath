using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private Transform _targetParent;
    [SerializeField] private Transform _targetPosition;

    public UnityEvent GameFinished;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out Player player))
        {
            player.PlayerMovement.AllowInpupControl(false);
            player.transform.SetParent(_targetParent);
            player.transform.position = _targetPosition.position;
            GameFinished?.Invoke();
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
