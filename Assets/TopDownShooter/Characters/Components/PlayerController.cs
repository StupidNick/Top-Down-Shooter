using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Handling
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 10;

    //System
    private Quaternion targetRotation;

    //Components
    private CharacterController controller;
    private Camera cam;
    public ItemsComponent itemsComponent;
    

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    
    void Update()
    {
        ControlMouse();

        if(Input.GetButton("Shoot"))
        {
            itemsComponent.GetCurrentWeapon().GetComponent<BaseWeaponComponent>().StartShooting();
        }
        if(Input.GetButtonUp("Shoot"))
        {
            itemsComponent.GetCurrentWeapon().GetComponent<BaseWeaponComponent>().ResetShootingCounter();
        }
        if(Input.GetButtonDown("Reload"))
        {
            itemsComponent.GetCurrentWeapon().GetComponent<BaseWeaponComponent>().StartReload();
        }
    }


    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);
    }
}
