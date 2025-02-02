﻿using System.Threading;
using Altom.AltDriver;
using NUnit.Framework;
using UnityEngine;

public class Tests_AltTester
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
    public void TestMoveBallWithMoveMouse()
    {
        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();
        altDriver.MoveMouse(new AltVector2(0, 0), 3f);
        altDriver.MoveMouse(new AltVector2(100, 100), 3f);
        altDriver.MoveMouse(new AltVector2(-200, -200), 3f);
        ball = altDriver.FindObject(By.NAME, "Player");
        var finalPosition = ball.GetWorldPosition();
        Assert.AreNotEqual(initialPosition.x, finalPosition.x);
    }

    [Test]
    public void TestScrollOnScrollbar()
    {
        altDriver.LoadScene("MiniGame");
        var scrollbar = altDriver.FindObject(By.NAME, "Scrollbar Vertical");
        var scrollbarPosition = scrollbar.GetComponentProperty<float>("UnityEngine.UI.Scrollbar", "value", "UnityEngine.UI");
        altDriver.MoveMouse(altDriver.FindObject(By.NAME, "Scroll View").GetScreenPosition(), 1);
        altDriver.Scroll(new AltVector2(-3000, -3000), 1, true);
        var scrollbarPositionFinal = scrollbar.GetComponentProperty<float>("UnityEngine.UI.Scrollbar", "value", "UnityEngine.UI");
        Assert.AreNotEqual(scrollbarPosition, scrollbarPositionFinal);
    }

    [Test]
    public void TestMoveMouseOnScrollbar()
    {
        altDriver.LoadScene("MiniGame");
        var objects = altDriver.GetAllElementsLight();
        var scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        AltVector2 scrollbarInitialPosition = scrollbar.GetScreenPosition(); // use screen coordinates instead of world coordinates        
        altDriver.MoveMouse(scrollbar.GetScreenPosition()); // move mouse in area where scroll reacts
        altDriver.Scroll(-200, 0.1f);
        scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        AltVector2 scrollbarFinalPosition = scrollbar.GetScreenPosition();
        Assert.AreNotEqual(scrollbarInitialPosition.y, scrollbarFinalPosition.y);//compare y as there is no equality comparer on AltVector2. and we expect only y to change
    }

    [Test]
    public void TestSwipeOnScrollbar()
    {
        altDriver.LoadScene("MiniGame");
        var scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        AltVector2 scrollbarInitialPosition = new AltVector2(scrollbar.worldX, scrollbar.worldY);
        altDriver.Swipe(new AltVector2(scrollbar.x, scrollbar.y), new AltVector2(scrollbar.x, scrollbar.y - 200), 3);
        scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        AltVector2 scrollbarFinalPosition = new AltVector2(scrollbar.worldX, scrollbar.worldY);
        Assert.AreNotEqual(scrollbarInitialPosition.y,scrollbarFinalPosition.y);
    }

    [Test]
    public void TestClickNearScrollBarMovesScrollBar()
    {
        altDriver.LoadScene("MiniGame");

        var scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        var scrollbarInitialPosition = scrollbar.GetScreenPosition();
        
        var scrollBarMoved = new AltVector2(scrollbar.x, scrollbar.y - 100);
        altDriver.MoveMouse(scrollBarMoved, 1);

        altDriver.Click(new AltVector2(scrollbar.x, scrollbar.y - 100));

        scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        var scrollbarFinalPosition = scrollbar.GetScreenPosition();

        Assert.AreNotEqual(scrollbarInitialPosition.y, scrollbarFinalPosition.y);
    }

    [Test]
    public void TestBeginMoveEndTouchMovesScrollbar()
    {
        altDriver.LoadScene("MiniGame");
        var scrollbar = altDriver.FindObject(By.NAME, "Handle");
        var scrollbarPosition = scrollbar.GetScreenPosition();
        int fingerId = altDriver.BeginTouch(scrollbar.GetScreenPosition());
        altDriver.MoveTouch(fingerId, scrollbarPosition);
        AltVector2 newPosition = new AltVector2(scrollbar.x, scrollbar.y - 500);
        altDriver.MoveTouch(fingerId, newPosition);
        altDriver.EndTouch(fingerId);
        scrollbar = altDriver.FindObject(By.NAME, "Handle");
        var scrollbarPositionFinal = scrollbar.GetScreenPosition();

        Assert.AreNotEqual(scrollbarPosition.y, scrollbarPositionFinal.y);
    }

    [Test]
    public void TestPressKeyNearScrollBarMovesScrollBar()
    {
        altDriver.LoadScene("MiniGame");

        var scrollbar = altDriver.FindObject(By.NAME, "Handle");
        var scrollbarPosition = scrollbar.GetScreenPosition();
        var scrollBarMoved = new AltVector2(scrollbar.x, scrollbar.y - 100);
        altDriver.MoveMouse(scrollBarMoved, 1);
        altDriver.PressKey(AltKeyCode.Mouse0, 0.1f);
        scrollbar = altDriver.FindObject(By.NAME, "Handle");
        var scrollbarPositionFinal = scrollbar.GetScreenPosition();
        Assert.AreNotEqual(scrollbarPosition.y, scrollbarPositionFinal.y);
    }

    [Test]
    public void TestKeyDownAndKeyUpMouse0MovesScrollBar()
    {
        altDriver.LoadScene("MiniGame");

        var scrollbar = altDriver.FindObject(By.NAME, "Scrollbar Vertical");
        var handle = altDriver.FindObject(By.NAME, "Handle");
        var scrollbarPosition = scrollbar.GetComponentProperty<float>("UnityEngine.UI.Scrollbar", "value", "UnityEngine.UI");
        var scrollBarMoved = new AltVector2(handle.x, handle.y - 100);
        altDriver.MoveMouse(scrollBarMoved, 1);
        altDriver.KeyDown(AltKeyCode.Mouse0);
        altDriver.KeyUp(AltKeyCode.Mouse0);
        scrollbar = altDriver.FindObject(By.NAME, "Scrollbar Vertical");
        var scrollbarPositionFinal = scrollbar.GetComponentProperty<float>("UnityEngine.UI.Scrollbar", "value", "UnityEngine.UI");
        Assert.AreNotEqual(scrollbarPosition, scrollbarPositionFinal);
    }

    [Test]
    public void TestBallMovesOnPressKey()
    {
        altDriver.LoadScene("MiniGame");

        var ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.PressKey(AltKeyCode.S, 1f, 1f);
        var newBall = altDriver.FindObject(By.NAME, "Player");
        Debug.Log("Ball moved backward");
        Assert.AreNotEqual(ball.GetWorldPosition().z, newBall.GetWorldPosition().z);
        Thread.Sleep(1000);

        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.PressKey(AltKeyCode.W, 1f, 2f);
        newBall = altDriver.FindObject(By.NAME, "Player");
        Debug.Log("Ball moved forward");
        Assert.AreNotEqual(ball.GetWorldPosition().z, newBall.GetWorldPosition().z);
        Thread.Sleep(1000);

        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.PressKey(AltKeyCode.A, 1f, 2f);
        newBall = altDriver.FindObject(By.NAME, "Player");
        Debug.Log("Ball moved to the left");
        Assert.AreNotEqual(ball.GetWorldPosition().x, newBall.GetWorldPosition().x);
        Thread.Sleep(1000);

        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.PressKey(AltKeyCode.D, 1f, 2f);
        newBall = altDriver.FindObject(By.NAME, "Player");
        Debug.Log("Ball moved to the right");
        Assert.AreNotEqual(ball.GetWorldPosition().x, newBall.GetWorldPosition().x);
        Thread.Sleep(2000);
    }

    [Test]
    public void TestTiltBall()
    {
        altDriver.LoadScene("MiniGame");
        var ball = altDriver.FindObject(By.NAME, "Player");
        var initialPosition = ball.GetWorldPosition();
        altDriver.Tilt(new AltVector3(1000, 1000, 1), 3f);
        Assert.AreNotEqual(initialPosition.x, altDriver.FindObject(By.NAME, "Player").GetWorldPosition().x);
    }

    [Test]
    public void TestDoubleClick()
    {
        altDriver.LoadScene("MiniGame");
        var button = altDriver.FindObject(By.NAME, "SpecialButton").Click();
        Thread.Sleep(1000);
        button.Click();
        var text = altDriver.FindObject(By.PATH,"//ScrollCanvas/SpecialButton/Text (TMP)").GetText();
        Assert.AreEqual("2",text);
    }

    [Test]
    public void TestHoldButton()
    {
        altDriver.LoadScene("MiniGame");
        var button = altDriver.FindObject(By.NAME, "SpecialButton");

        altDriver.HoldButton(button.GetScreenPosition(), 1);
        Thread.Sleep(1000);
        var text = altDriver.FindObject(By.PATH,"//ScrollCanvas/SpecialButton/Text (TMP)").GetText();
        Assert.AreEqual("1",text);

        altDriver.HoldButton(button.GetScreenPosition(), 1);
        text = altDriver.FindObject(By.PATH,"//ScrollCanvas/SpecialButton/Text (TMP)").GetText();
        Assert.AreEqual("2",text);
    }

    [Test]
    public void TestBallMovesFromCenterToRightTopCornerThenToLeftBottomCornerOnPressKeys()
    {
        altDriver.LoadScene("MiniGame");
        AltKeyCode[] keysWD = { AltKeyCode.W, AltKeyCode.D };
        AltKeyCode[] keysAS = { AltKeyCode.A, AltKeyCode.S };

        var ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.PressKeys(keysWD, 1f, 2f);
        Debug.Log("Ball moved to Right Top corner");
        var newBall = altDriver.FindObject(By.NAME, "Player");
        Assert.AreNotEqual(ball.GetWorldPosition().z, newBall.GetWorldPosition().z);
        Thread.Sleep(1000);

        ball = altDriver.FindObject(By.NAME, "Player");
        altDriver.PressKeys(keysAS, 1f, 2f);
        Debug.Log("Ball moved to Left Bottom corner");
        newBall = altDriver.FindObject(By.NAME, "Player");
        Assert.AreNotEqual(ball.GetWorldPosition().z, newBall.GetWorldPosition().z);
    }

    [Test]
    public void TestMultipointSwipeScrollbar()
    {
        altDriver.LoadScene("MiniGame");
        var scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        AltVector2 scrollbarInitialPosition = new AltVector2(scrollbar.worldX, scrollbar.worldY);

        var pos = new[]
        {
            scrollbar.GetScreenPosition(),
            new AltVector2(scrollbar.x, scrollbar.y - 100),
            new AltVector2(scrollbar.x, scrollbar.y - 200),
        };

        altDriver.MultipointSwipe(pos, 3);
        
        scrollbar = altDriver.WaitForObject(By.NAME, "Handle");
        AltVector2 scrollbarFinalPosition = new AltVector2(scrollbar.worldX, scrollbar.worldY);
        Assert.AreNotEqual(scrollbarInitialPosition.y, scrollbarFinalPosition.y);
    }

    [Test]
    public void TestTapOnSpecialButton()
    {
        altDriver.LoadScene("MiniGame");

        var button = altDriver.FindObject(By.NAME, "SpecialButton");
        altDriver.Tap(button.GetScreenPosition(), 1);
        Thread.Sleep(1000);
        var text = altDriver.FindObject(By.PATH,"//ScrollCanvas/SpecialButton/Text (TMP)").GetText();
        Assert.AreEqual("1",text);

        altDriver.Tap(button.GetScreenPosition(), 2);
        text = altDriver.FindObject(By.PATH,"//ScrollCanvas/SpecialButton/Text (TMP)").GetText();
        Assert.AreEqual("3",text);
    }

    [Test]
    public void TestResetInput()
    {
        altDriver.LoadScene("MiniGame");

        altDriver.KeyDown(AltKeyCode.P, 1);
        Assert.True(altDriver.FindObject(By.NAME, "AltTesterPrefab").GetComponentProperty<bool>("Altom.AltTester.NewInputSystem", "Keyboard.pKey.isPressed", "Assembly-CSharp"));
        altDriver.ResetInput();
        Assert.False(altDriver.FindObject(By.NAME, "AltTesterPrefab").GetComponentProperty<bool>("Altom.AltTester.NewInputSystem", "Keyboard.pKey.isPressed", "Assembly-CSharp"));
    }
}