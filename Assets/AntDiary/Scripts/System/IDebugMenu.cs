using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebugMenu
{
    string pageTitle { get; }
    void OnGUIPage();
}
