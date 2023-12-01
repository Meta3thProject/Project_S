using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class QUEST_TABLEAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/_JSH/Data/QUEST_DATA.xlsx";
    private static readonly string assetFilePath = "Assets/_JSH/Data/QUEST_TABLE.asset";
    private static readonly string sheetName = "QUEST_TABLE";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            QUEST_TABLE data = (QUEST_TABLE)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(QUEST_TABLE));
            if (data == null) {
                data = ScriptableObject.CreateInstance<QUEST_TABLE> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<QUEST_TABLEData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<QUEST_TABLEData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
