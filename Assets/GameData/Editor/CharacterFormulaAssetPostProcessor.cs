using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class CharacterFormulaAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/GameData/Excel/Character.xlsx";
    private static readonly string assetFilePath = "Assets/GameData/Excel/CharacterFormula.asset";
    private static readonly string sheetName = "CharacterFormula";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            CharacterFormula data = (CharacterFormula)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(CharacterFormula));
            if (data == null) {
                data = ScriptableObject.CreateInstance<CharacterFormula> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<CharacterFormulaData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<CharacterFormulaData>().ToArray();
                data.dataList = query.Deserialize<CharacterFormulaData>();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
