using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour
{
    [Range(10, 50)]public int segments = 10;
    [Range(1f, 10f)]public float radius = 3f;

    private float lineWidth = 0.1f;
    private const int zero = 0;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;

        CreateCircle();
    }

    private void CreateCircle()
    {
        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = zero;

        lineRenderer.positionCount = segments;
        lineRenderer.widthMultiplier = lineWidth;

        Vector3 startPosition = transform.localPosition;

        for (int index = zero; index < segments; index++)
        {
            Vector3 nextPosition = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(index, startPosition + nextPosition);

            theta += deltaTheta;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = zero;

        Vector3 oldSegmentPosition = Vector3.zero;

        for (int index = zero; index < segments; index++)
        {
            Vector3 segmentPosition = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            Gizmos.DrawLine(oldSegmentPosition, transform.position + segmentPosition);
            oldSegmentPosition = transform.position + segmentPosition;

            theta += deltaTheta;
        }
    }
#endif
}
