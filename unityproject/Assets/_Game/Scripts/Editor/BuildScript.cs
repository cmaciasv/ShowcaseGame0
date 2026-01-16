using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildScript
{
    [MenuItem("Build/Build WebGL")]
    public static void BuildWebGL()
    {
        string buildPath = "Build/WebGL";
        
        // Disable compression to avoid "Content-Encoding: gzip" server header issues during local testing
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { EditorBuildSettings.scenes[0].path };
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.WebGL;
        buildPlayerOptions.options = BuildOptions.None;

        Debug.Log("Starting WebGL Build...");
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("<color=green>Build Succeeded!</color> Size: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.LogError("<color=red>Build Failed!</color>");
        }
    }
}
