using UnityEngine;
using UnityEngine.UI;

public class PlayerSight : MonoBehaviour
{
    public Transform playerTransform;
    public Image minimapIcon;
    public float fieldOfView = 0f;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh coneMesh;

    void Start()
    {
        // MeshFilter 및 MeshRenderer 추가
        meshFilter = minimapIcon.gameObject.GetComponent<MeshFilter>();
        meshRenderer = minimapIcon.gameObject.GetComponent<MeshRenderer>();

        // 메시 생성
        coneMesh = CreateConeMesh(fieldOfView);

        // MeshFilter에 메시 할당
        meshFilter.mesh = coneMesh;

        // 부채꼴 색상 설정
        meshRenderer.material.color = Color.white;
    }

    void Update()
    {
        UpdateMinimapIcon();
    }

    void UpdateMinimapIcon()
    {
        // 플레이어 아이콘의 위치를 설정
        minimapIcon.transform.position = CalculateMinimapPosition(new Vector3(playerTransform.position.x, 17.9f, playerTransform.position.z));

        // 플레이어 아이콘의 회전을 설정 (playerTransform의 y값에 20을 더한 각도로)
        minimapIcon.transform.rotation = Quaternion.Euler(0, playerTransform.eulerAngles.y - 90, 0);
    }

    Vector3 CalculateMinimapPosition(Vector3 worldPosition)
    {
        // 월드 좌표를 미니맵 좌표로 변환하는 코드
        // 예시에서는 간단히 worldPosition을 사용하고 있습니다.
        return worldPosition;
    }

    Mesh CreateConeMesh(float angle)
    {
        int segments = 30;
        float radius = 5f;

        Mesh mesh = new Mesh();

        // 정점 배열 생성
        Vector3[] vertices = new Vector3[segments + 2];
        vertices[0] = Vector3.zero; // 정점 0은 원점

        for (int i = 1; i <= segments + 1; i++)
        {
            float radian = Mathf.Deg2Rad * (i * angle / segments - angle / 2f);
            vertices[i] = new Vector3(Mathf.Cos(radian) * radius, 0f, Mathf.Sin(radian) * radius);
        }

        // 삼각형 배열 생성
        int[] triangles = new int[segments * 3];
        for (int i = 0, vi = 1; i < segments * 3; i += 3, vi++)
        {
            triangles[i] = 0;
            triangles[i + 1] = vi;
            triangles[i + 2] = vi + 1;
        }

        // 메시에 정점 및 삼각형 할당
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        return mesh;
    }
}