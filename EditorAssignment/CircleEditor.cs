using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Circle))]
public class CircleEditor : EditorWindow
{
    public GameObject gameObject;
    private LineRenderer lineRenderer;

    private int segments;
    private const int zero;
    private float lineWidth;
    private float radius;

    [MenuItem("Window/CircleEditor")]
    public static void ShowWindow()
    {
        GetWindow<CircleEditor>("Circle Editor");
    }

    public void OnGUI()
    {
        GUILayout.Label("Circle settings", EditorStyles.boldLabel);
        gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);
        
        if (!gameObject.GetComponent<LineRenderer>())
        {
            gameObject.AddComponent<LineRenderer>();
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.loop = true;
        }
        else
        {
            if (!gameObject.GetComponent<LineRenderer>().loop)
            {
                lineRenderer.loop = true;
            }
        }
        
        EditorGUI.BeginChangeCheck();

        segments = EditorGUILayout.IntSlider("Sides", segments, 10, 50);
        radius = EditorGUILayout.Slider("Radius", radius, 1f, 10f);

        if (EditorGUI.EndChangeCheck())
        {
            CreateCircle();
        }
    }

    private void CreateCircle()
    {
        float deltaTheta = (2f * Mathf.PI) / segments;
        float theta = zero;

        lineRenderer.positionCount = segments;
        lineRenderer.widthMultiplier = lineWidth;

        Vector3 startPosition = gameObject.transform.position;

        for (int index = zero; index < segments; index++)
        {
            Vector3 nextPosition = new Vector3(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta), 0f);
            lineRenderer.SetPosition(index, startPosition + nextPosition);

            theta += deltaTheta;
        }
    }
}
