using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{

    public class HandController : MonoBehaviour
    {

        // the abilities the player has, mapped by color
        public Dictionary<AbilityColor, HashSet<IAbility>> abilities;

        // input source
        private SteamVR_Input_Sources input = SteamVR_Input_Sources.RightHand;

        // the trigger    
        public SteamVR_Action_Boolean trigger;

        // the hand
        public Hand hand;

        // the current color mode of the hand
        public AbilityColor currentColor;

        // whether the trigger is currently held
        private bool triggerHeld;

        public IAbility punchScript;

        // Start is called before the first frame update
        void Start()
        {
            trigger.AddOnStateDownListener(OnTriggerPressed, input);
            trigger.AddOnStateUpListener(OnTriggerReleased, input);

            abilities = new Dictionary<AbilityColor, HashSet<IAbility>>();
            HashSet<IAbility> blueAbilities = new HashSet<IAbility>();
            blueAbilities.Add(GetComponent<Punch>());
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

        private void OnTriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;
            triggerHeld = true;

            CastAbilities(currentColor);
        }

        private void OnTriggerReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;
            triggerHeld = false;
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
    }

}
