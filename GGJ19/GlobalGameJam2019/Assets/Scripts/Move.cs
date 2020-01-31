using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Move : MonoBehaviour {
    public SteamVR_Action_Vector2 moveAction;
    public SteamVR_Action_Vector2 rotationAction;
    public SteamVR_Action_Boolean jumpAction;
    public SteamVR_Input_Sources hand;
    public SizeManager sizeManager;
    public Collider collider;

    private Rigidbody rigidBody;
    private float smallRatio = .5f;
    private float acceleration = 20f;
    private float maxWalkSpeed = 5f;
    private float turnSpeed = 120f;
    private float jumpSpeed = 4.5f;
    private float smallGravityRatio = .5f;

    private void Start() {
       rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update() {
        float sizeRatio = sizeManager.isPlayerSmall() ? smallRatio : 1f;

        Vector2 moveAxis = moveAction.GetAxis(hand);
        Vector2 rotationAxis = rotationAction.GetAxis(hand);
        Vector3 movement = new Vector3(moveAxis.x, 0, moveAxis.y);
        float rot = transform.eulerAngles.y;
        movement = Quaternion.AngleAxis(rot, Vector3.up) * movement;
        Vector3 prevVelocity = rigidBody.velocity;
        Vector3 nextVelocity = prevVelocity + movement * acceleration * sizeRatio * Time.deltaTime;
        if (nextVelocity.magnitude <= maxWalkSpeed * sizeRatio) {
            rigidBody.velocity = nextVelocity; //TODO this could be bad if verticle speed is too high and then you can't move horizontally
        }

        Vector3 rotation = new Vector3(0, rotationAxis.x, 0);
        Quaternion deltaRotation = Quaternion.Euler(rotation * turnSpeed * Time.deltaTime);
        rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);

        //Check for jump
        if (!jumpAction.GetLastState(hand) && jumpAction.GetState(hand) && IsGrounded()) {
            Jump(sizeRatio);
        }
    }

    private void FixedUpdate() {
        if (sizeManager.isPlayerSmall() && !IsGrounded()) {
            rigidBody.AddForce(-Physics.gravity * smallGravityRatio * rigidBody.mass);
        }
    }

    bool IsGrounded() {
        float distanceToGround = collider.bounds.extents.y + .1f;
        //Debug.DrawRay(collider.transform.position, Vector3.down * distanceToGround, Color.green);
        return Physics.Raycast(collider.transform.position, Vector3.down, distanceToGround);
    }

    private void Jump(float sizeRatio)
    {
        rigidBody.velocity = rigidBody.velocity += Vector3.up * jumpSpeed * sizeRatio;
    }
}