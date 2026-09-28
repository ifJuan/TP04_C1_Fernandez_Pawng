using UnityEngine;

public class Appearance : MonoBehaviour, PlayerSubscriber
{
    public enum Side
    {
        Left,
        Right
    }

    [Header("Data")]
    [SerializeField] private PlayerDefaultValuesSo defaultValues;

    private Side playerSide;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultValues.initialColor;
        playerSide = defaultValues.side;
        MoveToInitialPosition();
        SetHeightPercentage(GameStateManager.Instance.GetHeightPercentage());
    }

    void Awake()
    {
        GameStateManager.Instance.Subscribe(this);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.Unsubscribe(this);
    }

    public void MoveToInitialPosition()
    {
        Camera cam = Camera.main;
        float viewportX = playerSide == Side.Left ? 0.1f : 0.9f;
        Vector3 pos = cam.ViewportToWorldPoint(new Vector3(viewportX, 0.5f, 10f));
        float halfWidth = spriteRenderer.bounds.extents.x;
        pos.x += playerSide == Side.Left ? halfWidth : -halfWidth;
        spriteRenderer.transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallBehaviour>())
        {
            spriteRenderer.color = new Color(
                UnityEngine.Random.Range(0f, 1f), 
                UnityEngine.Random.Range(0f, 1f), 
                UnityEngine.Random.Range(0f, 1f), 
                1f
            );
        }
        if (collision.gameObject.GetComponent<Limit>()) {
            spriteRenderer.color = Color.grey;
        }
    }

    private void SetHeightPercentage(float heightPercentage)
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        var height = (heightPercentage / 100f) * screenHeight;
        gameObject.transform.parent.localScale = new Vector3(gameObject.transform.localScale.x, height, 1f);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    public void HeightChanged(float percentage)
    {
        SetHeightPercentage(percentage);
    }
}
