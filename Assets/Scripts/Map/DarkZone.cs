using UnityEngine;

public class DarkZone : MonoBehaviour
{

    LineRenderer line;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;

    }

    private void Update()
    {
        line.widthCurve = new AnimationCurve(new Keyframe(0, transform.localScale.y), new Keyframe(1, transform.localScale.y));
    }
}
