using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.DebugUI;

public class Movement : MonoBehaviour, PlayerSubscriber
{
    [Header("Data")]
    [SerializeField] private PlayerDefaultValuesSo defaultValues;

    public float movementSpeed;
    private float speedModifier;
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameStateManager.Instance.Subscribe(this);
        speedModifier = GameStateManager.Instance.GetSpeedModifier();
        movementSpeed = defaultValues.movementSpeed;
        upKey = defaultValues.upKey;
        downKey = defaultValues.downKey;
        leftKey = defaultValues.leftKey;
        rightKey = defaultValues.rightKey;
    }

    void Awake()
    {
        GameStateManager.Instance.Subscribe(this);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.Unsubscribe(this);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float fixedSpeed = movementSpeed * speedModifier * Time.fixedDeltaTime;

        if (Input.GetKey(upKey))
            rb.linearVelocity = Vector2.up * fixedSpeed;
        if (Input.GetKey(downKey))
            rb.linearVelocity = Vector2.down * fixedSpeed;
    }

    public void HeightChanged(float percentage)
    {
        // Not needed.
    }

    public void ColorChanged(UnityEngine.Color color)
    {
        // Not needed.
    }

    public void SpeedModifierChanged(float speedModifier)
    {
        this.speedModifier = speedModifier;
    }
}
