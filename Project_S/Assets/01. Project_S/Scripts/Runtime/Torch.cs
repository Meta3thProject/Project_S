using UnityEngine;

public class Torch : MonoBehaviour
{
    // Start is called before the first frame 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("Trigger 닿았음");
            other.gameObject.transform.localScale *= 2f;
        }
    }
}
