using UnityEditor;
using UnityEngine;
using Vive.Plugin.SR;
using System.Collections.Generic;

[CustomEditor(typeof(ViveSR_RigidReconstructionRenderer))]
[CanEditMultipleObjects]
public class ViveSR_RigidReconstructionRendererEditor : Editor
{
    string[] displayMode = new[] { "Full Scene Point", "Field Of View", "Adaptive Mesh" };
    string[] adaptiveLable = new[] { "64cm", "32cm", "16cm", "8cm", "4cm", "2cm" };
    List<float> adaptiveLevel = new List<float>{ 64.0f, 32.0f, 16.0f, 8.0f, 4.0f, 2.0f };
    int maxSelectID, minSelectID;
    float errorThres, exportMaxSize, exportMinSize;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!Application.isPlaying) return;

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        GUILayout.Label(new GUIContent("[Runtime Command]"), style);
        EditorGUILayout.Separator();
        
        // start / stop
        GUILayout.Label(new GUIContent("--Start/Stop--"), style);
        if (!ViveSR_RigidReconstruction.IsScanning && !ViveSR_RigidReconstruction.IsExporting)
        {
            if (GUILayout.Button("Start Reconstruction"))
            {
                ViveSR_RigidReconstruction.StartScanning();
            }
        }
        if (ViveSR_RigidReconstruction.IsScanning && !ViveSR_RigidReconstruction.IsExporting)
        {
            if (GUILayout.Button("Stop Reconstruction"))
            {
                ViveSR_RigidReconstruction.StopScanning();
            }

            // live extraction mode
            EditorGUILayout.Separator();
            GUILayout.Label(new GUIContent("--Live Extraction--"), style);
            int curMode = (int)ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Display Mode:");
            curMode = EditorGUILayout.Popup(curMode, displayMode);
            GUILayout.EndHorizontal();
            if (curMode != (int)ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode)
            {
                ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode = (ReconstructionDisplayMode)curMode;
            }
            // adaptive tunning
            if (curMode == (int)ReconstructionDisplayMode.ADAPTIVE_MESH)
            {
                EditorGUILayout.Separator();
                GUILayout.Label(new GUIContent("--Live Adaptive Mesh Tuning--"), style);
                DrawAdaptiveParamUI(ViveSR_RigidReconstruction.LiveAdaptiveMaxGridSize, ViveSR_RigidReconstruction.LiveAdaptiveMinGridSize, ViveSR_RigidReconstruction.LiveAdaptiveErrorThres);
                ViveSR_RigidReconstruction.LiveAdaptiveMaxGridSize = adaptiveLevel[maxSelectID];
                ViveSR_RigidReconstruction.LiveAdaptiveMinGridSize = adaptiveLevel[minSelectID];
                ViveSR_RigidReconstruction.LiveAdaptiveErrorThres = errorThres;
            }
        }        

        // export
        EditorGUILayout.Separator();        
        if (ViveSR_RigidReconstruction.IsScanning && !ViveSR_RigidReconstruction.IsExporting)
        {
            GUILayout.Label(new GUIContent("--Export--"), style);
            bool exportAdaptive = ViveSR_RigidReconstruction.ExportAdaptiveMesh;
            ViveSR_RigidReconstruction.ExportAdaptiveMesh = GUILayout.Toggle(exportAdaptive, "Export Adaptive Model");

            if (ViveSR_RigidReconstruction.ExportAdaptiveMesh)
            {
                // live extraction mode
                EditorGUILayout.Separator();
                GUILayout.Label(new GUIContent("--Export Adaptive Mesh Tuning--"), style);
                DrawAdaptiveParamUI(ViveSR_RigidReconstruction.ExportAdaptiveMaxGridSize, ViveSR_RigidReconstruction.ExportAdaptiveMinGridSize, ViveSR_RigidReconstruction.ExportAdaptiveErrorThres);
                ViveSR_RigidReconstruction.ExportAdaptiveMaxGridSize = adaptiveLevel[maxSelectID];
                ViveSR_RigidReconstruction.ExportAdaptiveMinGridSize = adaptiveLevel[minSelectID];
                ViveSR_RigidReconstruction.ExportAdaptiveErrorThres = errorThres;
            }

            if (GUILayout.Button("Start Export Model"))
            {
                ViveSR_RigidReconstruction.ExportModel("Model");
            }
        }
    }

    private void DrawAdaptiveParamUI(float maxGridSize, float minGridSize, float thres)
    {
        GUILayout.Label("Adaptive Range (Max~Min):");
        GUILayout.BeginHorizontal();
        maxSelectID = adaptiveLevel.IndexOf(maxGridSize);
        minSelectID = adaptiveLevel.IndexOf(minGridSize);
        maxSelectID = EditorGUILayout.Popup(maxSelectID, adaptiveLable);
        minSelectID = EditorGUILayout.Popup(minSelectID, adaptiveLable);
        GUILayout.EndHorizontal();

        GUILayout.Label("Divide Threshold:");
        GUILayout.BeginHorizontal();
        errorThres = GUILayout.HorizontalSlider(thres, 0.0f, 1.5f);
        GUILayout.Label("" + errorThres.ToString("0.00"));
        GUILayout.EndHorizontal();
    }
}
