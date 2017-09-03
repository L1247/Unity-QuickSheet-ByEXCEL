using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zirpl.CalcEngine;
public class UnitTest_Formula : MonoBehaviour
{
    CalculationEngine calculator = new CalculationEngine();
    public CharacterFormula fighterFormula;

    // A class instance for persistence data
    private PlayerStat playerStatus = new PlayerStat();
    // Use this for initialization
    void Start ( )
    {
        SetPlayer();
        //CalculationEngine calculator = new CalculationEngine();
        //calculator.Variables[ "a" ] = 1;

        //// The result ouput 2 as you expect
        //var result = calculator.Evaluate( "a + 1" );
        //print( result );
    }

    private void SetPlayer ( )
    {
        // Specify skill level
        playerStatus.SkillLevel = 4;

        CalculationEngine calculator = new CalculationEngine();

        // CalcEngine uses Reflection to access the properties of the PlayderData object
        // so they can be used in expressions.
        calculator.DataContext = playerStatus;

        //// Calculate each of stat for a player
        playerStatus.STR = Convert.ToSingle( calculator.Evaluate( GetFormula( "STR" ) ) );
        Debug.LogFormat( "STR: {0}" , playerStatus.STR );

        playerStatus.DEX = Convert.ToSingle( calculator.Evaluate( GetFormula( "DEX" ) ) );
        Debug.LogFormat( "DEX: {0}" , playerStatus.DEX );

        playerStatus.ITL = Convert.ToSingle( calculator.Evaluate( GetFormula( "ITL" ) ) );
        Debug.LogFormat( "ITL: {0}" , playerStatus.ITL );

        playerStatus.HP = Convert.ToSingle( calculator.Evaluate( GetFormula( "HP" ) ) );
        Debug.LogFormat( "HP: {0}" , playerStatus.HP );

        playerStatus.MP = Convert.ToSingle( calculator.Evaluate( GetFormula( "MP" ) ) );
        Debug.LogFormat( "MP: {0}" , playerStatus.MP );
    }
    // A helper function to retrieve formula data with the given formula name.
    string GetFormula ( string formulaName )
    {
        string formulaString = fighterFormula.dataArray.Where( e => e.Stat == formulaName )
                                        .FirstOrDefault().Formula;
        print( formulaString );
        return formulaString;
    }
}
