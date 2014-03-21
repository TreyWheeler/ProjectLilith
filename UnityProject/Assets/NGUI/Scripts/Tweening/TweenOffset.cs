using UnityEngine;

/// <summary>
/// Tween the anchors's relative offset.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Offset")]
public class TweenOffset : UITweener
{
    public Vector2 from;
    public Vector2 to;

    Transform mTrans;
    UIAnchor mAnchor;

    /// <summary>
    /// Current offset.
    /// </summary>

    public Vector2 offset
    {
        get
        {
            if (mAnchor != null) return mAnchor.relativeOffset;
            return Vector2.zero;
        }
        set
        {
            if (mAnchor != null) mAnchor.relativeOffset = value;
        }
    }

    /// <summary>
    /// Find all needed components.
    /// </summary>

    void Awake()
    {
        mAnchor = GetComponent<UIAnchor>();
    }

    /// <summary>
    /// Interpolate and update the offset.
    /// </summary>

    protected override void OnUpdate(float factor, bool isFinished) { offset = from * (1f - factor) + to * factor; }

    /// <summary>
    /// Start the tweening operation.
    /// </summary>

    public static TweenOffset Begin(GameObject go, float duration, Vector2 offset)
    {
        TweenOffset comp = UITweener.Begin<TweenOffset>(go, duration);
        comp.from = comp.offset;
        comp.to = offset;

        if (duration <= 0f)
        {
            comp.Sample(1f, true);
            comp.enabled = false;
        }
        return comp;
    }
}