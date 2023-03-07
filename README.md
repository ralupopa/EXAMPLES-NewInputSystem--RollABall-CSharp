# RollABall - CSharp tests

This project contains C# AltTester tests for a project using the New Input System.
The tested actions are: 
- move mouse
- click
- begin/move/end touch
- swipe
- key down/key up 
- press key
- tilt
- scroll

## Pre-requisites
1. Install the required [.NET Framework Developer Pack](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks#supported-versions-framework), version 4.7.1 [installer link](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net471-developer-pack-offline-installer)
2. Install Unity Hub
3. Install Unity Editor version 2021.3.16f1

## Before running the tests
To run the tests, you must include the AltTester Unity SDK in the project. To do that, you can choose between the following ways:
1. Add the AltTester Unity SDK submodule to the project
    - use ``git submodule update --init`` command to pull the git submodule;
    - make sure that the submodule added is on the master branch (you can use the following command ``git checkout master`` in the <i>Assets/AltTester-Unity-SDK</i> folder);
    - also, if you already have the project, you should make a ``git pull`` on the master branch, in order to ensure that you are using the latest version of AltTester.

    <br>
2. Download AltTester Unity SDK and import it into Unity 
    - download the AltTester Unity SDK from the [Altom website: Alttester](https://altom.com/testing-tools/alttester/) or using this [link for unitypackage](https://altom.com/app/uploads/AltTester/sdks/AltTester.unitypackage);
    - import the package into the project (drag-n-drop the package in the Assets folder);
    - a pop-up will appear, select All and click on Import.
    
## Run the tests

You can open the project in the Unity Editor and run the tests from AltTester Editor window.

The tests can be found in Editor->Tests folder in the Tests_AltTester class.
