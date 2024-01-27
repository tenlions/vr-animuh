using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class FireBallShooter : MonoBehaviour
    {

        private SteamVR_Input_Sources rightInput = SteamVR_Input_Sources.RightHand;
        public SteamVR_Action_Boolean fireballButton;

        public Transform fireballChargePosition;

        public Hand hand;

        public AudioSource audioSource;

        // the prefab to instantiate when the fireball is cast
        public GameObject blueFireballPrefab;
        // the transform to spawn and charge the fireball at
        public Transform fireballSpawn;
        // the speed at which the fireball grows
        public float growSpeed = 0.1f;
        // the point towards which to aim the fireball
        public Transform aimPoint;

        private GameObject currentFireball2;
        private bool buttonPressed2;
        private Timer fireballTimer;

        const float MAX_CHARGE = .1f;
        const float INITIAL_SCALE = .01f;
        const float CHARGE_TIME = 3f;

        // Start is called before the first frame update
        void Start()
        {
            fireballButton.AddOnStateDownListener(OnTriggerPressed, rightInput);
            fireballButton.AddOnStateUpListener(OnTriggerReleased, rightInput);

            buttonPressed2 = false;
        }

        // Update is called once per frame
        void Update()
        {

            if (currentFireball2 != null && buttonPressed2)
            {
                //Debug.DrawLine(currentFireball2.transform.position, aimPoint.position, Color.red, 1f);
                //Debug.DrawRay(currentFireball2.transform.position, aimPoint.position, Color.green, 1f);
                ChargeFireball2();
            }

        }

        private void OnTriggerPressed(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;
            buttonPressed2 = true;


            // if we dont have a fireball, start charging
            if (currentFireball2 == null)
            {
                CreateFireball2();
            }
        }

        private void OnTriggerReleased(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
        {
            // only react to right hand
            if (!handEqualsFromSource(fromSource)) return;

            ShootFireball2();

            buttonPressed2 = false;
        }

        private bool handEqualsFromSource(SteamVR_Input_Sources fromSource)
        {
            return fromSource.ToString().Equals(hand.name);
        }


        private void CreateFireball2()
        {
            if (currentFireball2 != null) return;


            fireballTimer = new Timer(CHARGE_TIME, this.name);
            fireballTimer.StartTimer();

            currentFireball2 = GameObject.Instantiate(blueFireballPrefab, fireballSpawn.position, fireballSpawn.rotation);
            currentFireball2.transform.localScale *= INITIAL_SCALE;
        }

        private void ChargeFireball2()
        {
            // make sure we really have a fireball
            if (currentFireball2 == null) return;

            currentFireball2.transform.position = fireballSpawn.position;
            if (currentFireball2.transform.localScale.x < MAX_CHARGE)
            {
                currentFireball2.transform.localScale += Vector3.one * (growSpeed * Time.deltaTime);
            }

        }

        private void ShootFireball2()
        {
            // only shoot if we have a fireball
            if (currentFireball2 == null) return;

            Vector3 shotDirection = aimPoint.position - currentFireball2.transform.position;
            shotDirection.Normalize();

            currentFireball2.transform.rotation = Quaternion.identity;
            currentFireball2.SendMessage("FireProjectile", shotDirection, SendMessageOptions.DontRequireReceiver);

            fireballTimer = null;
            currentFireball2 = null;
        }
    }
}

    
