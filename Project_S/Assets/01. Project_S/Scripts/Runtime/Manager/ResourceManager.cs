using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : GSingleton<ResourceManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// Resources 폴더에서 프리팹을 가져와서 인스턴스로 생성하는 함수
    /// </summary>
    /// <param name="_path">경로 혹은 오브젝트 이름</param>
    /// <param name="_pos">생성될 오브젝트의 위치</param>
    /// <param name="_rot">생성될 오브젝트의 회전</param>
    /// <returns>프리팹 오브젝트</returns>
    public GameObject GetResource(string _path, Vector3 _pos, Quaternion _rot)
    {
        GameObject tempObject = default;

        tempObject = Resources.Load<GameObject>(_path);
        
        tempObject.transform.position = _pos;
        tempObject.transform.rotation = _rot;

        return Instantiate(tempObject);        
    }       // GetResource()
}
