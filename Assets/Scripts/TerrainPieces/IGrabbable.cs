using UnityEngine;
public interface IGrabbable {
    void Grab(Transform grabParent);
    void Drop(Transform dropPosition);
}
