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

public class ScoreCount : BaseNetLogic
{
    public static Label ScoreTextLabel;
    public override void Start()
    {
        ScoreTextLabel = Owner as Label;
        ScoreTextLabel.Text = "Score: " + MovingScript.GlobalScoreCounter;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public static void Score()
    {
        MovingScript.GlobalScoreCounter++;
        ScoreCount.ScoreTextLabel.Text = "Score: " + MovingScript.GlobalScoreCounter;
    }
}
