using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    public LayerMask whatIsStock;

    public InputActionReference moveAction;

    public InputActionReference jumpAction;

    public InputActionReference lookAction;

    public CharacterController charCon;

    public Camera theCam;

    private GameObject heldPickup;

    public Transform holdPoint;

    public float moveSpeed;

    private float ySpeed;

    public float jumpForce;

    private float horiRot, vertRot;

    public float lookSpeed;

    public float minLookAngle, maxLookAngle;

    public float interactionRange;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        horiRot += lookInput.x * Time.deltaTime * lookSpeed;
        transform.rotation = Quaternion.Euler(0f, horiRot, 0f);

        vertRot -= lookInput.y * Time.deltaTime * lookSpeed;
        vertRot = Mathf.Clamp(vertRot, minLookAngle, maxLookAngle);
        theCam.transform.localRotation = Quaternion.Euler(vertRot, 0f, 0f);

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        //Debug.Log(moveInput);

        //transform.position = transform.position + new Vector3(moveInput.x * Time.deltaTime * moveSpeed, 0f, moveInput.y * Time.deltaTime * moveSpeed);

        //Vector3 moveAmount = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 vertMove = transform.forward * moveInput.y;
        Vector3 horiMove = transform.right * moveInput.x;
        //Debug.Log(vertMove + "-" + horiMove);

        Vector3 moveAmount = horiMove + vertMove;
        moveAmount = moveAmount.normalized;

        moveAmount = moveAmount * moveSpeed;

        if (charCon.isGrounded == true)
        {
            ySpeed = 0f;

            if (jumpAction.action.WasPressedThisFrame())
            {
                ySpeed = jumpForce;
            }
        }

        ySpeed = ySpeed + (Physics.gravity.y * Time.deltaTime);

        moveAmount.y = ySpeed;

        charCon.Move(moveAmount * Time.deltaTime);

        Ray ray = theCam.ViewportPointToRay(new Vector3 (.5f, .5f, 0f));
        RaycastHit hit;

        //if(Physics.Raycast(ray, out hit, interactionRange, whatIsStock))
        //{
        //    Debug.Log("I see a pickup");
        //}
        //else
        //{
        //    Debug.Log("I can't see anything!!!");
        //}

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (Physics.Raycast(ray, out hit, interactionRange, whatIsStock))
            {
                //Debug.Log("I see a pickup");

                heldPickup = hit.collider.gameObject;
                heldPickup.transform.SetParent(holdPoint);
                heldPickup.transform.localPosition = Vector3.zero;
                heldPickup.transform.localRotation = Quaternion.identity;

                heldPickup.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            heldPickup.GetComponent<Rigidbody>().isKinematic = false;

            heldPickup.transform.SetParent(null);
            heldPickup = null;
        }
    }
}
