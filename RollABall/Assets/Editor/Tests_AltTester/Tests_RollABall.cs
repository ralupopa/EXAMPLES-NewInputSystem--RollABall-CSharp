using System.Threading;
using Altom.AltDriver;
using NUnit.Framework;
using UnityEngine;

public class Tests_RollABall
{
    public AltDriver altDriver;
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altDriver = new AltDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altDriver.Stop();
    }

    [Test]
    public void TestMoveBallToCollectATLeast5Boxes()
    {
        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();

        AltKeyCode[] keysWD = { AltKeyCode.W, AltKeyCode.D };
        AltKeyCode[] keysAS = { AltKeyCode.A, AltKeyCode.S };

        altDriver.PressKeys(keysWD, 1f, 1);
        Debug.Log("Ball moved UP + RIGHT");

        altDriver.PressKey(AltKeyCode.S, 3f, 2);
        Debug.Log("Ball moved DOWN");

        altDriver.PressKey(AltKeyCode.A, 1f, 1);
        Debug.Log("Ball moved LEFT");

        altDriver.PressKey(AltKeyCode.W, 1f, 2);
        Debug.Log("Ball moved UP");

        altDriver.PressKeys(keysAS, 2f, 1.5f);
        Debug.Log("Ball moved LEFT + DOWN");

        altDriver.PressKeys(keysWD, 1f, 1);
        Debug.Log("Ball moved UP + RIGHT");

        ball = altDriver.FindObject(By.NAME, "Player");
        var finalPosition = ball.GetWorldPosition();
        Assert.AreNotEqual(initialPosition.x, finalPosition.x);
        Thread.Sleep(1000);

        var countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );

        altDriver.PressKey(AltKeyCode.D, 1f, 1);
        altDriver.PressKeys(keysWD, 1f, 1);
        Debug.Log("Ball moved UP + RIGHT");

        Thread.Sleep(1000);

        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );

        Thread.Sleep(2000);

        altDriver.PressKey(AltKeyCode.A, 1f, 1.5f);
        Debug.Log("Ball moved LEFT");

        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        Assert.GreaterOrEqual(countPicked.GetText(), "Count: 5");

        altDriver.PressKeys(keysAS, 2f, 1.5f);
        Debug.Log("Ball moved LEFT + DOWN");
    }


}