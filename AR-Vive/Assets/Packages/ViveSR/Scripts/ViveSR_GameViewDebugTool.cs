using System.Collections.Generic;
using UnityEngine;

namespace Vive.Plugin.SR
{
    public class ViveSR_GameViewDebugTool : MonoBehaviour
    {
        private bool EnableDebugTool = false;
        private int ToolbarIndex = 0;
        private string[] ToolbarLabels = new string[] { "SeeThrough", "Depth", "3D" };
        private const short AreaXStart = 10;
        private const short AreaYStart = 45;

        private void Update()
        {
            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.R)) EnableDebugTool = !EnableDebugTool;
        }

        private void OnGUI()
        {
            if (!EnableDebugTool) return;

            ToolbarIndex = GUI.Toolbar(new Rect(10, 10, 250, 30), ToolbarIndex, ToolbarLabels);
            GUILayout.Space(45);
            GUILayout.BeginArea(new Rect(AreaXStart, AreaYStart, Screen.width - AreaXStart, Screen.height - AreaYStart));
            switch (ToolbarIndex)
            {
                case 0:
                    SeeThroughGUI();
                    break;
                case 1:
                    DepthGUI();
                    break;
                case 2:
                    ReconstructionGUI();
                    break;
            }
            GUILayout.EndArea();
        }
        #region SeeThroughGUI
        private void SeeThroughGUI()
        {
            ViveSR_DualCameraImageRenderer.UpdateDistortedMaterial = GUILayout.Toggle(ViveSR_DualCameraImageRenderer.UpdateDistortedMaterial, "Update Camera Material");
            ViveSR_DualCameraImageRenderer.UpdateUndistortedMaterial = GUILayout.Toggle(ViveSR_DualCameraImageRenderer.UpdateUndistortedMaterial, "Update Undistorted Material");
            ViveSR_DualCameraImageRenderer.UpdateDepthMaterial = GUILayout.Toggle(ViveSR_DualCameraImageRenderer.UpdateDepthMaterial, "Update Depth Material");
            ViveSR_DualCameraImageRenderer.CallbackMode = GUILayout.Toggle(ViveSR_DualCameraImageRenderer.CallbackMode, "Callback Mode");
        }
        #endregion

        #region DepthGUI
        private void DepthGUI()
        {
            bool EnableDepth = GUILayout.Toggle(ViveSR_DualCameraImageCapture.DepthProcessing, "Depth Processing");
            if (ViveSR_DualCameraImageCapture.DepthProcessing != EnableDepth) ViveSR_DualCameraImageCapture.EnableDepthProcess(EnableDepth);
            ViveSR_DualCameraDepthCollider.UpdateDepthCollider = GUILayout.Toggle(ViveSR_DualCameraDepthCollider.UpdateDepthCollider, "Depth Mesh Collider");
            ViveSR_DualCameraDepthCollider.UpdateDepthColliderRange = GUILayout.Toggle(ViveSR_DualCameraDepthCollider.UpdateDepthColliderRange, "Depth Mesh Collider Range");

            GUILayout.Label("Near Distance:");
            GUILayout.BeginHorizontal();
            float NearDiatanceThres = ViveSR_DualCameraDepthCollider.UpdateColliderNearDistance = GUILayout.HorizontalSlider(ViveSR_DualCameraDepthCollider.UpdateColliderNearDistance, 0.0f, 10.0f);
            GUILayout.Label("" + NearDiatanceThres.ToString("0.00"));
            GUILayout.EndHorizontal();

            GUILayout.Label("Far Distance:");
            GUILayout.BeginHorizontal();
            float FarDiatanceThres = ViveSR_DualCameraDepthCollider.UpdateColliderFarDistance = GUILayout.HorizontalSlider(ViveSR_DualCameraDepthCollider.UpdateColliderFarDistance, 0.0f, 10.0f);
            GUILayout.Label("" + FarDiatanceThres.ToString("0.00"));
            GUILayout.EndHorizontal();
            if (EnableDepth)
            {
                ViveSR_DualCameraImageCapture.DepthRefinement = GUILayout.Toggle(ViveSR_DualCameraImageCapture.DepthRefinement, "Depth Refinement");
                ViveSR_DualCameraImageCapture.DepthEdgeEnhance = GUILayout.Toggle(ViveSR_DualCameraImageCapture.DepthEdgeEnhance, "Depth Edge Enhance");

            }
        }
        #endregion

        #region ReconstructionGUI
        //string[] displayMode = new[] { "Full Scene Point", "Field Of View", "Adaptive Mesh" };
        //string[] adaptiveLable = new[] { "64cm", "32cm", "16cm", "8cm", "4cm", "2cm" };
        List<float> adaptiveLevel = new List<float> { 64.0f, 32.0f, 16.0f, 8.0f, 4.0f, 2.0f };
        int maxSelectID, minSelectID;
        float errorThres, exportMaxSize, exportMinSize;
        private void ReconstructionGUI()
        {
            GUIStyle StyleBold = new GUIStyle { fontStyle = FontStyle.Bold };

            GUILayout.Label(new GUIContent("[Runtime Command]"), StyleBold);        // start / stop
            GUILayout.Label(new GUIContent("--Start/Stop--"), StyleBold);
            if (!ViveSR_RigidReconstruction.IsScanning && !ViveSR_RigidReconstruction.IsExporting)
            {
                if (GUILayout.Button("Start Reconstruction", GUILayout.ExpandWidth(false)))
                {
                    ViveSR_RigidReconstruction.StartScanning();
                }
            }
            if (ViveSR_RigidReconstruction.IsScanning && !ViveSR_RigidReconstruction.IsExporting)
            {
                if (GUILayout.Button("Stop Reconstruction", GUILayout.ExpandWidth(false)))
                {
                    ViveSR_RigidReconstruction.StopScanning();
                }

                GUILayout.Label(new GUIContent("--Live Extraction--"), StyleBold);
                int curMode = (int)ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode;

                if (curMode != (int)ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode)
                {
                    ViveSR_RigidReconstructionRenderer.LiveMeshDisplayMode = (ReconstructionDisplayMode)curMode;
                }
                // adaptive tunning
                if (curMode == (int)ReconstructionDisplayMode.ADAPTIVE_MESH)
                {
                    GUILayout.Label(new GUIContent("--Live Adaptive Mesh Tuning--"), StyleBold);
                    DrawAdaptiveParamUI(ViveSR_RigidReconstruction.LiveAdaptiveMaxGridSize, ViveSR_RigidReconstruction.LiveAdaptiveMinGridSize, ViveSR_RigidReconstruction.LiveAdaptiveErrorThres);
                    ViveSR_RigidReconstruction.LiveAdaptiveMaxGridSize = adaptiveLevel[maxSelectID];
                    ViveSR_RigidReconstruction.LiveAdaptiveMinGridSize = adaptiveLevel[minSelectID];
                    ViveSR_RigidReconstruction.LiveAdaptiveErrorThres = errorThres;
                }
            }

            // export
            if (ViveSR_RigidReconstruction.IsScanning && !ViveSR_RigidReconstruction.IsExporting)
            {
                GUILayout.Label(new GUIContent("--Export--"), StyleBold);
                bool exportAdaptive = ViveSR_RigidReconstruction.ExportAdaptiveMesh;
                ViveSR_RigidReconstruction.ExportAdaptiveMesh = GUILayout.Toggle(exportAdaptive, "Export Adaptive Model");

                if (ViveSR_RigidReconstruction.ExportAdaptiveMesh)
                {
                    // live extraction mode
                    GUILayout.Label(new GUIContent("--Export Adaptive Mesh Tuning--"), StyleBold);
                    DrawAdaptiveParamUI(ViveSR_RigidReconstruction.ExportAdaptiveMaxGridSize, ViveSR_RigidReconstruction.ExportAdaptiveMinGridSize, ViveSR_RigidReconstruction.ExportAdaptiveErrorThres);
                    ViveSR_RigidReconstruction.ExportAdaptiveMaxGridSize = adaptiveLevel[maxSelectID];
                    ViveSR_RigidReconstruction.ExportAdaptiveMinGridSize = adaptiveLevel[minSelectID];
                    ViveSR_RigidReconstruction.ExportAdaptiveErrorThres = errorThres;
                }

                if (GUILayout.Button("Start Export Model", GUILayout.ExpandWidth(false)))
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
            GUILayout.EndHorizontal();

            GUILayout.Label("Divide Threshold:");
            GUILayout.BeginHorizontal();
            errorThres = GUILayout.HorizontalSlider(thres, 0.0f, 1.5f);
            GUILayout.Label("" + errorThres.ToString("0.00"));
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}