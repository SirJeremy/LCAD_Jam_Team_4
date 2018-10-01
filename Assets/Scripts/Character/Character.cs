using UnityEngine;

public class Character : MonoBehaviour, IDamageable {
    #region Variables
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private Transform grabPosition;
    [SerializeField]
    private Transform dropPosition;
    [SerializeField]
    private int health = 1;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float ladderMoveSpeed = 2;
    [SerializeField]
    private float jumpSpeed = 5;
    [SerializeField]
    private float airControl = 1;
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


    private Vector3 airVelocity = Vector3.zero;
    private float horizontalLook = 0;
    private float verticalLook = 0;
    private bool isGrabbing = false;
    private bool isClimbingLadder = false;
    private IGrabbable grabbedObject;
    private LadderData ladderData;

    private float ySpeed = 0;

    public delegate void Empty();
    public static event Empty OnDeath;
    #endregion

    #region Properties
    public int Health {
        get { return health; }
        set {
            if (value < 0) {
                health = 0;
                Die();
            }                
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
            verticalLook = Mathf.Clamp(value, -90, 90);
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
        if(isClimbingLadder) {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) {
                UnmountLadder();
            }
            else
                MoveLadder();
        }
        else {
            Move();
            if (isGrabbing) {
                if (Input.GetKeyDown(KeyCode.E))
                    Drop();
            }
            else if (Input.GetKeyDown(KeyCode.E))
                Use();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Conveyor conveyor = hit.collider.gameObject.GetComponent<Conveyor>();
        DamageMesh damageMesh = hit.collider.gameObject.GetComponent<DamageMesh>();
        if(conveyor != null) {
            characterController.Move(conveyor.Velocity * Time.deltaTime);
        }
        if(damageMesh != null) {
            Damage(1);
        }
    }
    #endregion

    #region Methods
    public void Damage(int amount) {
        Health -= amount;
    }
    private void Die() {
        this.enabled = false;
        if(OnDeath != null) {
            OnDeath();
        }
    }
    private void Rotate() {
        HorizontalLook += Input.GetAxisRaw("Mouse X") * horizontalSensitivity;
        VerticalLook += Input.GetAxisRaw("Mouse Y") * -verticalSensitivity;
        transform.localRotation = Quaternion.Euler(new Vector3(0, HorizontalLook, 0));
        cam.localRotation = Quaternion.Euler(new Vector3(VerticalLook, 0, 0));
    }
    private void Move() {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDir = moveDir.normalized * speed;
        if(characterController.isGrounded) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                ySpeed = jumpSpeed;
                airVelocity = moveDir;
            }
            else {
                airVelocity = Vector3.zero;
                ySpeed = -1;
            }
                
            moveDir += Vector3.up * ySpeed;
        }
        else {
            airVelocity = Vector3.Lerp(airVelocity, moveDir, airControl * Time.deltaTime);
            moveDir = airVelocity;
            moveDir += Physics.gravity * gravityMultiplyer * Time.deltaTime;
            moveDir += Vector3.up * ySpeed;
        }
        ySpeed = moveDir.y;
        moveDir *= Time.deltaTime;
        moveDir = transform.localRotation * moveDir;
        characterController.Move(moveDir);
        
    }
    private void Use() {
        RaycastHit hit;
        IUseable useable;
        IGrabbable grabbable;
        Ladder ladder;
        if(Physics.SphereCast(cam.transform.position, useSphereCastRadius, cam.transform.forward, out hit, useRange)) {
            useable = hit.collider.gameObject.GetComponent<IUseable>();
            grabbable = hit.collider.gameObject.GetComponent<IGrabbable>();
            ladder = hit.collider.gameObject.GetComponent<Ladder>();
            if (useable != null)
                useable.Use();
            if(grabbable != null) {
                grabbable.Grab(grabPosition);
                Grab(grabbable);
            }
            if(ladder != null) {
                ladderData = ladder.LadderData;
                MountLadder();
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * useRange, Color.red, .5f);
    }
    private void Grab(IGrabbable grabbedObject) {
        isGrabbing = true;
        this.grabbedObject = grabbedObject;
    }
    private void Drop() {
        if (grabbedObject == null)
            return;
        grabbedObject.Drop(dropPosition);
        isGrabbing = false;
    }
    private void MountLadder() {
        isClimbingLadder = true;
        float newY = transform.position.y;
        newY = Mathf.Clamp(newY, ladderData.BottomLimit.y, ladderData.TopLimit.y);
        transform.position = new Vector3(ladderData.BottomLimit.x, newY, ladderData.BottomLimit.z);
        ySpeed = 0;
    }
    private void UnmountLadder() {
        isClimbingLadder = false;
        ladderData = null;
    }
    private void MoveLadder() {
        Vector3 moveDir = Vector3.up * Input.GetAxisRaw("Vertical") * ladderMoveSpeed * Time.deltaTime;
        float newY = moveDir.y + transform.position.y;
        if (newY > ladderData.TopLimit.y) {
            transform.position = ladderData.ExitTopPosition;
            UnmountLadder();
        }
        else if (newY < ladderData.BottomLimit.y) {
            transform.position = ladderData.ExitBottomPosition;
            UnmountLadder();
        }
        else
            characterController.Move(moveDir);
    }
    #endregion
}
