using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum to represent the different ability colors
public enum AbilityColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Grey
    
}

// static class to get the color associated with the given ability color
public static class AbilityColors
{
    // get the color associated with the given ability color
    public static Color GetColor(AbilityColor abilityColor)
    {
        switch (abilityColor)
        {
            case AbilityColor.Red:
                return Color.red;
            case AbilityColor.Blue:
                return Color.blue;
            case AbilityColor.Green:
                return Color.green;
            case AbilityColor.Yellow:
                return Color.yellow;
            case AbilityColor.Grey:
                return Color.grey;;
            default:
                return Color.white;
        }
    }
}
