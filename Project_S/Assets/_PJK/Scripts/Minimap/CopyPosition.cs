using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    

    [SerializeField]
    private Transform target;
    // Update is called once per frame
    void Update()
    {
        if (!target) return;

        transform.position = new Vector3(target.position.x, target.position.y, target.position.z);

    }
}
