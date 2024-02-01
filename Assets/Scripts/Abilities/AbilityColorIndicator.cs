using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityColorIndicator : MonoBehaviour
{
    public ColorIndicator indicator_red;
    public ColorIndicator indicator_blue;
    public ColorIndicator indicator_green;
    public ColorIndicator indicator_grey;

    private ColorIndicator[] indicators;

    private AbilityColor currentColor;

    // Update is called once per frame
    void Start()
    {
        // stick all indicators in an array and initialize them
        indicators = new ColorIndicator[] { indicator_red, indicator_blue, indicator_green, indicator_grey };
        indicator_red.SetColor(AbilityColor.Red, false);
        indicator_blue.SetColor(AbilityColor.Blue, false);
        indicator_green.SetColor(AbilityColor.Green, false);
        indicator_grey.SetColor(AbilityColor.Grey, false);
    }

    public AbilityColor GetCurrentColor()
    {
        return currentColor;
    }

    public void SelectColor(AbilityColor abilityColor)
    {
        Debug.Log("------------------------------------------------------------------------------");
        Debug.Log("Selecting color " + abilityColor);
        foreach (ColorIndicator indicator in indicators)
        {
            if (indicator.GetColor() == abilityColor)
            {
                Debug.Log("Found indicator with color " + abilityColor);
                currentColor = indicator.Enable();
            }
            else
            {
                indicator.Disable();
            }
        }
    }
}
