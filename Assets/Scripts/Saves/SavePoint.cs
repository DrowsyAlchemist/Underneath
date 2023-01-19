using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SavePoint : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public Transform SpawnPoint => _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            foreach (var gameObj in FindObjectsOfType<GameObject>(includeInactive: true))
                if (gameObj.TryGetComponent(out ISaveable saveable))
                    saveable.Save();
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}