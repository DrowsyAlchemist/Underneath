using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwaper : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;
    [SerializeField] private Transform _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.transform.position = _spawnPoint.position;
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
