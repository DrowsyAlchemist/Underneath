using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var gameObj in FindObjectsOfType<GameObject>())
            if (gameObj.TryGetComponent(out ISaveable saveable))
                saveable.Save();
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
