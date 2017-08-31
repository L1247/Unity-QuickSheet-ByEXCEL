using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class CharacterCCAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/GameData/Excel/Character.xlsx";
    private static readonly string assetFilePath = "Assets/GameData/Excel/CharacterCC.asset";
    private static readonly string sheetName = "CharacterCC";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            CharacterCC data = (CharacterCC)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(CharacterCC));
            if (data == null) {
                data = ScriptableObject.CreateInstance<CharacterCC> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<CharacterCCData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<CharacterCCData>().ToArray();
                data.dataList = query.Deserialize<CharacterCCData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
