using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Circle))]
public class CircleEditor : EditorWindow
{
    public Object source;
    private LineRenderer lineRenderer;

    private int sides;
    private float radius;

    [MenuItem("Window/CircleEditor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CircleEditor));
    }

    public void OnGUI()
    {
        GUILayout.Label("Circle settings", EditorStyles.boldLabel);
        lineRenderer = (LineRenderer)EditorGUILayout.ObjectField(source, typeof(LineRenderer), true);

        sides = EditorGUILayout.IntSlider("Sides", sides, 10, 50);
        radius = EditorGUILayout.Slider("Radius", radius, 1f, 10f);
    }

    private void Update()
    {
        lineRenderer.gameObject.GetComponent<Circle>().segments = sides;
        lineRenderer.gameObject.GetComponent<Circle>().radius = radius;
    }
}
