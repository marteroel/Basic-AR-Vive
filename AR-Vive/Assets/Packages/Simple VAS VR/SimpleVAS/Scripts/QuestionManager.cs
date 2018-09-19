using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

namespace SimpleVAS {
	
	public class QuestionManager : MonoBehaviour {

		List<string> questionList = new List<string>();

		public Text questionUI;
		public Button nextButton;
		public Scrollbar scrollValue;
		public ConditionLoader sceneLoader;

		private CsvWrite csvWriter;
		private CsvReadMultiple csvReader;

		public GameObject UiObject;

		public static string questionnaireItem, VASvalue;

		private int currentItem, currentQuestionnaire;

		public static int currentCondition;


		// Use this for initialization
		void Start () {

			currentItem = 0;
			nextButton.interactable = false;

			csvWriter = GetComponent<CsvWrite> ();
			csvReader = GetComponent<CsvReadMultiple> ();
		}

		void Update() {

			if (Input.GetKeyDown ("space") && UiObject.activeSelf != true) {
				ManageUI ();
			}
	
		}

		public void ManageUI (){
			
			if (UiObject.activeSelf == true)
				UiObject.SetActive (false);
			
			else {
				csvReader.LoadQuestionnaire(currentQuestionnaire);
				UiObject.SetActive (true);
				questionList = csvReader.questionnaireInput;
				questionUI.text = questionList[currentItem];
			}
		}
			
		public void OnScaleSelection(){
			
			nextButton.interactable = true;

		}


		public void OnNextButton() {
		
			nextButton.interactable = false;
			questionnaireItem = currentItem.ToString ();
			VASvalue = scrollValue.value.ToString();
			csvWriter.onNextButtonPressed ();

			currentItem ++;

			if (currentItem < questionList.Count) {
				questionUI.text = questionList [currentItem];
				Debug.Log ("there are still items in the current questionnaire");
			}


			else if (currentItem == questionList.Count) {
				currentQuestionnaire++;
				currentItem = 0;
				questionList.Clear();
				//ManageUI();

				if (currentQuestionnaire < csvReader.files.Length) ManageUI ();
				else sceneLoader.LoadScene ();
			}
		}


	}
	
}
