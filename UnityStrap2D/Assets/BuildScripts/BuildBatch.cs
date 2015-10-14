using UnityEditor;
using UnityEngine;

public class BuildBatch : MonoBehaviour {
	static string[] scenes = {
		"Assets/Resources/Scenes/Main.unity"
	};
	static string bundleIdentifier = "me.mattak.example"; // Replace with your package
	
	[UnityEditor.MenuItem("Tools/Build Project AllScene iOS")]
	static void BuildIos() {
		BuildOptions opt = BuildOptions.SymlinkLibraries
			| BuildOptions.AllowDebugging
			| BuildOptions.ConnectWithProfiler
			| BuildOptions.Development;
		
		EditorUserBuildSettings.symlinkLibraries = true;
		EditorUserBuildSettings.development = true;
		
		string dstDevice = "Device";
		
		PlayerSettings.iOS.sdkVersion = iOSSdkVersion.DeviceSDK;
		string errorMsg_Device = BuildPipeline.BuildPlayer(scenes, dstDevice, BuildTarget.iOS, opt);
		
		if (string.IsNullOrEmpty(errorMsg_Device)) {
			Debug.Log("====== Device build SUCCEED ======");
		} else {
			Debug.Log("====== Device build FAILURE ======");
			Debug.LogError(errorMsg_Device);
		}
	}

	[UnityEditor.MenuItem("Tools/Build Project AllScene Android")]
	static void BuildAndroid() {
		PlayerSettings.bundleIdentifier = bundleIdentifier;
		PlayerSettings.statusBarHidden = true;
		PlayerSettings.Android.keystoreName = "";
		PlayerSettings.Android.keystorePass = "";
		PlayerSettings.Android.keyaliasName = "";

		BuildPipeline.BuildPlayer (
			scenes,
			"app.apk",
			BuildTarget.Android,
			BuildOptions.None
		);

	}
}