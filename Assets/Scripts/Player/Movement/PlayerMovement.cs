using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;

    [SerializeField] public float _gravityModifier = 1.5f;

    [SerializeField] private float _resistance;

    private Vector2 _velocity;
    private Vector2 _additionalVelocity;

    private void Update()
    {
        _velocity.y += _gravityModifier * 9.8f * Time.deltaTime;
        _additionalVelocity = Vector2.MoveTowards(_additionalVelocity, Vector2.zero, _resistance * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
            _velocity.y = _jumpForse;

        Vector2 resultVelocity = _velocity + _additionalVelocity;
        transform.Translate(resultVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
            _velocity.x = _speed;
        else if (Input.GetKey(KeyCode.A))
            _velocity.x = -1 * _speed;
        else
            _velocity.x = 0;
    }

    public void AddForse(Vector2 forse)
    {
        _additionalVelocity = forse;
    }
}
