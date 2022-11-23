using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(CharacterController), typeof(HealthComponent))]
public class PlayerController : MonoBehaviour
{
    // Handling
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 10;

    //System
    private Quaternion targetRotation;
    private static LayerMask _layerMask; 
    [SerializeField] private LayerMask layerMask;

    //Components
    private CharacterController _controller;
    private Camera _camera;
    private ItemsComponent _itemsComponent;
    private HealthComponent _healthComponent;
    private List<MonoBehaviour> _deathList;
    

    void Start()
    {
        _layerMask = layerMask;
        _controller = GetComponent<CharacterController>();
        _itemsComponent = GetComponent<ItemsComponent>();
        _healthComponent = GetComponent<HealthComponent>();
        _camera = Camera.main;

        _healthComponent.Dead += OnPlayerDeath;
    }

    public void AddComponentToDeathList(MonoBehaviour component)
    {
        _deathList.Add(component);
    }

    
    void Update()
    {
        ControlMouse();

        if(Input.GetButton("Shoot"))
        {
            _itemsComponent.StartShootFromCurrentWeapon();
        }
        if(Input.GetButtonUp("Shoot"))
        {
            _itemsComponent.GetCurrentWeapon().ResetShootingCounter();
        }
        if(Input.GetButtonDown("Reload"))
        {
            _itemsComponent.GetCurrentWeapon().StartReload();
        }
    }


    private void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _camera.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        _controller.Move(motion * Time.deltaTime);
    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            return raycastHit.point;
        }
        return new Vector3(0,0,0);
    }


    private void OnPlayerDeath()
    {
        foreach (var component in _deathList)
        {
            component.enabled = false;
        }
    }
}
