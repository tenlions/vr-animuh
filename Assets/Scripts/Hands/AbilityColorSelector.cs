using UnityEngine;
using Valve.VR.InteractionSystem;

// Offers methods to select an ability color 
public class AbilityColorSelector : MonoBehaviour
{

    private AbilityColor currentColor;
    public float threshold = 0.1f;

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
        HandDirection direction = GetHandDirection(hand);

        switch (direction)
        {
            case HandDirection.UP:
                return AbilityColor.Red;
            case HandDirection.OUTWARD:
                return AbilityColor.Grey;
            case HandDirection.DOWN:
                return AbilityColor.Blue;
            case HandDirection.INWARD:
                return AbilityColor.Yellow;
            default:
                return currentColor;
        }
    }

    // Returns the direction of the hand
    private HandDirection GetHandDirection(Hand hand)
    {
        Vector3 velocity = hand.GetTrackedObjectVelocity().normalized;

        // only select a color if the hand is moving fast enough
        if (velocity.magnitude < threshold)
        {
            return HandDirection.NONE;
        }

        Vector3 handForward = hand.transform.forward;

        float angle = Vector3.Angle(handForward, velocity);
        if (angle < 45f)
        {
            return HandDirection.UP;
        }
        else if (angle < 135f)
        {
            return MapHorizontalDirection(hand, HorizontalDirection.RIGHT);
        }
        else if (angle < 225f)
        {
            return HandDirection.DOWN;
        }
        else
        {
            return MapHorizontalDirection(hand, HorizontalDirection.LEFT);
        }
    }

    // Maps the given horizontal direction to the relative hand direction, meaning a left hand going left goes outward while a right hand going left goes inward
    private HandDirection MapHorizontalDirection(Hand hand, HorizontalDirection direction)
    {
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            if (direction == HorizontalDirection.RIGHT)
            {
                return HandDirection.OUTWARD;
            }
            else
            {
                return HandDirection.INWARD;
            }
        }
        else
        {
            if (direction == HorizontalDirection.RIGHT)
            {
                return HandDirection.INWARD;
            }
            else
            {
                return HandDirection.OUTWARD;
            }
        }
    }

    // Returns the current ability color
    public AbilityColor GetCurrentColor()
    {
        return currentColor;
    }

    // the minimum velocity required to select a color
    public float GetThreshold()
    {
        return threshold;
    }

    // Sets the minimum velocity required to select a color
    public void SetThreshold(float threshold)
    {
        this.threshold = threshold;
    }

    // Global directions
    private enum HorizontalDirection 
    { 
        LEFT, RIGHT 
    }

    // Relative hand directions
    private enum HandDirection
    {
        UP, DOWN, INWARD, OUTWARD, NONE
    }
}
