using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SavePoint : MonoBehaviour
{
    private const string SavesFolderName = "Player";
    private const string PlayerPositionFileName = "Position";

    private void OnEnable()
    {
        var playerPosition = SaveLoadManager.GetLoadOrDefault<SaveableVector>(SavesFolderName, PlayerPositionFileName);

        if (playerPosition != default)
            AccessPoint.Player.transform.position = new Vector3(playerPosition.X, playerPosition.Y, playerPosition.Z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            foreach (var gameObj in FindObjectsOfType<GameObject>(includeInactive: true))
                if (gameObj.TryGetComponent(out ISaveable saveable))
                    saveable.Save();

            var playerPosition = new SaveableVector(transform.position);
            SaveLoadManager.Save(SavesFolderName, PlayerPositionFileName, playerPosition);
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    [System.Serializable]
    private class SaveableVector
    {
        public float X;
        public float Y;
        public float Z;

        public SaveableVector(Vector3 vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }
    }
}