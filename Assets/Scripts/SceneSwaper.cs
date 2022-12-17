using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneSwaper : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;
    [SerializeField] private Transform _nextSceneSpawnPoint;

    public event UnityAction<SceneSwaper> PlayerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            SwapScene(player);
    }

    public void SwapScene(Player player)
    {
        player.transform.position = _nextSceneSpawnPoint.position;
        SceneManager.LoadScene(_nextSceneName);
    }
}
