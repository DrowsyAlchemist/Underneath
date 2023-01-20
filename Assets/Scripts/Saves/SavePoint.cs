using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SavePoint : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    public Vector2 SpawnPoint => _spawnPoint.position;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            foreach (var gameObjects in FindObjectsOfType<GameObject>(includeInactive: true))
                if (gameObjects.TryGetComponent(out ISaveable saveable))
                    saveable.Save();
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}