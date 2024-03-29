using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for handling the set of abilities of a given color
public interface IAbilityHandler
{
    AbilityColor GetColor();

    void HandleTriggerDown(HandState handState, HandState otherHandState);

    void HandleTriggerUp(HandState handState, HandState otherHandState);

    // handles the calling hand's collision with a given other collider
    void HandleCollision(Collider other);
}
