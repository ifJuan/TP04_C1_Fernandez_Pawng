using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BallBehaviour : MonoBehaviour, BallSubscriber
{
    [Header("Data")]
    [SerializeField] private BallDefaultValuesSo defaultValues;

    private float initialForce;
    private float bounceSpeedReaction = 1.01f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialForce = defaultValues.initialForce;
        bounceSpeedReaction = defaultValues.bounceSpeedReaction;
        spriteRenderer.color = GameStateManager.Instance.GetBallColor();

        transform.position = new Vector3(0f, 0f, 0f); // Manager should do it
        rb.AddForce(Vector2.up * initialForce);
        rb.AddForce(Vector2.left * initialForce);
    }

    void Awake()
    {
        GameStateManager.Instance.Subscribe(this);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.Unsubscribe(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 bounceDirection = collision.GetContact(0).normal;
        rb.AddForce(bounceDirection * bounceSpeedReaction, ForceMode2D.Impulse);
    }

    public void ColorChanged(Color color)
    {
        spriteRenderer.color = color;
    }
}
