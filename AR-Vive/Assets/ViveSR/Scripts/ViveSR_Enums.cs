﻿using System.Runtime.InteropServices;

namespace Vive.Plugin.SR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DataInfo
    {
        public System.IntPtr ptr;
        public int mask;
        public System.IntPtr description;
    };

    [StructLayout(LayoutKind.Sequential)]
    struct CameraParams
    {
        public double Cx_L;
        public double Cx_R;
        public double Cy_L;
        public double Cy_R;
        public double FocalLength_L;
        public double FocalLength_R;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
        public double[] Rotation;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public double[] Translation;
    };
}

namespace Vive.Plugin.SR
{
    enum ModuleStatus
    {
        SR_DISABLE = 0,
        SR_ENABLE = 1,
        SR_UPDATE = 2,
        SR_NOT_UPDATE = 3,
    };
    enum ModuleType
    {
        ENGINE_SEETHROUGH = 1,          // support device : VIVE Pro
        DEVICE_VIVE_HMD_DUALCAM = 2,    // obsolete
        ENGINE_UNDISTORTION = 3,        // obsolete
        ENGINE_DEPTH = 4,
        ENGINE_RIGID_RECONSTRUCTION = 5,
        ENGINE_CHAPERONE = 6,
    };

    enum WorkLinkMethod
    {
        NONE = -1,
        PASSIVE = 0,
        ACTIVE = 1,
    };

    public enum FrameworkStatus
    {
        WORKING,
        STOP,
        ERROR
    }

    public enum DualCameraMode
    {
        REAL,
        MIX
    }

    public enum DualCameraIndex
    {
        LEFT,
        RIGHT
    }

    public enum CalibrationType
    {
        RELATIVE,
        ABSOLUTE
    }

    public enum CalibrationAxis
    {
        X, Y, Z
    }

    public enum DualCameraDisplayMode
    {
        VIRTUAL,
        REAL,
        MIX
    }

    public enum DualCameraStatus
    {
        NOT_FOUND,
        IDLE,
        WORKING,
        ERROR
    }

    public enum UndistortionMethod
    {
        DEFISH_BY_MESH,
        DEFISH_BY_SRMODULE,
    }
}

// Native
namespace Vive.Plugin.SR
{
    enum Error
    {
        FAILED = -1,
        WORK = 0,
        INVAILD_INPUT = 1,
        FILE_NOT_FOUND = 2,
        DATA_NOT_FOUND = 13,
        INITIAL_FAILED = 1001,
        NOT_IMPLEMENTED = 1003,
        NULL_POINTER = 1004,
        OVER_MAX_LENGTH = 1005,
        FILE_INVALID = 1006,
        UNINSTALL_STEAM = 1007,
        MEMCPY_FAIL = 1008,
        NOT_MATCH = 1009,
        NODE_NOT_EXIST = 1010,
        UNKONW_MODULE = 1011,
        MODULE_FULL = 1012,
        UNKNOW_TYPE = 1013,
        INVALID_MODULE = 1014,
        INVALID_TYPE = 1015,
        MEMORY_NOT_ENOUGH = 1016,
        BUZY = 1017,
        NOT_SUPPORT = 1018,
    };

    enum LogLevel
    {
        _0 = 3,              // turn-off any log except error & warning
        _1 = 4,              // the lower level, the less log, default value
        _2 = 5,
        _3 = 6,
        _MAX = 10,           // turn-on all log msg
    };

    enum Module_Param
    {
        ENABLE_FPSCONTROL = 1001,
        SET_FPS = 1002,
    };

    enum CameraParam
    {
        CX_L,
        CX_R,
        CY_L,
        CY_R,
        FOCAL_LENGTH_L,
        FOCAL_LENGTH_R,
        R_M0,
        R_M1,
        R_M2,
        R_M3,
        R_M4,
        R_M5,
        R_M6,
        R_M7,
        R_M8,
        T_M0,
        T_M1,
        T_M2
    };

    #region SeeThrough
    enum SeeThroughParam
    {
        VR_INIT = 0,
        VR_INIT_TYPE,
        OUTPUT_DISTORTED_WIDTH,
        OUTPUT_DISTORTED_HEIGHT,
        OUTPUT_DISTORTED_CHANNEL,
        OUTPUT_UNDISTORTED_WIDTH,
        OUTPUT_UNDISTORTED_HEIGHT,
        OUTPUT_UNDISTORTED_CHANNEL,
        OUTPUT_FPS,
        OFFSET_HEAD_TO_CAMERA,          // float[6]  (x0,y0,z0,x1,y1,z1)
        PLAY_AREA_RECT,                 // float[12] (x0,y0,z0,x1,y1,z1,...)
        VIDEO_RES_NATIVE_PTR,
        VIDEO_RES_VIEW_NATIVE_PTR,
        IMAGE_NATIVE_TEXTURE_PTR_L,
        IMAGE_NATIVE_TEXTURE_PTR_R,
        CAMERA_BRIGHTNESS = 100,
        CAMERA_CONTRAST,
        CAMERA_HUE,
        CAMERA_SATURATION,
        CAMERA_SHARPNESS,
        CAMERA_GAMMA,
        CAMERA_COLOR_ENABLE,
        CAMERA_WHITE_BALANCE,
        CAMERA_BACKLIGHT_COMPENSATION,
        CAMERA_GAIN,
        CAMERA_PAN,
        CAMERA_TILT,
        CAMERA_ROLL,
        CAMERA_ZOOM,
        CAMERA_EXPOSURE,
        CAMERA_IRIS,
        CAMERA_FOCUS,
        UNDISTORTION_MODE = 200,
        UNDISTORTION_CX,
        UNDISTORTION_CY,
        UNDISTORTION_FOCAL_LENGTH,
        UNDISTORTION_FMAT_RM_L,
        UNDISTORTION_FMAT_RM_R,
        UNDISTORTION_INTRINSIC_L,
        UNDISTORTION_INTRINSIC_R,
        UNDISTORTION_R_RECTIFY_L,
        UNDISTORTION_R_RECTIFY_R,
        UNDISTORTION_COEFFS_L,
        UNDISTORTION_COEFFS_R,
        UNDISTORTION_P_NEWPROJ_L,
        UNDISTORTION_P_NEWPROJ_R,
        UNDISTORTION_MAP_SIZE,
        UNDISTORTION_MAP_L,
        UNDISTORTION_MAP_R,
        UNDISTORTION_CENTER,
    };

    enum SeeThroughDataMask
    {
        DISTORTED_FRAME_LEFT = 0,       // sizeof(char) * 612 * 460 * 4
        DISTORTED_FRAME_RIGHT = 1,      // sizeof(char) * 612 * 460 * 4
        UNDISTORTED_FRAME_LEFT = 2,     // sizeof(char) * 1150 * 750 * 4
        UNDISTORTED_FRAME_RIGHT = 3,        // sizeof(char) * 1150 * 750 * 4
        FRAME_SEQ = 4,      // sizeof(unsigned int)
        TIME_STP = 5,       // sizeof(unsigned int)
        POSE_LEFT = 6,      // sizeof(float) * 16
        POSE_RIGHT = 7,     // sizeof(float) * 16
        LEFT_AEINDEX = 8,       // sizeof(int)
        RIGHT_AEINDEX = 9,		// sizeof(int)
    };

    enum SeeThroughCallback
    {
        BASIC = 1001,
    };
    #endregion
/*
    #region Distorted
    enum DistortedParam
    {
        VR_INIT,
        VR_INIT_TYPE,
        OUTPUT_WIDTH,
        OUTPUT_HEIGHT,
        OUTPUT_CHAANEL,
        OPTPUT_IMAGE_ORIGIN,    // 0 top-left, 1 top-right, 2 bottom-left, 3 bottom-right
        OUTPUT_FPS,
        OFFSET_HEAD_TO_CAMERA,  // float[6]  (x0,y0,z0,x1,y1,z1)
        PLAY_AREA_RECT,		    // float[12] (x0,y0,z0,x1,y1,z1,...)
        VideoResNativePtr,
        VideoResViewNativePtr,
    };

    enum DistortedDataMask
    {
        LEFT_FRAME  = 0,    // sizeof(char) * 612 * 460 * 4
        RIGHT_FRAME = 1,    // sizeof(char) * 612 * 460 * 4
        FRAME_SEQ   = 2,    // sizeof(unsigned int)
        TIME_STP    = 3,    // sizeof(unsigned int)
        LEFT_POSE   = 4,    // sizeof(float) * 16
        RIGHT_POSE  = 5,	// sizeof(float) * 16
    };

    enum DistortedCallback
    {
        BASIC = 1001,
    };
    #endregion

    #region Undistorted
    enum UndistortedParam
    {
        OUTPUT_WIDTH,
        OUTPUT_HEIGHT,
        OUTPUT_CHAANEL,
        CX = 3,
		CY = 4,
		MODE = 5,
		FOCULENS = 6,
		FMAT_RM_L = 7,
		FMAT_RM_R = 8,
		INTRINSIC_L = 9,
		INTRINSIC_R = 10,
		R_RECTIFY_L = 13,
		R_RECTIFY_R = 14,
		COEFFS_L = 15,
		COEFFS_R = 16,
		P_NEWPROJ_L = 17,
		P_NEWPROJ_R = 18,
		MAP_UndistortionSize,
		MAP_Undistortion_L,
		MAP_Undistortion_R,
        UndistortionCenter,
        IMAGE_NativeTexPtr_L,
        IMAGE_NativeTexPtr_R,
    };

    enum UndistortedDataMask
    {
        LEFT_FRAME  = 0,    // sizeof(char) * 1150 * 750 * 4
        RIGHT_FRAME = 1,    // sizeof(char) * 1150 * 750 * 4
        FRAME_SEQ   = 2,    // sizeof(unsigned int)
        TIME_STP    = 3,    // sizeof(unsigned int)
        LEFT_POSE   = 4,    // sizeof(float) * 16
        RIGHT_POSE  = 5,	// sizeof(float) * 16
    };

    enum UndistortedCallback
    {
        BASIC = 1001,
    };
    #endregion
*/
    #region Depth
    enum DepthParam
    {
        OUTPUT_WIDTH,
        OUTPUT_HEIGHT,
        OUTPUT_CHAANEL_0,
        OUTPUT_CHAANEL_1,
        TYPE,
        FOCULENS,
        BASELINE,
        COLLIDER_QUALITY,
        MESH_NEAR_DISTANCE,
        MESH_FAR_DISTANCE,
        DENOISE_MEDIAN_FILTER,  // range : 1, 3, 5; (default: 3)
        CONFIDENCE_THRESHOLD,   // range : 0 ~ 5; (default: 0.05)
        DENOISE_GUIDED_FILTER,	// range : 1 ~ 7; (default: 3)
        DEPTH_USING_CASE,
        KEEP_ONFLY_RESULT_AT_END,
    };
    public enum DepthCase
    {
        DEFAULT,
        CLOSE_RANGE,
    };
    enum DepthDataMask
    {
        LEFT_FRAME   = 0,   // sizeof(char) * 640 * 480 * 4
        DEPTH_MAP    = 1,   // sizeof(float) * 640 * 480 * 1
        FRAME_SEQ    = 2,   // sizeof(unsigned int)
        TIME_STP     = 3,   // sizeof(unsigned int)
        POSE         = 4,   // sizeof(float) * 16
        NUM_VERTICES = 5,   // sizeof(unsigned int)
        BYTEPERVERT  = 6,   // sizeof(unsigned int)
        VERTICES     = 7,   // sizeof(float) * 640 * 480 * 3
        NUM_INDICES  = 8,   // sizeof(unsigned int)
        INDICES      = 9,   // sizeof(int) * 640 * 480 * 6
    };
    enum DepthCmd
    {
        EXTRACT_DEPTH_MESH = 0,
        ENABLE_SELECT_MESH_DISTANCE_RANGE,
        ENABLE_REFINEMENT,
        ENABLE_EDGE_ENHANCE,
        CHANGE_DEPTH_CASE,
        ENABLE_ONFLY,
    }
    enum DepthCallback
    {
        BASIC = 1001,
    };
    #endregion

    #region Reconstruction
    enum ReconstructionParam
    {
        VOXEL_SIZE = 0,
        COLOR_SIZE = 1,
        DATA_SOURCE = 2,
        DATASET_PATH = 3,
        RGB_IMAGE_EXT = 4,
        DATASET_FRAME = 5,
        MAX_DEPTH = 6,
        MIN_DEPTH = 7,
        POINTCLOUD_POINTSIZE = 9,
        EXPORT_ADAPTIVE_MODEL = 10,
        ADAPTIVE_MAX_GRID = 11,
        ADAPTIVE_MIN_GRID = 12,
        ADAPTIVE_ERROR_THRES = 13,

        SECTOR_SIZE = 15,           // float
        SECTOR_NUM_PER_SIDE = 16,	// int

        ENABLE_FRUSTUM_CULLING = 20,

        CONFIG_FILEPATH = 21,
        //CONFIG_DATA_SOURCE,
        //CONFIG_DATASET_FRAME_NUM,
        //CONFIG_DATASET_PATH,
        CONFIG_QUALITY,
        CONFIG_EXPORT_COLLIDER,
        CONFIG_EXPORT_TEXTURE,

        DATA_CURRENT_POS = 31,
        LITE_POINT_CLOUD_MODE,
        FULL_POINT_CLOUD_MODE,
        LIVE_ADAPTIVE_MODE,
        MESH_REFRESH_INTERVAL = 37,
        ENABLE_SECTOR_GROUPER = 38,

        VERTEX_BUFFER_NATIVE_PTR = 99,
        INDEX_BUFFER_NATIVE_PTR = 100,
    };

    enum ReconstructionDataMask
    {
        FRAME_SEQ       = 0,    // sizeof(unsigned int)
        POSEMTX44       = 1,    // sizeof(float) * 16
        NUM_VERTICES    = 2,    // sizeof(int)
        BYTEPERVERT     = 3,    // sizeof(int)
        VERTICES        = 4,    // sizeof(float) * 8 * 2500000
        NUM_INDICES     = 5,    // sizeof(int)
        INDICES         = 6,    // sizeof(int) * 2500000
        CLDTYPE         = 7,    // sizeof(int)
        COLLIDERNUM     = 8,    // sizeof(unsigned int)
        CLD_NUM_VERTS   = 9,    // sizeof(unsigned int) * 200
        CLD_NUMIDX      = 10,   // sizeof(unsigned int) * 200
        CLD_VERTICES    = 11,   // sizeof(float) * 3 * 50000
        CLD_INDICES     = 12,   // sizeof(int) * 100000
        SECTOR_NUM      = 13,   // sizeof(int)
        SECTOR_ID_LIST  = 14,   // sizeof(int) * 1000000
        SECTOR_VERT_NUM = 15,   // sizeof(int) * 1000000
        SECTOR_IDX_NUM  = 16,   // sizeof(int) * 1000000
    };

    enum ReconstructionCmd
    {
        START = 0,
        STOP = 1,
        SHOW_INFO = 2,
        EXTRACT_POINT_CLOUD = 3,
        EXTRACT_VERTEX_NORMAL = 4,
        EXPORT_MODEL_RIGHT_HAND = 5,
        EXPORT_MODEL_FOR_UNITY = 6,
        EXTRACT_COLLIDER_ONE_TIME = 7,
    };

    enum ReconstructionCallback
    {
        EXPORT_PROGRESS = 1,
        BASIC = 1001,
    };

    public enum ReconstructionDataSource
    {
        HMD = 0,
        DATASET = 1
    }
    public enum ReconstructionQuality
    {
        LOW = 2,
        MID = 3,
        HIGH = 4,
    }

    public enum ReconstructionLiveMeshExtractMode
    {
        VERTEX_WITHOUT_NORMAL = 0,
        VERTEX_WITH_NORMAL = 1,
        FACE_NORMAL = 2,
    };

    public enum ReconstructionLiveColliderType
    {
        CONVEX_SHAPE = 0,
        BOUNDING_BOX_SHAPE = 1,
    };

    public enum ReconstructionExportStage
    {
        STAGE_EXTRACTING_MODEL = 0x0017,
        STAGE_COMPACTING_TEXTURE = 0x0018,
        STAGE_SAVING_MODEL_FILE = 0x0019,
        STAGE_EXTRACTING_COLLIDER = 0x001A,        
    }

    public enum ReconstructionDisplayMode
    {
        FULL_SCENE = 0,
        FIELD_OF_VIEW = 1,
        ADAPTIVE_MESH = 2,
    }
    #endregion

    #region Chaperone
    enum ChaperoneParam
    {
        OUTPUT_WIDTH,
        OUTPUT_HEIGHT,
        OUTPUT_CHAANEL_0,
        OUTPUT_CHAANEL_1,
        TYPE,
    };
    enum ChaperoneDataMask
    {
        LEFT_FRAME  = 0,
        MASK_MAP    = 1,
        DEPTH_MAP   = 2,
        FRAME_SEQ   = 3,
        TIME_STP    = 4,
        POSE        = 5,
    };
    #endregion
}
