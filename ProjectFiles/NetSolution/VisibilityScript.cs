#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.Core;
using FTOptix.CoreBase;
using FTOptix.NetLogic;
#endregion

public class VisibilityScript : BaseNetLogic
{
    public Item RectangleItem;
    public override void Start()
    {
        RectangleItem = Owner as Item;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void VisibilityMethod()
    {
        RectangleItem.Visible = !RectangleItem.Visible;
    }
}
