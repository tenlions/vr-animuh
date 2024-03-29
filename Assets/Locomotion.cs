using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Locomotion : MonoBehaviour
{
    public SteamVR_Action_Vector2 locomotionInput;
    public SteamVR_Action_Boolean dashInput;
    public Transform cameraTranform;

    private Rigidbody playerBody;
    private CapsuleCollider capsuleCollider;

    public float movementSpeed = 2.0f;

    public float dashPower = 10f;
    
    private Vector3 movementDir;

    private static readonly Vector3 VEC_NO_Y = new Vector3(1,0,1);


    
        

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(locomotionInput.axis.x, 0, locomotionInput.axis.y));
        movementDir.y = 0;
        
        if (dashInput.stateDown)
        {
            playerBody.AddForce(movementDir * dashPower, ForceMode.VelocityChange);
            Debug.Log("Dash");
        }

        else
        {
            transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * movementSpeed, Vector3.up);
            Debug.Log("Move");
        }            

        float distanceFromFloor = Vector3.Dot(cameraTranform.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);
        capsuleCollider.center = cameraTranform.localPosition - 0.5f * distanceFromFloor * Vector3.up;
    }
}
