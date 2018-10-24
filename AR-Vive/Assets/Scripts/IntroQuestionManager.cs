using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleVAS;

namespace SimpleVAS {

	public class IntroQuestionManager : MonoBehaviour {

		List<string> questionList = new List<string>();

		public Text questionUI;
		public Button nextButton;
		public Scrollbar scrollValue;
		public ConditionLoader sceneLoader;

		public CsvWrite csvWriter;
		private VASLabeler labeler;
		private CsvReadMultiple csvReader;
		//private CsvReadMultiple csvReader;

		//public GameObject UiObject;


		private int currentItem, currentQuestionnaire, itemToWrite;

		public static int currentCondition;


		// Use this for initialization
		void Start () {

			CSVconditionWriter.condition = "Mood";
			currentItem = 0;
			nextButton.interactable = false;
			csvReader = GetComponent<CsvReadMultiple> ();
			labeler = GetComponent<VASLabeler> ();

			csvReader.LoadQuestionnaire(currentQuestionnaire);
			questionList = csvReader.questionnaireInput;
			questionUI.text = questionList [currentItem];
			labeler.ChangeLabels (currentQuestionnaire);

		}

		void Update() {

		}



		public void OnScaleSelection(){

			nextButton.interactable = true;

		}


		public void OnNextButton() {

			nextButton.interactable = false;
			QuestionManager.questionnaireItem = itemToWrite.ToString();
			QuestionManager.VASvalue = scrollValue.value.ToString();//This is a bit problematic but solved in this dirty way
			//Debug.Log ("THE VALUE IS " + QuestionManager.VASvalue);
			scrollValue.value = 0.5f;
			csvWriter.onNextButtonPressed ();

			currentItem ++;
			itemToWrite++;

			if (currentItem < questionList.Count) {
				labeler.ChangeLabels (currentQuestionnaire);
				questionUI.text = questionList [currentItem];
				Debug.Log ("there are still items in the current questionnaire");
			}


			else if (currentItem == questionList.Count) {
				currentQuestionnaire++;
				currentItem = 0;
				questionList.Clear();

				if (currentQuestionnaire < csvReader.files.Length) {
					labeler.ChangeLabels (currentQuestionnaire);
					csvReader.LoadQuestionnaire (currentQuestionnaire);
					//UiObject.SetActive (true);
					questionList = csvReader.questionnaireInput;
					questionUI.text = questionList [currentItem];
				}

				else  sceneLoader.LoadScene ();
			}
		}


	}

}