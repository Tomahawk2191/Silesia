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
        objToMove.DOMove(PlayerInteract.Instance.objPos.transform.position, 2);
        //for (int i = 0; i < objToMove.childCount; i++)
        //{
        //    objToMove.GetChild(i).DOMove(PlayerInteract.Instance.objPos.transform.position, 2);
        //}
        // objToMove.GetChild(0).DORotate(PlayerInteract.Instance.objPos.transform.eulerAngles, 2);
    }

    public void cameraMoveOut()
    {
        PlayerInteract.input.EnableInputForInteraction();
        objToMove.DOMove(startPos, 2);
        objToMove.DORotate(startRot, 2);
    }
}
