using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Damager : MonoBehaviour
{
    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
