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
using System.Security.Cryptography.X509Certificates;
#endregion

public class ChangeColor : BaseNetLogic
{
    public Rectangle ObjectItem;
    public Color Color1;
    public int Counter;
    public UAValue ColorCode;
    public override void Start()
    {
        ObjectItem = Owner as Rectangle;
        ColorCode = 4292505814u;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void ChangeColorMethod()
    {
        if (Counter == 5)
        {
            ObjectItem.FillColor = Colors.PeachPuff;
        }
        if (Counter == 4)
        {
            ObjectItem.FillColor = Colors.LightSeaGreen;
        }
        if (Counter == 3)
        {
            ObjectItem.FillColor = Colors.SeaShell;
        }
        if (Counter == 2)
        {
            ObjectItem.FillColor = Colors.Red;
        }
        if (Counter == 1)
        {
            ObjectItem.FillColor = ColorCode;
        }
        if (Counter == 0)
        {
            Color1 = Colors.DarkBlue;
            ObjectItem.FillColor = Color1;
        }

        Random MyRandomGenerator = new Random();
        int RandomNumber = MyRandomGenerator.Next(0, 5);
        Counter = RandomNumber;
        Log.Info(ObjectItem.FillColor.ARGB.ToString());
        Log.Info("Random Number: " + RandomNumber);
        Log.Info(ObjectItem.FillColor.ARGB.ToString());
    }
}
