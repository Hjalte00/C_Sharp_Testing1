#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.Retentivity;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.NetLogic;
using FTOptix.CoreBase;
using FTOptix.Core;
#endregion

public class ChangeColorRandom : BaseNetLogic
{
    public Rectangle ObjectItem;

    public override void Start()
    {
        ObjectItem = Owner as Rectangle;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void ChangeColorMethod()
    {
        Random RandomUAValue = new Random();
        UInt32 RandomNumber = (UInt32)RandomUAValue.NextInt64(0, 4292505814);
        Log.Info(RandomNumber.ToString());
        ObjectItem.FillColor = (UAValue)RandomNumber;
    }
}
