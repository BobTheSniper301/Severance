using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.smallRadius);
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.FarRadius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Vector3 viewAngle03 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.closeAngle / 2);
        Vector3 viewAngle04 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.closeAngle / 2);

        Vector3 viewAngle05 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.FarAngle / 2);
        Vector3 viewAngle06 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.FarAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle03 * fov.smallRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle04 * fov.smallRadius);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle05 * fov.FarRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle06 * fov.FarRadius);
    }

    private Vector3 DirectionFromAngle(float eularY, float angleInDegrees)
    {
        angleInDegrees += eularY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
