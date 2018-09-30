using UnityEngine;

public class Character : MonoBehaviour {
    #region Variables
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private int health = 1;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float gravityMultiplyer = 1;
    [SerializeField]
    private float horizontalSensitivity = 1;
    [SerializeField]
    private float verticalSensitivity = 1;
    [SerializeField]
    private float useSphereCastRadius = .1f;
    [SerializeField]
    private float useRange = 3;

    private float horizontalLook = 0;
    private float verticalLook = 0;
    #endregion

    #region Properties
    public int Health {
        get { return health; }
        set {
            if (value < 0)
                health = 0;
            else
                health = value;
        }
    }
    private float HorizontalLook {
        get { return horizontalLook; }
        set {
            horizontalLook = Mathf.Repeat(value, 360);
        }
    }
    private float VerticalLook {
        get { return verticalLook; }
        set {
            verticalLook = Mathf.Clamp(value, -180, 180);
        }
    }
    #endregion

    #region MonoBehaviours
    private void Start() {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
    }
    private void Update() {
        Rotate();
        Move();
        if (Input.GetKeyDown(KeyCode.E))
            Use();
    }
    #endregion

    #region Methods
    private void Rotate() {
        HorizontalLook += Input.GetAxisRaw("Mouse X") * horizontalSensitivity;
        VerticalLook += Input.GetAxisRaw("Mouse Y") * -verticalSensitivity;
        transform.localRotation = Quaternion.Euler(new Vector3(0, HorizontalLook, 0));
        cam.localRotation = Quaternion.Euler(new Vector3(VerticalLook, 0, 0));
    }
    private void Move() {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDir = moveDir.normalized * speed * Time.deltaTime;
        moveDir = transform.localRotation * moveDir;
        characterController.Move(moveDir);
    }
    private void Use() {
        RaycastHit hit;
        IInteractable interactable;
        IUseable useable;
        if(Physics.SphereCast(cam.transform.position, useSphereCastRadius, cam.transform.forward, out hit, useRange)) {
            interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            useable = hit.collider.gameObject.GetComponent<IUseable>();
            if (interactable != null)
                interactable.Interact();
            if (useable != null)
                useable.Use();
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * useRange, Color.red, .5f);
    }
    #endregion
}
