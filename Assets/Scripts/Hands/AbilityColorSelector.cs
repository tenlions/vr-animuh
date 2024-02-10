using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

// Offers methods to select an ability color 
public class AbilityColorSelector : MonoBehaviour
{

    private AbilityColor currentColor;
    public float threshold = 0.00f;

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

    private struct DirectionAngles
    {

    }

    private Vector3 OriginToVelocity(Vector3 velocity) 
    {
        return velocity - transform.position;
    }

    private float AngleBetween(Vector3 velocity, Vector3 direction) 
    {
        return Vector3.Angle(velocity, direction);
    }

    private NonRelativeDirection LowestAngle(Vector3 velocity)
    {
        Dictionary<NonRelativeDirection, float> angles = new Dictionary<NonRelativeDirection, float>()
        {
            {NonRelativeDirection.UP, AngleBetween(OriginToVelocity(velocity), DirVec(transform.up)) },
            {NonRelativeDirection.DOWN, AngleBetween(OriginToVelocity(velocity), DirVec(-transform.up)) },
            {NonRelativeDirection.LEFT, AngleBetween(OriginToVelocity(velocity), DirVec(-transform.right)) },
            {NonRelativeDirection.RIGHT, AngleBetween(OriginToVelocity(velocity), DirVec(transform.right)) },
        };

        return angles.OrderBy(e => e.Value).First().Key;
    }

    private Vector3 DirVec(Vector3 direction) 
    {
        return transform.position + direction;
    }

    // Returns the direction of the hand
    private HandDirection GetHandDirection(Hand hand)
    {
        Vector3 vec3_velocity = hand.GetTrackedObjectVelocity();

        if (true)
        {

            NonRelativeDirection dir = LowestAngle(vec3_velocity);
            if (NonRelativeDirection.LEFT == dir)
            {
                Debug.Log("LEFT");
                return HandDirection.INWARD;
            }

            if (NonRelativeDirection.RIGHT == dir)
            {
                Debug.Log("RIGHT");
                return HandDirection.OUTWARD;
            }

            if (NonRelativeDirection.UP == dir)
            {
                Debug.Log("UP");
                return HandDirection.UP;
            }

            if (NonRelativeDirection.DOWN == dir)
            {
                Debug.Log("DOWN");
                return HandDirection.DOWN;
            }

            return HandDirection.NONE;
        }

        // only select a color if the hand is moving fast enough
        if (vec3_velocity.magnitude < threshold)
        {
            return HandDirection.NONE;
        }

        Vector3 handForward = hand.transform.up;

        float angle = Vector3.Angle(handForward, vec3_velocity);
        Debug.Log("-----------------------------------------------");
        Debug.Log("Forward: " + handForward);
        Debug.Log("Velocity: " + vec3_velocity);
        Debug.Log("Magnitude: " + vec3_velocity.magnitude);
        Debug.Log("Angle: " + angle);
        Debug.Log("-----------------------------------------------");

        if (angle < 45f)
        {
            Debug.Log("UP UP UP");
            return HandDirection.UP;
        }
        else if (angle < 135f)
        {
            Debug.Log("RIGHT RIGHT RIGHT");
            return MapHorizontalDirection(hand, NonRelativeDirection.RIGHT);
        }
        else if (angle < 225f)
        {
            Debug.Log("DOWN DOWN DOWN");
            return HandDirection.DOWN;
        }
        else
        {
            Debug.Log("LEFT LEFT LEFT");
            return MapHorizontalDirection(hand, NonRelativeDirection.LEFT);
        }
    }

    // Maps the given horizontal direction to the relative hand direction, meaning a left hand going left goes outward while a right hand going left goes inward
    private HandDirection MapHorizontalDirection(Hand hand, NonRelativeDirection direction)
    {
        if (hand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            if (direction == NonRelativeDirection.RIGHT)
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
            if (direction == NonRelativeDirection.RIGHT)
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
    private enum NonRelativeDirection 
    { 
        LEFT, RIGHT, UP, DOWN 
    }

    // Relative hand directions
    private enum HandDirection
    {
        UP, DOWN, INWARD, OUTWARD, NONE
    }
}
