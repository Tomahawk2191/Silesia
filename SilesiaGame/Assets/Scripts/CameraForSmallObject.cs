using DG.Tweening;
using UnityEngine;

public class CameraForSmallObjects : ICameraMovementType
{
    private PlayerInteract playerInteract;
    private Transform objToMove;
    private Vector3 startPos, startRot;

    public CameraForSmallObjects(Transform objToMove)
    {
        playerInteract = PlayerInteract.Instance;
        this.objToMove = objToMove;
        startPos = objToMove.position;
        startRot = objToMove.eulerAngles;
    }

    public void cameraMoveIn()
    {
        PlayerInteract.input.BlockInputForInteraction();
        objToMove.GetChild(0).DOMove(PlayerInteract.Instance.objPos.transform.position, 2);
        //objToMove.GetChild(0).DORotate(PlayerInteract.Instance.objPos.transform.eulerAngles, 2);
       
    }

    public void cameraMoveOut()
    {
        PlayerInteract.input.EnableInputForInteraction();
        objToMove.GetChild(0).DOMove(startPos, 2);
        objToMove.GetChild(0).DORotate(startRot, 2);
    }
}
