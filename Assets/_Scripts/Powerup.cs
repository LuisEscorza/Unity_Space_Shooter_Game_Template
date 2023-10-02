using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed;
    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private string _activateMessage;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.velocity = _speed * transform.up;
    }

    private void OnEnable()
    {  
        _rigidBody.velocity = _speed * transform.up;
    }

    protected abstract void ActivatePowerup(PlayerBase player);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.TryGetComponent(out PlayerBase player);
            ActivatePowerup(player);
            GameplayManager.Manager.GetComponent<GameplayHudManager>().PowerupPickedUp(_activateMessage);
            AudioManager.Manager.PlaySFX(_activateSound);
            ObjectPoolManager.Manager.ReturnObjectToPool(gameObject);
        }
    }

}
