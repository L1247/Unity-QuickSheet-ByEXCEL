using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Empty_WorkSheetClass_Name))]
public class Editor : BaseExcelEditor<Empty_WorkSheetClass_Name>
{	
    public override void OnEnable()
    {
        base.OnEnable();
        
        Empty_WorkSheetClass_Name data = target as Empty_WorkSheetClass_Name;
        
        databaseFields = ExposeProperties.GetProperties(data);
        
        foreach(Data e in data.dataArray)
        {
            dataFields = ExposeProperties.GetProperties(e);
            pInfoList.Add(dataFields);
        }
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //DrawDefaultInspector();
        if (GUI.changed)
        {
            pInfoList.Clear();
            
            Empty_WorkSheetClass_Name data = target as Empty_WorkSheetClass_Name;
            foreach(Data e in data.dataArray)
            {
                dataFields = ExposeProperties.GetProperties(e);
                pInfoList.Add(dataFields);
            }
            
            EditorUtility.SetDirty(target);
            Repaint();
        }
    }
    
    public override bool Load()
    {
        Empty_WorkSheetClass_Name targetData = target as Empty_WorkSheetClass_Name;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<Data>().ToArray();
			
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
