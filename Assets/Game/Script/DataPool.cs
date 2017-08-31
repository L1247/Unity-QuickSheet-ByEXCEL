using UnityEngine;
public class DataPool  {
    public static CharacterCC m_CharacterDT;

    /// <summary>
    /// 初始化Pool
    /// </summary>
    public static void f_InitPool ()
    {
        m_CharacterDT = Resources.Load<CharacterCC>( "ExcelData/CharacterCC" );
        Debug.Log( m_CharacterDT.dataList.Count );
    }
    
}
