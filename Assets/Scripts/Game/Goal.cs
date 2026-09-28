using UnityEngine;
using static Appearance;

public class Goal : MonoBehaviour
{
    public enum Side
    {
        Left,
        Right
    }

    [SerializeField] private Side side;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        InitialPosition();
    }

    private void InitialPosition()
    {
        Camera cam = Camera.main;
        float viewportX = side == Side.Left ? 0f : 1f;
        Vector3 pos = cam.ViewportToWorldPoint(new Vector3(viewportX, 0.5f, 10f));
        float halfWidth = spriteRenderer.bounds.extents.x;
        pos.x += side == Side.Left ? -halfWidth : halfWidth;
        spriteRenderer.transform.position = pos;
    }

    public Side GetSide()
    {
        return side;
    }
}
