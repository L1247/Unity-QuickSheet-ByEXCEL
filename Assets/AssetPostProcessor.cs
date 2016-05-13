using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

///
/// !!! Machine generated code !!!
///
public class AssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "D:/Unity Project/Blog/Excel2GameDemo/Assets";
    private static readonly string assetFilePath = "D:/Unity Project/Blog/Excel2GameDemo/.asset";
    private static readonly string sheetName = "Example";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            Example data = (Example)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(Example));
            if (data == null) {
                data = ScriptableObject.CreateInstance<Example> ();
                data.sheetName = filePath;
                data.worksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<Data>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<Data>().ToArray();

                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
