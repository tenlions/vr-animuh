using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDrawer : MonoBehaviour
{
    public bool drawVelocity = true;
    public bool invertVelocity = false;
    public bool drawLocalCoords = true;
    public bool drawHandForward = false;

    [Range(0.1f, 5.0f)]
    public float arrowScale = 1.0f;

    private Color color_x = Color.red;
    private Color color_y = Color.green;
    private Color color_z = Color.blue;
    private Color color_handForward = Color.cyan;
    private Color color_velocity = Color.yellow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrawGizmos()
    {
        if (drawLocalCoords) DrawLocalCoords();
        if (drawVelocity) DrawVelocity(GetRigidbody());

        if (drawHandForward)
        {
            DrawArrow(transform, Quaternion.AngleAxis(45, transform.right) * transform.forward, color_handForward);
        }
    }

    // Draws the local coordinates of the GameObject
    private void DrawLocalCoords()
    {
        // draw x axis with arrow
        DrawArrow(transform, transform.right, color_x);

        // draw y axis with arrow
        DrawArrow(transform, transform.up, color_y);

        // draw z axis with arrow
        DrawArrow(transform, transform.forward, color_z);
    }

    // Draws an arrow from the origin in the direction, with the specified color
    private void DrawArrow(Transform origin, Vector3 direction, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(origin.position, (origin.position + (arrowScale * direction)));
        Cone.DrawCone(origin.position + (arrowScale * direction), direction, 0.1f);
    }

    // Draws the velocity of the Rigidbody, if it exists
    private void DrawVelocity(Rigidbody rb)
    {
        if (rb == null) return;

        Vector3 velocity = rb.velocity;
        if (invertVelocity) velocity = -velocity;

        Gizmos.color = color_velocity;
        Gizmos.DrawLine(rb.transform.position, (rb.transform.position + rb.velocity));
        Cone.DrawCone(rb.transform.position + rb.velocity, rb.velocity, 0.1f);
    }

    // Retrieves the Rigidbody component from the GameObject, or its parent, or its children. Returns null if none is found.
    private Rigidbody GetRigidbody()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) rb = GetComponentInParent<Rigidbody>();
        if (rb == null) rb = GetComponentInChildren<Rigidbody>();
        return rb;
    }

    private static class Cone
    {
        private static Vector3[] newVertices = new Vector3[]
        {
            new Vector3(-0.0f, 1.0f, 0.0f),
            new Vector3(0.2f, 0.0f, 0.4f),
            new Vector3(0.5f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(-0.3f, 0.0f, 0.4f),
            new Vector3(-0.5f, 0.0f, 0.0f),
            new Vector3(-0.2f, 0.0f, -0.4f),
            new Vector3(0.2f, 0.0f, -0.4f),
        };
        private static Vector3[] newNormals = new Vector3[]
        {
            new Vector3(0,1,0),
            new Vector3(1,0,1),
            new Vector3(1,0,0),
            new Vector3(0,-1,0),
            new Vector3(-1,0,1),
            new Vector3(-1,0,0),
            new Vector3(-1,0,-1),
            new Vector3(1,0,-1),
        };
        private static int[] newTriangles = new int[]
        {
                0, 1, 2, 2, 1, 3, 0, 4, 1, 1, 4, 3, 0, 5, 4, 4, 5, 3, 0, 6, 5, 5, 6, 3, 0, 7, 6, 6, 7, 3, 0, 2, 7, 7, 2, 3
        };

        private static Mesh mesh;

        static Cone()
        {
            mesh = new Mesh();
            mesh.vertices = newVertices;
            mesh.triangles = newTriangles;
            mesh.normals = newNormals;
        }

        public static void DrawCone(Vector3 position, Vector3 rotation, float scale)
        {
            Gizmos.DrawMesh(mesh, position, Quaternion.FromToRotation(Vector3.up, rotation), new Vector3(scale, 2 * scale, scale));
        }
    }
}
