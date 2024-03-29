using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{

    public class HandController : MonoBehaviour
    {

        // the abilities the player has, mapped by color
        public Dictionary<AbilityColor, HashSet<IAbility>> abilities;
        public BlueAbilityHandler handler_blue;
        public IAbilityHandler handler_red;
        public IAbilityHandler handler_yellow;
        public IAbilityHandler handler_grey;
        
        public HandController otherHand;

        // input source
        public SteamVR_Input_Sources input;

        // the trigger    
        public SteamVR_Action_Boolean trigger;

        // the buttons
        public SteamVR_Action_Boolean upperButton;
        public SteamVR_Action_Boolean lowerButton;

        // the hand
        public Hand hand;

        // the current color mode of the hand
        public AbilityColor currentColor;

        // whether the trigger is currently held
        private bool triggerHeld;
        private bool lowerButtonHeld;
        private bool upperButtonHeld;

        private HandPose currentPose;
        private HandPosition currentPosition;

        public IAbility punchScript;
        public AbilityColorSelector abilityColorSelector;

        public GameObject obj_abilityColorIndicator;

        // Start is called before the first frame update
        void Start()
        {
            RegisterListeners();

            abilities = new Dictionary<AbilityColor, HashSet<IAbility>>();
            HashSet<IAbility> blueAbilities = new HashSet<IAbility>();
            blueAbilities.Add(punchScript);
            abilities.Add(AbilityColor.Blue, blueAbilities);
        }

        // Update is called once per frame
        void Update()
        {
            if (triggerHeld)
            {
                ChargeAbilities(currentColor);
            }
        }

        void OnActionPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;
            
            if (fromAction == lowerButton)
            {
                lowerButtonHeld = true;
                currentPose = HandPose.AIM;
                currentPosition = HandPosition.AIM;
            }
            else if (fromAction == trigger)
            {
                triggerHeld = true;
                GetAbilityHandler().HandleTriggerDown(GetCurrentState(), otherHand.GetCurrentState());
            }
        }

        void OnActionReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;

            if (fromAction == upperButton)
            {
                upperButtonHeld = false;
            }
            else if (fromAction == lowerButton)
            {
                lowerButtonHeld = false;
                currentPose = HandPose.OPEN;
            }
            else if (fromAction == trigger)
            {
                triggerHeld = false;
                GetAbilityHandler().HandleTriggerUp(GetCurrentState(), otherHand.GetCurrentState());
            }
        }

        void OnColorSelectButtonPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;

            if (fromAction == upperButton)
            {
                abilityColorSelector.SetForwardPressed(hand);
                upperButtonHeld = true;
            }
        }

        void OnColorSelectButtonReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;

            if (fromAction == upperButton)
            {
                currentColor = abilityColorSelector.SelectColor(hand);
                obj_abilityColorIndicator.GetComponent<AbilityColorIndicator>().SelectColor(currentColor);
                upperButtonHeld = false;
            }
        }

        public HandPose GetCurrentPose()
        {
            return currentPose;
        }

        public HandPosition GetCurrentPosition()
        {
            return currentPosition;
        }

        public HandState GetCurrentState()
        {
            return new HandState(currentPose, currentPosition, Time.time);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<IHittable>() == null) return;

            GetAbilityHandler().HandleCollision(other);
        }

        private IAbilityHandler GetAbilityHandler()
        {
            switch (currentColor)
            {
                case AbilityColor.Blue:
                    return handler_blue;
                case AbilityColor.Red:
                    return handler_red;
                case AbilityColor.Yellow:
                    return handler_yellow;
                default:
                    return handler_grey;
            }
        }

        private bool handEqualsFromSource(SteamVR_Input_Sources fromSource)
        {
            return fromSource.ToString().Equals(hand.name);
        }

        private void CastAbilities(AbilityColor color)
        {
            HashSet<IAbility> abilities = GetAbilities(color);
            foreach (IAbility ability in abilities)
            {
                ability.Cast();
            }
        }

        private void ChargeAbilities(AbilityColor color)
        {
            HashSet<IAbility> abilities = GetAbilities(color);
            foreach (IAbility ability in abilities)
            {
                ability.Charge();
            }
        }

        private void ReleaseAbilities(AbilityColor color)
        {
            HashSet<IAbility> abilities = GetAbilities(color);
            foreach (IAbility ability in abilities)
            {
                ability.Release();
            }
        }

        private HashSet<IAbility> GetAbilities(AbilityColor color)
        {
            if (abilities.ContainsKey(color))
            {
                return abilities[color];
            }
            else
            {
                return new HashSet<IAbility>();
            }
        }

        // Registers the up and down state listeners for the trigger and buttons
        private void RegisterListeners()
        {
            trigger.AddOnStateDownListener(OnActionPressed, input);
            trigger.AddOnStateUpListener(OnActionReleased, input);

            upperButton.AddOnStateDownListener(OnActionPressed, input);
            upperButton.AddOnStateDownListener(OnColorSelectButtonPressed, input);
            upperButton.AddOnStateUpListener(OnActionReleased, input);
            upperButton.AddOnStateUpListener(OnColorSelectButtonReleased, input);

            lowerButton.AddOnStateDownListener(OnActionPressed, input);
            lowerButton.AddOnStateUpListener(OnActionReleased, input);
        }
    }

}
