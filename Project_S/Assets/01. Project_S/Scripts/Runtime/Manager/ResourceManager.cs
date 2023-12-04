using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    #region Dictionarys
    public static Dictionary<string, GameObject> objects;
    public static Dictionary<string, GameObject> effects;
    public static Dictionary<string, GameObject> items;
    #endregion

    public static void Init()
    {
        objects = new Dictionary<string, GameObject>();
        effects = new Dictionary<string, GameObject>();
        items = new Dictionary<string, GameObject>();

        AddResouces();
    }       // Init()

    // ! 리소스 배열에 추가
    public static void AddResouces()
    {
        GameObject[] objResources = Resources.LoadAll<GameObject>(RDefine.PATH_OBJECTS);
        GameObject[] effectResource = Resources.LoadAll<GameObject>(RDefine.PATH_EFFECTS);
        GameObject[] itemResources = Resources.LoadAll<GameObject>(RDefine.PATH_ITEMS);

        AddDictionary(objResources, objects);
        AddDictionary(effectResource, effects);
        AddDictionary(itemResources, items);

    }       // AddResource()

    // ! 리소스 배열을 딕셔너리에 캐싱
    private static void AddDictionary(GameObject[] resources_, Dictionary<string, GameObject> dictionary_)
    {
        foreach(GameObject resource in resources_)
        {
            dictionary_.Add(resource.name, resource);
        }
    }       // AddDictionary()
}
