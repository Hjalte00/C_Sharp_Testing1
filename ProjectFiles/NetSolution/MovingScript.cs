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
using System.Runtime.InteropServices;
#endregion

public class MovingScript : BaseNetLogic
{
    public FTOptix.UI.Rectangle MovingRectangle;
    public Item MovingRectangleItem;
    public static int GlobalScoreCounter = 0;
    private PeriodicTask myPeriodicTask;
    public System.Drawing.Color DynamicColor;
    public float Duration;
    public float TimeCounter = 0f;
    public Stopwatch CountingWatch;
    public float GreenCounter = 0f;
    public float RedCounter = 0f;
    public Window WindowItem;
    public int MovingWidth;
    public int MovingHeight;
    

    public override void Start()
    {
        MovingRectangle = Owner as FTOptix.UI.Rectangle;
        MovingRectangleItem = Owner as Item;
        DynamicColor = System.Drawing.Color.Green;
        MovingRectangle.FillColor = (UAValue)(UInt32)((DynamicColor.A << 24) | ((DynamicColor.R) << 16) |
            ((DynamicColor.G + 127) << 8) | (DynamicColor.B << 0));
        WindowItem = Owner.Owner.Owner as Window;
        InitializeMethod();

    }
    public override void Stop()
    {
        Log.Info("Stopped");
        Log.Info(TimeCounter.ToString());
    }

    [ExportMethod]
    public void InitializeMethod()
    {
        //Stop();
        Duration = 40f;
        MovingRectangle.Height = 51;
        MovingRectangle.Width = 51;
        MovingRectangleItem.LeftMargin = WindowItem.Width / 2 - 25; //25 because rectangle is 50 (51) wide
        MovingRectangleItem.TopMargin = WindowItem.Height / 2 - 25;
        ScoreCount.ScoreReset();
    }

        [ExportMethod]
    public void ColorChangeMethod()
    {

        if (TimeCounter >= Duration)
        {
            ResetMethod();
        }
        else
        {
            TimeCounter = TimeCounter + 1f;

                GreenCounter = (TimeCounter / Duration) * 255f;
                RedCounter = (TimeCounter / Duration) * 255f;

        }

        //Color conversion from ARGB to UINT + Change color
        MovingRectangle.FillColor = (UAValue)(UInt32)((DynamicColor.A << 24) | ((DynamicColor.R + (int)RedCounter) << 16) |
            ((DynamicColor.G+127 - (int)GreenCounter) << 8) | (DynamicColor.B << 0));
     }

    [ExportMethod]
    public void MovingMethod()
    {
        MovingRectangle.Height = MovingRectangle.Height - 1;
        MovingRectangle.Width = MovingRectangle.Width - 1;
        //Insert method to move object
        MovingWidth = ((int)(WindowItem.Width - MovingRectangle.Width));
        Random RandomLeftMarginGenerator = new Random();
        int RandomLeftMargin = RandomLeftMarginGenerator.Next(0, MovingWidth);

        MovingHeight = ((int)(WindowItem.Height - MovingRectangle.Height));
        Random RandomTopMarginGenerator = new Random();
        int RandomTopMargin = RandomTopMarginGenerator.Next(0, MovingHeight);

        //Log.Info(RandomLeftMargin.ToString() +","+ RandomTopMargin.ToString());
        MovingRectangleItem.LeftMargin = RandomLeftMargin;
        MovingRectangleItem.TopMargin = RandomTopMargin;

        ScoreCount.Score();
        ColorMethod();
    }

    [ExportMethod]
    public void ColorMethod()
    {
        myPeriodicTask = new PeriodicTask(ColorChangeMethod, 50, LogicObject);
        myPeriodicTask.Start();
    }

    public void ResetMethod()
    {

        //Reset:
        //Reset Color:
        MovingRectangle.FillColor = Colors.Blue; //(UAValue)(UInt32)((DynamicColor.A << 24) | ((DynamicColor.R) << 16) |
            //((DynamicColor.G + 127) << 8) | (DynamicColor.B << 0));
        //Stop periodic task:
        myPeriodicTask.Dispose();
        //Reset Counter
        TimeCounter = 0f;
        Log.Info("Reset Complete");
        //Call stop:
        Stop();
    }
}
