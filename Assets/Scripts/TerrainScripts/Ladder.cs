using UnityEngine;

public class Ladder : MonoBehaviour {
    #region Variables
    [SerializeField]
    private LadderData ladderData;
    #endregion

    #region Properties
    public LadderData LadderData { get { return ladderData; } }
    #endregion
}
