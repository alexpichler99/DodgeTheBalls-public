
using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.UI;

public class Restart : MonoBehaviour, IInputClickHandler
{
    public Manager manager;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (manager != null)
            manager.ResetGame();
    }
}
