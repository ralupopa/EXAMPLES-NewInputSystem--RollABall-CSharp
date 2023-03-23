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
    public void TestMoveBallUsingPressKeysToCollectAtLeast5Boxes()
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

    [Test]
    public void TestMoveBallUsingMoveMouseToCollectAtLeast5Boxes()
    
    {
        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();

        altDriver.MoveMouse(new AltVector2(120, 0), 1.5f);
        altDriver.MoveMouse(new AltVector2(0, 60), 1f);

        altDriver.MoveMouse(new AltVector2(-30, 40), 1.5f);

        var countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        Thread.Sleep(2000);

        altDriver.MoveMouse(new AltVector2(-30, -45), 3f);
        Thread.Sleep(2000);

        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );

        altDriver.MoveMouse(new AltVector2(-50, 40), 3f);
        Thread.Sleep(2000);
        altDriver.MoveMouse(new AltVector2(40, 20), 3f);
        Thread.Sleep(2000);
        altDriver.MoveMouse(new AltVector2(100, 100), 3f);
        Thread.Sleep(2000);
        altDriver.MoveMouse(new AltVector2(50, 0), 3f);
        Thread.Sleep(2000);
        Assert.GreaterOrEqual(countPicked.GetText(), "Count: 5");

        ball = altDriver.FindObject(By.NAME, "Player");
        var finalPosition = ball.GetWorldPosition();
        Assert.AreNotEqual(initialPosition.x, finalPosition.x);
    }

    [Test]
    public void TestMoveBallMoveMouseAndMathToCollectAtLeast5Boxes()
    {
        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();

        altDriver.MoveMouse(new AltVector2(-120, 0), 1.3f);

        ball = altDriver.FindObject(By.NAME, "Player");
        var leftPosition = ball.GetWorldPosition();
        
        Thread.Sleep(2000);
        var x = leftPosition.x;
        var y = leftPosition.y;

        var radius = 7;
        Debug.Log("Radius is "+ radius );
        var circumference = 2 * Mathf.PI * radius;
        Debug.Log("Circumference is "+ circumference );

        altDriver.MoveMouse(new AltVector2(50, Mathf.Sin(x)), 1.5f);
        Thread.Sleep(2000);
        var countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        Assert.AreEqual(countPicked.GetText(), "Count: 2");

        altDriver.MoveMouse(new AltVector2(2 * radius, circumference), 1.5f, true);
        Thread.Sleep(2000);
        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Assert.GreaterOrEqual(countPicked.GetText(), "Count: 4");

        Thread.Sleep(2000);
        altDriver.MoveMouse(new AltVector2(-2 * radius, -circumference), 1.5f);
        Thread.Sleep(2000);
        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Assert.GreaterOrEqual(countPicked.GetText(), "Count: 5");

        ball = altDriver.FindObject(By.NAME, "Player");
        var finalPosition = ball.GetWorldPosition();
        Assert.AreNotEqual(initialPosition.x, finalPosition.x);
    }

    [Test]
    public void TestMoveBallSwipeToCollectWIP()
    {
        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();

        var pickupZero = altDriver.FindObject(By.NAME, "PickUp");
        altDriver.Swipe(new AltVector2(ball.x, ball.y), new AltVector2(pickupZero.x, pickupZero.y), 5f);

        var countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        Assert.AreEqual(countPicked.GetText(), "Count: 1");

        var pickupOne = altDriver.FindObject(By.NAME, "PickUp (1)");
        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.Swipe(new AltVector2(ball.x, ball.y), new AltVector2(pickupOne.x, pickupOne.y), 1);

        Thread.Sleep(2000);
        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        Assert.AreEqual(countPicked.GetText(), "Count: 3");

        var pickupTwo = altDriver.FindObject(By.NAME, "PickUp (7)");
        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.Swipe(new AltVector2(ball.x, ball.y), new AltVector2(pickupTwo.x, pickupTwo.y), 5f);

        Thread.Sleep(2000);
        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        Assert.AreEqual(countPicked.GetText(), "Count: 5");

        //Thread.Sleep(2000);
        var pickupThree = altDriver.FindObject(By.NAME, "PickUp (4)");
        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.Swipe(new AltVector2(ball.x, ball.y), new AltVector2(pickupThree.x, pickupThree.y), 1);

        countPicked = altDriver.FindObject(By.NAME, "CountText");
        Debug.Log("Collected "+ countPicked.GetText() );
        //Assert.AreEqual(countPicked.GetText(), "Count: 5");

        ball = altDriver.FindObject(By.NAME, "Player");
        var finalPosition = ball.GetWorldPosition();
        Assert.AreNotEqual(initialPosition.x, finalPosition.x);
    }

    [Test]
    public void TestMoveBallMathToCollect()
    {

        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();

        var pickupZero = altDriver.FindObject(By.NAME, "PickUp");
        //var radius = pickupZero.x - ball.x;
        var radius = pickupZero.GetWorldPosition().x - ball.GetWorldPosition().x;
        Debug.Log("Radius is "+ radius );

        ball = altDriver.FindObject(By.NAME, "Player");

        for (int angleDegrees = 0; angleDegrees <=360; angleDegrees += 30)
        {
            var angleRadians = (angleDegrees / 180) * Mathf.PI;
            // var X = ball.GetWorldPosition().x + radius * (float)(Mathf.Cos(angleRadians));
            // var Y = ball.GetWorldPosition().y + radius * (float)(Mathf.Sin(angleRadians));

            // var X = ball.GetWorldPosition().x + radius * (float)Mathf.Cos((Mathf.PI / 180)* angleDegrees);
            // var Y = ball.GetWorldPosition().y + radius * (float)Mathf.Sin((Mathf.PI / 180)* angleDegrees);

            var X = ball.GetWorldPosition().x + 2 * radius * (float)Mathf.Cos(angleDegrees);
            var Y = ball.GetWorldPosition().y + 2 * radius * (float)Mathf.Sin(angleDegrees);
            altDriver.MoveMouse(new AltVector2(X, Y), 2f, true);
            
            Thread.Sleep(100);
            Debug.Log("angleRadians degree is " + angleRadians + " when angleeDegrees is " + angleDegrees);
            //ball = altDriver.FindObject(By.NAME, "Player");
        }

        ball = altDriver.FindObject(By.NAME, "Player");
        var finalPosition = ball.GetWorldPosition();
        Assert.AreNotEqual(initialPosition.x, finalPosition.x);
    }
}