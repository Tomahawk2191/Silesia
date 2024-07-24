using DG.Tweening;
using UnityEngine;

public class CameraForSmallObjects : ICameraMovementType
{
    private PlayerInteract playerInteract;
    private Transform objToMove;
    private Vector3 startPos, startRot;

    public CameraForSmallObjects(PlayerInteract playerInteract, Transform objToMove)
    {
        this.playerInteract = playerInteract;
        this.objToMove = objToMove;
        startPos = objToMove.position;
        startRot = objToMove.eulerAngles;
    }

    public void cameraMoveIn()
    {
        PlayerInteract.input.BlockInputForInteraction();
        objToMove.DOMove(playerInteract.holdPt.position, 2);
        objToMove.DORotate(playerInteract.holdPt.eulerAngles, 2);
       
    }

    public void cameraMoveOut()
    {
        PlayerInteract.input.EnableInputForInteraction();
        objToMove.DOMove(startPos, 2);
        objToMove.DORotate(startRot, 2);
    }
}
