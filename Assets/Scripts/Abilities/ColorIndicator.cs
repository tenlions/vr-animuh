using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIndicator : MaterialChanger
{
    private Light light_self;

    private const float ALPHA_ENABLED = .4f;
    private const float ALPHA_DISABLED = .2f;

    private AbilityColor abilityColor_self;

    // sets the color of the indicator to the given ability color, enabling it if enabled is true and disabling it if enabled is false
    public void SetColor(AbilityColor abilityColor, bool enabled)
    {
        Color color = AbilityColors.GetColor(abilityColor);
        color.a = enabled ? ALPHA_ENABLED : ALPHA_DISABLED;

        SetRGBA(color, enabled ? ALPHA_ENABLED : ALPHA_DISABLED);

        light_self = GetComponent<Light>();
        light_self.color = color;
        light_self.enabled = enabled;

        abilityColor_self = abilityColor;
    }

    // enables the indicator by enabling the light source and setting the alpha of the material to the enabled alpha and returns the ability color of the indicator
    public AbilityColor Enable()
    {
        SetAlpha(ALPHA_ENABLED);

        light_self.enabled = true;

        return abilityColor_self;
    }

    // disables the indicator by disabling the light source and setting the alpha of the material to the disabled alpha
    public void Disable()
    {
        SetAlpha(ALPHA_DISABLED);

        light_self.enabled = false;
    }

    // returns the ability color of the indicator
    public AbilityColor GetColor()
    {
        return abilityColor_self;
    }

}
