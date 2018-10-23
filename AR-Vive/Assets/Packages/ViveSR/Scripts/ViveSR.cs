using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vive.Plugin.SR
{
    public class ViveSR : MonoBehaviour
    {
        public static FrameworkStatus FrameworkStatus { get; protected set; }
        public static string LastError { get; protected set; }
        public bool EnableAutomatically;
        public ViveSR_DualCameraRig DualCameraRig;
        [SerializeField] protected ViveSR_RigidReconstructionRenderer RigidReconstruction;

        [HideInInspector] public List<UnityAction> OnStartFailed = new List<UnityAction>();
        [HideInInspector] public List<UnityAction> OnStartComplete = new List<UnityAction>();

        private static ViveSR Mgr = null;
        public static ViveSR Instance
        {
            get
            {
                if (Mgr == null)
                {
                    Mgr = FindObjectOfType<ViveSR>();
                }
                if (Mgr == null)
                {
                    Debug.LogError("ViveSR does not be attached on GameObject");
                }
                return Mgr;
            }
        }

		//MR
		public bool realWorldSwitch;
		private bool isOn = false;

        void Start()
        {
            FrameworkStatus = FrameworkStatus.STOP;
            if (EnableAutomatically)
            {
                StartFramework();
            }
        }

		void Update() {//MR
			if (!isOn) {
				if (realWorldSwitch) {
					StartFramework ();
					isOn = true;
				}
			}
			if (isOn) {
				if (!realWorldSwitch)
					StopFramework ();	
					isOn = false;
			}
		}

        void OnDestroy()
        {
            StopFramework();
        }

        public virtual void StartFramework()
        {
            if (FrameworkStatus == FrameworkStatus.WORKING) return;
            StartCoroutine(StartFrameworkCoroutine());
        }

        protected virtual IEnumerator StartFrameworkCoroutine()
        {
            int result = (int)Error.WORK;
            // Before initialize framework
            yield return new WaitForEndOfFrame();

            if (result == (int)Error.WORK) result = ViveSR_InitialFramework();
            if (result == (int)Error.WORK) Debug.Log("[ViveSR] Initial Framework : " + result);
            else
            {
                FrameworkStatus = FrameworkStatus.ERROR;
                LastError = "[ViveSR] Initial Framework : " + result;
                Debug.LogError(LastError);
                for (int i = 0; i < OnStartFailed.Count; i++) if (OnStartFailed[i] != null) OnStartFailed[i]();
                StopFramework();
                yield break;
            }
            yield return new WaitForEndOfFrame();

            if (RigidReconstruction != null) RigidReconstruction.InitRigidReconstructionParam();
            yield return new WaitForEndOfFrame();

            // Start framework
            if (result == (int)Error.WORK) result = ViveSR_StartFramework();
            if (result == (int)Error.WORK)
            {
                FrameworkStatus = FrameworkStatus.WORKING;
                Debug.Log("[ViveSR] Start Framework : " + result);
            }
            else
            {
                FrameworkStatus = FrameworkStatus.ERROR;
                LastError = "[ViveSR] Start Framework : " + result;
                Debug.LogError(LastError);
                for (int i = 0; i < OnStartFailed.Count; i++) if (OnStartFailed[i] != null) OnStartFailed[i]();
                StopFramework();
                yield break;
            }
            yield return new WaitForEndOfFrame();


            if (FrameworkStatus == FrameworkStatus.WORKING)
            {
                if (DualCameraRig != null)
                {
                    DualCameraRig.gameObject.SetActive(true);
                    DualCameraRig.Initial();
                }
                if (RigidReconstruction != null) RigidReconstruction.gameObject.SetActive(true);
            }
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < OnStartComplete.Count; i++) if (OnStartComplete[i] != null) OnStartComplete[i]();
        }

        public virtual void StopFramework()
        {
            if (DualCameraRig != null)
            {
                DualCameraRig.Release();
                DualCameraRig.gameObject.SetActive(false);
            }
            if (RigidReconstruction != null) RigidReconstruction.gameObject.SetActive(false);

            if (FrameworkStatus != FrameworkStatus.STOP)
            {
                int result = ViveSR_StopFramework();
                if (result != (int)Error.WORK)
                {
                    FrameworkStatus = FrameworkStatus.ERROR;
                    LastError = "[ViveSR] Stop Framework : " + result;
                    Debug.LogError(LastError);
                }
                else
                {
                    FrameworkStatus = FrameworkStatus.STOP;
                    Debug.Log("[ViveSR] Stop Framework");
                }
            }
            else
            {
                Debug.Log("[ViveSR] Stop Framework : not open");
            }
        }

        protected virtual int ViveSR_InitialFramework()
        {
            int result = (int)Error.FAILED;
            result = ViveSR_Framework.Initial();
            result = ViveSR_Framework.SetLogLevel(10);

            result = ViveSR_Framework.CreateModule((int)ModuleType.ENGINE_SEETHROUGH, ref ViveSR_Framework.MODULE_ID_SEETHROUGH);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] StartModule Error " + result); return result; }

            result = ViveSR_Framework.CreateModule((int)ModuleType.ENGINE_DEPTH, ref ViveSR_Framework.MODULE_ID_DEPTH);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] StartModule Error " + result); return result; }

            result = ViveSR_Framework.CreateModule((int)ModuleType.ENGINE_RIGID_RECONSTRUCTION, ref ViveSR_Framework.MODULE_ID_RIGID_RECONSTRUCTION);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] StartModule Error " + result); return result; }
            return result;
        }

        protected virtual int ViveSR_StartFramework()
        {
            int result = (int)Error.FAILED;

            result = ViveSR_Framework.StartModule(ViveSR_Framework.MODULE_ID_SEETHROUGH);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] StartModule Error " + result); return result; }

            result = ViveSR_Framework.StartModule(ViveSR_Framework.MODULE_ID_DEPTH);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] StartModule Error " + result); return result; }

            result = ViveSR_Framework.StartModule(ViveSR_Framework.MODULE_ID_RIGID_RECONSTRUCTION);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] StartModule Error " + result); return result; }

            //result = ViveSR_ModuleLink(MODULE_ID_SEETHROUGH, MODULE_ID_DEPTH, (int)WorkLinkMethod.ACTIVE);
            //if (result != (int)Error.WORK) { Debug.Log("ViveSR_ModuleLink Error " + result); return result; }

            result = ViveSR_Framework.ModuleLink(ViveSR_Framework.MODULE_ID_DEPTH, ViveSR_Framework.MODULE_ID_RIGID_RECONSTRUCTION, (int)WorkLinkMethod.ACTIVE);
            if (result != (int)Error.WORK) { Debug.Log("[ViveSR] ModuleLink Error " + result); return result; }

            return result;
        }

        protected virtual int ViveSR_StopFramework()
        {
            return ViveSR_Framework.Stop();
        }
    }
}