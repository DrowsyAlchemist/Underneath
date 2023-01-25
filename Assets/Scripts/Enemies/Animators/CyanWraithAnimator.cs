using System.Collections;
using UnityEngine;

public class CyanWraithAnimator : WraithAnimator
{
    [SerializeField] private float _sizeAfterInflation = 1.3f;

    public override void PlayAttack()
    {
        base.PlayAttack();
        StartCoroutine(Inflate());
    }

    private IEnumerator Inflate()
    {
        int direction = (transform.localScale.x > 0) ? 1 : -1;
        Vector3 startSize = transform.localScale;
        Vector3 endSize = new Vector3(direction * _sizeAfterInflation, _sizeAfterInflation, transform.localScale.z);

        yield return new WaitForEndOfFrame();
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