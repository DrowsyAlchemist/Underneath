using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/Enemy", order = 51)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int _defaultDamage = 1;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private int _award;

    public int DefaultDamage => _defaultDamage;
    public ParticleSystem HitEffect => _hitEffect;
    public int Award => _award;
}
