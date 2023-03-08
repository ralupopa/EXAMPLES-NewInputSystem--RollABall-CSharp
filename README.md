This project does not contain a build. In order to run tests need to instrument and build application.

## Pre-requisites
1. Install the required [.NET Framework Developer Pack](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks#supported-versions-framework), version 4.7.1 [installer link](https://dotnet.microsoft.com/en-us/download/dotnet-framework/thank-you/net471-developer-pack-offline-installer)
2. Install Unity Hub
3. Install **Unity Editor version 2021.3.20f1**
4. Import AltTester Unity SDK in Unity Editor
  * Assets > Import Package > Custom Package > use the SDK version suggested below in *Before running the tests*
5. Edit in `RollABall\Packages\manifest.json` version compatible for package `com.unity.render-pipelines.universal: 12.1.10`

### Issue encountered when importing project:
- Package `com.unity.render-pipelines.universal: 12.1.7` can not be found using Unity Editor 2021.3.19f1

This is why detailed in pre-requisites other versions which seemed compatible.

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

1. Open the project in the Unity Editor (see pre-requisites for which version proved to work).
2. Platform > Standalone > Build Target: StandaloneWindows
  * Build Only
3. Open AltTester Editor from Unity Editor menu (displayed only after imported package as mentioned above) and see tests under `Assembly-CSharp-Editor.dll`
4. If build completed successfully, should see in folder RollABall *build* folder.
5. Launch Game from executable under *build*
6. Select tests under **Tests_AltTester**
  * Run Selected Tests

The tests can be found in Editor->Tests folder in the Tests_AltTester class.

## RollABall - CSharp tests

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