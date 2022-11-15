using System.Collections;
using UnityEngine;

public class CyanWraithAnimator : EnemyAnimator
{
    [SerializeField] private float _sizeBeforeExplosion = 1.4f;

    public override void PlayAttack()
    {
        base.PlayAttack();
        StartCoroutine(Inflate());
    }

    private IEnumerator Inflate()
    {
        yield return new WaitForEndOfFrame();
        int direction = transform.localScale.x > 0 ? 1 : -1;
        Vector3 startSize = transform.localScale;
        Vector3 endSize = new Vector3(direction * _sizeBeforeExplosion, _sizeBeforeExplosion, transform.localScale.z);
        float duration = base.Animator.GetCurrentAnimatorStateInfo(0).length;
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startSize, endSize, counter / duration);
            yield return null;
        }
    }
}