using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CollectableMovement : MonoBehaviour
{
    [SerializeField] private float _collectingDuration = 0.33f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    public void MoveToCollector(Player player)
    {
        StartCoroutine(MoveToPlayer(player));
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        float elapsedTime = 0;
        float t;

        while (gameObject)
        {
            elapsedTime += Time.deltaTime;
            t = elapsedTime / _collectingDuration;
            transform.position = Vector2.Lerp(transform.position, player.GetPosition(), t);
            yield return null;
        }
    }
}