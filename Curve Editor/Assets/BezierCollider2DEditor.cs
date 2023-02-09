using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCollider2D))]
public class BezierCollider2DEditor : Editor
{
    BezierCollider2D bezierCollider; // Create a bezier curve object
    EdgeCollider2D edgeCollider; // Create an edge collider object

    int lastPointsQuantity = 0;
    Vector2 lastFirstPoint = Vector2.zero;
    Vector2 lastHandlerFirstPoint = Vector2.zero;
    Vector2 lastSecondPoint = Vector2.zero;
    Vector2 lastHandlerSecondPoint = Vector2.zero;

    public override void OnInspectorGUI()
    {
        bezierCollider = (BezierCollider2D)target;

        edgeCollider = bezierCollider.GetComponent<EdgeCollider2D>();

        if (edgeCollider != null) // If there is an edge collider object:
        {
            bezierCollider.z = EditorGUILayout.IntField("curve points", bezierCollider.z, GUILayout.MinWidth(100));
            bezierCollider.q = EditorGUILayout.Vector2Field("first point", bezierCollider.q, GUILayout.MinWidth(100));
            bezierCollider.u = EditorGUILayout.Vector2Field("handler first Point", bezierCollider.u, GUILayout.MinWidth(100));
            bezierCollider.y = EditorGUILayout.Vector2Field("second point", bezierCollider.y, GUILayout.MinWidth(100));
            bezierCollider.v = EditorGUILayout.Vector2Field("handler secondPoint", bezierCollider.v, GUILayout.MinWidth(100));

            EditorUtility.SetDirty(bezierCollider);

            if (bezierCollider.z > 0  &&
                (
                    lastPointsQuantity != bezierCollider.z ||
                    lastFirstPoint != bezierCollider.q ||
                    lastHandlerFirstPoint != bezierCollider.u ||
                    lastSecondPoint != bezierCollider.y ||
                    lastHandlerSecondPoint != bezierCollider.v
                )) // If there are more than 0 points, anything about the curve changes (Positions or amount of points):
            {
                lastPointsQuantity = bezierCollider.z;
                lastFirstPoint = bezierCollider.q;
                lastHandlerFirstPoint = bezierCollider.u;
                lastSecondPoint = bezierCollider.y;
                lastHandlerSecondPoint = bezierCollider.v; // Update all the point positions
                edgeCollider.points = bezierCollider.calculate2DPoints(); // Calculate all the points inbetween
            }

        }
    }

    void OnSceneGUI()
    {
        if (bezierCollider != null)
        {
            Handles.color = Color.grey;

            Handles.DrawLine(bezierCollider.transform.position + (Vector3)bezierCollider.u, bezierCollider.transform.position + (Vector3)bezierCollider.q);
            Handles.DrawLine(bezierCollider.transform.position + (Vector3)bezierCollider.v, bezierCollider.transform.position + (Vector3)bezierCollider.y);

            bezierCollider.q = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.q), Quaternion.identity, 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.q)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;
            bezierCollider.y = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.y), Quaternion.identity, 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.y)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;
            bezierCollider.u = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.u), Quaternion.identity, 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.u)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;
            bezierCollider.v = Handles.FreeMoveHandle(bezierCollider.transform.position + ((Vector3)bezierCollider.v), Quaternion.identity, 0.04f * HandleUtility.GetHandleSize(bezierCollider.transform.position + ((Vector3)bezierCollider.v)), Vector3.zero, Handles.DotHandleCap) - bezierCollider.transform.position;

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }

}