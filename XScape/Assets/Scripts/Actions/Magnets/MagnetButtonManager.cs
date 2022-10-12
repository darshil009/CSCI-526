using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetButtonManager : MonoBehaviour
{
    [SerializeField] List<MagnetButtonController> magnetButtonControllers;
    public void DeactivateAll()
    {
        foreach(MagnetButtonController magnetButtonController in magnetButtonControllers)
        {
            magnetButtonController.Deactivate();
        }   
    }

    public void AddMagnetButtonController(MagnetButtonController magnetButtonController)
    {
        magnetButtonControllers.Add(magnetButtonController);
    }
}
