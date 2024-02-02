using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbilityController : MonoBehaviour
{

    public IAbility ability_Key1;
    public AbilityColorIndicator abilityColorIndicator;

    // Start is called before the first frame update
    void Start()
    {
        ability_Key1 = GetComponentInChildren<Substitution>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && ability_Key1 != null)
        {
            ability_Key1.Release();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            abilityColorIndicator.SelectColor(AbilityColor.Red);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            abilityColorIndicator.SelectColor(AbilityColor.Grey);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            abilityColorIndicator.SelectColor(AbilityColor.Blue);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            abilityColorIndicator.SelectColor(AbilityColor.Green);
        }
    }
}
