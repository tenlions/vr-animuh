using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using Valve.VR.InteractionSystem;
using Vector3 = UnityEngine.Vector3;

// Offers methods to select an ability color 
public class AbilityColorSelector : MonoBehaviour
{

    private AbilityColor currentColor;

    private static readonly Vector3 V3_DEFAULT = new Vector3(666, 666, 666);
    private Vector3 point_forward_pressed = V3_DEFAULT;
    private const int IDX_X = 0;
    private const int IDX_Z = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Selects an ability color based on the hand's direction
    public AbilityColor SelectColor(Hand hand)
    {
        // only select a color if we have usable values
        if (point_forward_pressed == V3_DEFAULT) return currentColor;
        
        // get the vector between the pressed and released point
        Vector3 between = point_forward_pressed - GetHandForward(hand);
        int highestIndex = GetHighestValueIndex(between);
        float highestValue = between[highestIndex];

        // if the highest value is on the x component and we are the left hand, invert value
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.LeftHand && highestIndex == IDX_X) highestValue = -highestValue;

        Debug.Log("highest comp: " + (highestIndex == IDX_X ? "X" : "Z"));
        Debug.Log("highest value: " + highestValue);

        // reset the pressed point
        point_forward_pressed = V3_DEFAULT;

        // and return the color
        switch (highestIndex)
        {
            case IDX_X:
                if (highestValue > 0) return AbilityColor.Red; // inward
                else return AbilityColor.Grey; // outward
            case IDX_Z:
                if (highestValue > 0) return AbilityColor.Green;
                else return AbilityColor.Blue;
            default:
                return currentColor;
        }
    }

    public void SetForwardPressed(Hand hand)
    {
        this.point_forward_pressed = GetHandForward(hand);
    }

    // Returns the "forward" direction of the hand. Forward is the hand's forward vector rotated 45 degrees around the hand's right vector
    private Vector3 GetHandForward(Hand hand)
    {
        return Quaternion.AngleAxis(45, hand.transform.right) * hand.transform.forward;
    }

    private int GetHighestValueIndex(Vector3 vector)
    {
        float absX = Mathf.Abs(vector.x);
        float absZ = Mathf.Abs(vector.z);

        return absX > absZ ? IDX_X : IDX_Z;
    }

    // Returns the current ability color
    public AbilityColor GetCurrentColor()
    {
        return currentColor;
    }
}
