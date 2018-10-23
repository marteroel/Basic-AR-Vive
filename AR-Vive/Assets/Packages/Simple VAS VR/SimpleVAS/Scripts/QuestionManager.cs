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

		private CsvWrite csvWriter;
		private CsvReadMultiple csvReader;
		private VASLabeler labeler;

		public GameObject UiObject;
		public GameObject CameraUI;

		public bool isAR;
		//public SR_SceneManager AR_sceneManager;

		public static string questionnaireItem, VASvalue;

		private int currentItem, currentQuestionnaire;

		public static int currentCondition;
		public static string currentQuestionnaireToWrite;

		//public ViveSR viveSR;

		// Use this for initialization
		void Start () {

			currentItem = 0;
			currentQuestionnaire = 0;
			nextButton.interactable = false;

			csvWriter = GetComponent<CsvWrite> ();
			csvReader = GetComponent<CsvReadMultiple> ();
			labeler = GetComponent<VASLabeler> ();
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

			currentQuestionnaireToWrite = currentQuestionnaire.ToString ();
			//nextButton.interactable = false;
			questionnaireItem = currentItem.ToString ();
			VASvalue = scrollValue.value.ToString();
			scrollValue.value = 0.5f;
			csvWriter.onNextButtonPressed ();

			currentItem ++;

			if (currentItem < questionList.Count) {
				questionUI.text = questionList [currentItem];
			}


			else if (currentItem == questionList.Count) {
				currentQuestionnaire++;
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
