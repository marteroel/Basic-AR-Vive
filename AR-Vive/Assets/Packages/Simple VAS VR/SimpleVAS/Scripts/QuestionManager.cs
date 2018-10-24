using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;
//using Vive.Plugin.SR;

namespace SimpleVAS {
	
	public class QuestionManager : MonoBehaviour {

		List<string> questionList = new List<string>();

		public Text questionUI;
		public Button nextButton;
		public Scrollbar scrollValue;
		public ConditionLoader sceneLoader;
		public AvatarSelector avatarSelector;
		public AR_GloveSelector gloveSelector;

		private CsvWrite csvWriter;
		private CsvReadMultiple csvReader;
		private VASLabeler labeler;
		private CSVconditionWriter conditionWriter;

		public GameObject UiObject;
		public GameObject CameraUI;

		public int qNChangeCondition;//at which questionnaire index to change the condition
		//public bool isAR;
		//public SR_SceneManager AR_sceneManager;

		public static string questionnaireItem, VASvalue;

		private int currentItem, currentQuestionnaire, questionnaireToWrite, itemNumber;

		private bool isPost = false;

		public static int currentCondition;
		public static string currentQuestionnaireToWrite;

		//public ViveSR viveSR;

		// Use this for initialization
		void Start () {

			itemNumber = 0;
			currentItem = 0;
			currentQuestionnaire = 0;
			nextButton.interactable = false;

			csvWriter = GetComponent<CsvWrite> ();
			csvReader = GetComponent<CsvReadMultiple> ();
			labeler = GetComponent<VASLabeler> ();
			conditionWriter = GetComponent<CSVconditionWriter> ();
		}

		void Update() {

			if (Input.GetKeyDown ("space") && UiObject.activeSelf != true) {
				ManageUI ();
			}
	
		}

		public void ManageUI (){
			
			if (UiObject.activeSelf == true) {
				UiObject.SetActive (false);
				CameraUI.SetActive (false);
			}

			else {
				if (itemNumber == qNChangeCondition && !isPost) {
					Debug.Log ("it's CHANGING to POST");
					conditionWriter.ChangeCondition ();
					isPost = true;
					currentItem = 0;
					questionnaireToWrite = 0;
					//change to post avatar here.
					if (SceneManager.GetActiveScene ().name == "VR" && avatarSelector != null)
						avatarSelector.ChangeToPost ();
					if (SceneManager.GetActiveScene ().name == "AR" && gloveSelector != null)
						gloveSelector.AddGlove ();
				}
				
				labeler.ChangeLabels (currentQuestionnaire);
				csvReader.LoadQuestionnaire(currentQuestionnaire);
				UiObject.SetActive (true);
				CameraUI.SetActive (true);
				questionList = csvReader.questionnaireInput;
				questionUI.text = questionList[currentItem];
			}
		}
			
		public void OnScaleSelection(){
			
			nextButton.interactable = true;

		}


		public void OnNextButton() {

			currentQuestionnaireToWrite = questionnaireToWrite.ToString ();
			//nextButton.interactable = false;
			questionnaireItem = currentItem.ToString ();
			VASvalue = scrollValue.value.ToString();
			scrollValue.value = 0.5f;
			csvWriter.onNextButtonPressed ();

			currentItem ++;
			itemNumber++;

			if (currentItem < questionList.Count) {
				questionUI.text = questionList [currentItem];
			}


			else if (currentItem == questionList.Count) {
				currentQuestionnaire++;
				questionnaireToWrite++;
				currentItem = 0;
				questionList.Clear();
				//ManageUI();

				if (currentQuestionnaire < csvReader.files.Length) { 
					ManageUI ();
				} 

				else {
						sceneLoader.LoadScene ();
				}
			}
		}



	}
	
}
