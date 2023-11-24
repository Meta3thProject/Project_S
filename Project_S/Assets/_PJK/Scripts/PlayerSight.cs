using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    public MeshRenderer visionConeRenderer; // 시야를 나타낼 메쉬 렌더러
    public float visionAngle = 60f; // 시야 각도
    public float visionDistance = 10f; // 시야 거리

    void Update()
    {
        DrawVisionCone();
    }

    void DrawVisionCone()
    {
        MeshFilter meshFilter = visionConeRenderer.GetComponent<MeshFilter>();
        Mesh visionMesh = new Mesh();

        int segments = 50; // 메쉬의 세그먼트 수, 조정 가능

        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;
        float angleIncrement = visionAngle / segments;
        for (int i = 1; i <= segments + 1; i++)
        {
            float angle = angleIncrement * (i - 1);
            vertices[i] = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0f, Mathf.Sin(Mathf.Deg2Rad * angle)) * visionDistance;
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        visionMesh.vertices = vertices;
        visionMesh.triangles = triangles;

        meshFilter.mesh = visionMesh;
    }
}
