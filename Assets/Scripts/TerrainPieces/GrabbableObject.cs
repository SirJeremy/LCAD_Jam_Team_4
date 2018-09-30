using UnityEngine;

public class GrabbableObject : MonoBehaviour, IGrabbable {
    #region Variable
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private BoxCollider boxCollider;
    #endregion

    #region Methods
    public void Grab(Transform grabParent) {
        transform.parent = grabParent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        rb.isKinematic = true;
        boxCollider.enabled = false;
    }
    public void Drop(Transform dropPosition) {
        transform.parent = null;
        transform.localPosition = dropPosition.position;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        rb.isKinematic = false;
        boxCollider.enabled = true;
        rb.velocity = Vector3.zero;
    }
    #endregion
}
