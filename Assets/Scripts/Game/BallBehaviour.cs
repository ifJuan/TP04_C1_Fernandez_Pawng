using Unity.VisualScripting;
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
        LaunchBall();
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
        Goal goal = collision.gameObject.GetComponent<Goal>();
        if (goal)
        {
            GameStateManager.Instance.GoalFor(side: goal.GetSide());
            return;
        }

        Vector2 bounceDirection = collision.GetContact(0).normal;
        rb.AddForce(bounceDirection * bounceSpeedReaction, ForceMode2D.Impulse);
    }

    private void LaunchBall()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rb.AddForce(direction * initialForce, ForceMode2D.Impulse);
    }

    public void ColorChanged(Color color)
    {
        spriteRenderer.color = color;
    }

    public void RemoveFromField()
    {
        Destroy(gameObject);
    }
}
