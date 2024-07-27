using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public float height = 1.0f;
    public Color meshColor = Color.red;
    public float xOffset = 0.0f; // Offset di axis X
    public float yOffset = 1.0f; // Offset di axis Y (sama dengan verticalOffset sebelumnya)
    public float zOffset = 0.0f; // Offset di axis Z (horizontalOffset sebelumnya)
    public bool showSensorVisual = true; // Enable/disable sensor visual
    public Transform player;

    private Mesh mesh;
    private EnemyAI enemyAI;

    void Start()
    {
        mesh = CreateWedgeMesh();
        enemyAI = GetComponent<EnemyAI>();
    }

    void Update()
    {
        Scan();
    }

    private void Scan()
    {
        if (player == null || enemyAI == null)
            return;

        Vector3 sensorPosition = transform.position + new Vector3(xOffset, yOffset, zOffset);
        Vector3 directionToPlayer = (player.position - sensorPosition).normalized;
        float distanceToPlayer = Vector3.Distance(sensorPosition, player.position);
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer <= distance && angleToPlayer <= angle)
        {
            RaycastHit hit;
            if (Physics.Raycast(sensorPosition, directionToPlayer, out hit, distance))
            {
                if (hit.transform == player)
                {
                    enemyAI.SetPlayerDetected(true);
                    return;
                }
            }
        }

        enemyAI.SetPlayerDetected(false);
    }

    private Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero + new Vector3(xOffset, yOffset, zOffset);
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance + new Vector3(xOffset, yOffset, zOffset);
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance + new Vector3(xOffset, yOffset, zOffset);

        Vector3 topCenter = bottomCenter + Vector3.up * height;
        Vector3 topRight = bottomRight + Vector3.up * height;
        Vector3 topLeft = bottomLeft + Vector3.up * height;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;
        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance + new Vector3(xOffset, yOffset, zOffset);
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance + new Vector3(xOffset, yOffset, zOffset);

            topRight = bottomRight + Vector3.up * height;
            topLeft = bottomLeft + Vector3.up * height;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnDrawGizmos()
    {
        if (!showSensorVisual) return;

        if (mesh == null)
        {
            mesh = CreateWedgeMesh();
        }

        Gizmos.color = meshColor;
        Gizmos.DrawMesh(mesh, transform.position + new Vector3(xOffset, yOffset, zOffset), transform.rotation);

        Gizmos.DrawWireSphere(transform.position + new Vector3(xOffset, yOffset, zOffset), distance);
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
    }
}
