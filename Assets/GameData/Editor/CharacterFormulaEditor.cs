using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityQuickSheet;
using System.Linq;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(CharacterFormula))]
public class CharacterFormulaEditor : BaseExcelEditor<CharacterFormula>
{	    
    public override bool Load()
    {
        CharacterFormula targetData = target as CharacterFormula;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<CharacterFormulaData>().ToArray();
            targetData.dataList = query.Deserialize<CharacterFormulaData>();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
