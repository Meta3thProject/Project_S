using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ResourceManager
{
    #region Dictionarys
    public static Dictionary<string, GameObject> objects;
    public static Dictionary<string, GameObject> effects;
    public static Dictionary<string, GameObject> items;
    public static Dictionary<string, Sprite> sprites;
    

    #endregion

    public static void Init()
    {
        objects = new Dictionary<string, GameObject>();
        effects = new Dictionary<string, GameObject>();
        items = new Dictionary<string, GameObject>();
        sprites = new Dictionary<string, Sprite>();

        AddResouces();
    }       // Init()

    // ! 리소스 배열에 추가
    public static void AddResouces()
    {
        GameObject[] objResources = Resources.LoadAll<GameObject>(RDefine.PATH_OBJECTS);
        GameObject[] effectResource = Resources.LoadAll<GameObject>(RDefine.PATH_EFFECTS);
        GameObject[] itemResources = Resources.LoadAll<GameObject>(RDefine.PATH_ITEMS);
        Sprite [] spriteResources = Resources.LoadAll<Sprite>(RDefine.PATH_SPRITES);

        AddDictionary(objResources, objects);
        AddDictionary(effectResource, effects);
        AddDictionary(itemResources, items);
        AddDictionary(spriteResources, sprites);

 
    }       // AddResource()

    // ! 리소스 배열을 딕셔너리에 캐싱
    private static void AddDictionary<T>(T[] resources_, Dictionary<string, T> dictionary_)
    {
        
        foreach(T resource in resources_)
        {
            Object temp = resource as Object;
            dictionary_.Add(temp.name, resource);
        }
    }       // AddDictionary()
}
