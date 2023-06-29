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
using System.Drawing;
using System.Diagnostics;
#endregion

public class MovingScript : BaseNetLogic
{
    public FTOptix.UI.Rectangle MovingRectangle;
    public Item MovingRectangleItem;
    public static int GlobalScoreCounter = 0;
    private PeriodicTask myPeriodicTask;
    public FTOptix.Core.Color StartColor;
    public FTOptix.Core.Color EndColor;
    public System.Drawing.Color DynamicColor;
    public float Duration = 1000f;
    public float TimeCounter = 0f;
    public Stopwatch CountingWatch;

    public override void Start()
    {
        MovingRectangle = Owner as FTOptix.UI.Rectangle;
        MovingRectangleItem = Owner as Item;
        MovingRectangle.FillColor = Colors.LightGreen;
        StartColor = Colors.LightGreen;
        EndColor = Colors.Red;
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
    }

    [ExportMethod]
    public void MovingMethod()
    {

        MovingRectangle.Height = MovingRectangle.Height - 1;
        MovingRectangle.Width = MovingRectangle.Width - 1;
        ScoreCount.Score();
        ColorMethod();

    }

    [ExportMethod]
    public void ColorMethod()
    {
        myPeriodicTask = new PeriodicTask(ColorChangeMethod, 5, LogicObject);
        myPeriodicTask.Start();
        CountingWatch.Start();
    }
    [ExportMethod]
    public void ColorChangeMethod()
    {

        /*
        float Percent = (TimeCounter / Duration) * 100;
        DynamicColor.ToArgb().R = StartColor.R + (EndColor.R - StartColor.R) * Percent;
        */
        TimeCounter = TimeCounter + 5f;
        Log.Info(TimeCounter.ToString());

        /*Log.Info(CountingWatch.ElapsedMilliseconds.ToString());
        if (CountingWatch.ElapsedMilliseconds >= 1000)
        {
            CountingWatch.Stop();
            myPeriodicTask.Cancel();

        }*/
    }
}
