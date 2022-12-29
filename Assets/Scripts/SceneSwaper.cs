using UnityEngine;

public class SceneSwaper : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;
    [SerializeField] private Transform _nextSceneSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
            SceneLoader.LoadScene(_nextSceneName, _nextSceneSpawnPoint.position);
    }
}
