using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.InteractionSystem
{
    public class BlueFireScript : MonoBehaviour
    {

        private GameObject fireObject = null;
        private bool isBurning = false;

        public GameObject fireParticlePrefab;
        public bool startActive;

        public ParticleSystem customParticles;

        private float burnTime = 10;
        public float ignitionDelay = 0;
        private float ignitionTime;

        public AudioSource ignitionSound;

        public bool canSpreadFromThisSource = true;

        private bool hasLogged = false;


        public GameObject fireParticle;
        public Transform sphereTransform;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!hasLogged && isBurning)
            {
                Debug.Log("In Update");
                Debug.Log("Time: " + Time.time);
                Debug.Log("ignitionTime: " + ignitionTime);
                Debug.Log("Result: " + (Time.time - ignitionTime));
                Debug.Log("BurnTime: " + burnTime);
                hasLogged = true;
            }
            
            if (isBurning && ((Time.time - ignitionTime) >= burnTime)) {
                Debug.Log("Stopping burn");
                Invoke("StopBurning", 0f);
            }
        }

        private void BlueFireExposure()
        {
            if (fireObject == null)
            {
                Debug.Log("Ball exposed to fire, invoking");
                Invoke("StartBurning", ignitionDelay);
            }


        }

        private void StopBurning() 
        {
            isBurning = false;
            ignitionTime = 0;

            if (fireObject != null)
            {
                Destroy(fireObject);
                fireObject = null;
            }

            if (fireParticle != null) fireParticle.SetActive(false);
        }

        private void StartBurning()
        {
            // only start if not already burning
            if (isBurning) return;

            Debug.Log("Starting burn");

            isBurning = true;
            ignitionTime = Time.time;
            Debug.Log("ignitionTime: " + ignitionTime);

            // Play the fire ignition sound if there is one
            if (ignitionSound != null)
            {
                ignitionSound.Play();
            }

            if (fireParticlePrefab != null)
            {
                //Debug.Log("Instantiating particle object");
                //fireObject = Instantiate(fireParticlePrefab, sphereTransform) as GameObject;
            }

            if (fireParticle != null) fireParticle.SetActive(true);
            
        }
    }
}


