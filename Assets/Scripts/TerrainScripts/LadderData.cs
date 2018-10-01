using UnityEngine;

[System.Serializable]
public class LadderData {
    [SerializeField]
    private Transform bottomLimit;
    [SerializeField]
    private Transform topLimit;
    [SerializeField]
    private Transform exitBottomPosition;
    [SerializeField]
    private Transform exitTopPosition;

    public Vector3 BottomLimit { get { return bottomLimit.position; } }
    public Vector3 TopLimit { get { return topLimit.position; } }
    public Vector3 ExitBottomPosition { get { return exitBottomPosition.position; } }
    public Vector3 ExitTopPosition { get { return exitTopPosition.position; } }

    public LadderData() {
        bottomLimit = null;
        topLimit = null;
        exitBottomPosition = null;
        exitTopPosition = null;
    }
    public LadderData(Transform bottomLimit, Transform topLimit, Transform exitBottomPosition, Transform exitTopPosition) {
        this.bottomLimit = bottomLimit;
        this.topLimit = topLimit;
        this.exitBottomPosition = exitBottomPosition;
        this.exitTopPosition = exitTopPosition;
    }
}
