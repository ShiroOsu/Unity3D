using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class CircleEditor : EditorWindow
{
    public GameObject gameObject;
    private LineRenderer lineRenderer;

    private int segments;
    private const int zero = 0;
    private float lineWidth = 0.1f;
    private float radius;

    [MenuItem("Window/CircleEditor")]
    public static void ShowWindow()
    {
        GetWindow<CircleEditor>("Circle Editor");
    }

    public void OnGUI()
    {
        GUILayout.Label("Circle settings", EditorStyles.boldLabel);
        
        Assert.IsNotNull(gameObject, "You need to assign a gameobject in the Circle Editor.");
        gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);

        // If gameobject does not have a LineRenderer component, add it.
        if (!gameObject.GetComponent<LineRenderer>())
        {
            gameObject.AddComponent<LineRenderer>();
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.loop = true;
        }
        else
        {
            // If it already has a LineRenderer component, set loop to true.
            if (!gameObject.GetComponent<LineRenderer>().loop)
            {
                lineRenderer.loop = true;
            }
        }

        // If we close Circle Editor and open a new one it does not
        // get the LineRenderer component already on the gameobject.
        if (!lineRenderer)
        {
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.loop = true;
        }

        // If we are changing the values for radius and segments.
        EditorGUI.BeginChangeCheck();

        segments = EditorGUILayout.IntSlider("Segments", segments, 10, 50);
        radius = EditorGUILayout.Slider("Radius", radius, 1f, 10f);

        if (EditorGUI.EndChangeCheck())
        {
            CreateCircle();
        }
    }

    // Create Circle.
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
