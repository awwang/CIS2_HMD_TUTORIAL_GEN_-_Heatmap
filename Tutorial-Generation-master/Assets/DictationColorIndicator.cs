using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using SimpleJSON;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles displayed text (dictation result, camera status, step number).
/// Additionally writes to JSON file.
/// </summary>
public class DictationColorIndicator : MonoBehaviour, IDictationHandler {

    /// <summary>
    /// Renderer of DictationIndicator object
    /// </summary>
    private new Renderer renderer;

	public GameObject stepDisplay;
	public GameObject dictationDisplay;
	public GameObject imageDisplay;
	public GameObject cameraStatus;

    /// <summary>
    /// TextMesh displaying dictation result
    /// </summary>
    public TextMesh dictationOutputText;

    /// <summary>
    /// Step number displayed
    /// </summary>
    public TextMesh stepCountDisplayText;
    //public GameObject objectToBeManipulated;

    /// <summary>
    /// Camera status displayed to user
    /// </summary>
    public TextMesh cameraStatusText;

	//public GameObject quad;

	/// <summary>
	/// Step count (initialized at 0)
	/// </summary>
	public int stepCount = 0;

    /// <summary>
    /// Recording status
    /// </summary>
    private bool isRecording;
	public GameObject heatmapCanvas;

	/// <summary>
	/// String representing JSON file with recorded steps
	/// </summary>
	//private static string blankJSON = System.IO.File.ReadAllText(@"Assets\BlankJSON.JSON");
    //JSONNode data = SimpleJSON.JSON.Parse(blankJSON);
	//JSONNode blankStep;

	/// <summary>
	/// The path to the image in the applications local folder.
	/// </summary>
	//private string JSONLocalFilePath;

	/// <summary>
	/// The path to the users picture folder.
	/// </summary>
	//private string JSONFolderPath;

	/// <summary>
	/// Get renderer at start
	/// </summary>
	void Awake()
	{
		renderer = GetComponent<Renderer>();
		heatmapCanvas.SetActive(false);
		//blankStep = data["JuxtopiaTask"]["Steps"][0];
		/*#if NETFX_CORE
				getPicturesFolderAsync();
		#endif

			}

		#if NETFX_CORE

			private async void getPicturesFolderAsync() {
				Windows.Storage.StorageLibrary picturesStorage = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);
				JSONFolderPath = picturesStorage.SaveFolder.Path;
			}

		#endif*/
	}
	/// <summary>
	/// Beginning dictation on voice command
	/// </summary>
	public void OnDictationStart() {
        renderer.material.color = Color.red;
        dictationOutputText.color = Color.red;
        ToggleRecording(); // toggle dictation recording
    }

    /// <summary>
    /// Begin next step. Reset text and increment counter.
    /// </summary>
    public void OnNextStep() {
		/*Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
		Color transp = Color.white;
		transp.a = 0.0f;
		quadRenderer.material.color = transp;*/

		/*if (stepCount != 0)
		{
			//Debug.LogWarning(imageCapture.imageFileName);
			//data["JuxtopiaTask"]["Steps"][stepCount]["DisplayAnchoredImage"]["Image_Path"] = imageCapture.imageFileName;
		}*/

		stepCount++; //increment step counter
        stepCountDisplayText.text = "Step " + stepCount.ToString(); // update step number
        dictationOutputText.text = "Say, \"Start recording\" to record text."; // reset instructional text
        cameraStatusText.text = "Camera ready"; // reset camera status
        renderer.material.color = Color.white;
        dictationOutputText.color = Color.white;
    }

	public void OnHeatmapActivation()
	{
		//AudioListener a = Camera.current.GetComponent<AudioListener>();
		//canvas.SetActive(false);
		stepDisplay.GetComponent<Renderer>().enabled = false;
		dictationDisplay.GetComponent<Renderer>().enabled = false;
		imageDisplay.GetComponent<Renderer>().enabled = false;
		cameraStatus.GetComponent<Renderer>().enabled = false;
		heatmapCanvas.SetActive(true);
		//SceneManager.LoadScene("HoloLensClient", LoadSceneMode.Additive);
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName("HoloLensClient"));
	}

	public void OnHide()
	{
		Debug.LogWarning("hide command");
		stepDisplay.GetComponent<Renderer>().enabled = true;
		dictationDisplay.GetComponent<Renderer>().enabled = true;
		imageDisplay.GetComponent<Renderer>().enabled = true;
		cameraStatus.GetComponent<Renderer>().enabled = true;

		//canvas.SetActive(true);
		heatmapCanvas.SetActive(false);
		//SceneManager.LoadScene("scene", LoadSceneMode.Single);
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName("scene"));
	}

	/// <summary>
	/// When dictation is complete, end recording.
	/// </summary>
	public void OnDictationComplete(DictationEventData eventData)
    {
        dictationOutputText.text = eventData.DictationResult; // record result

        isRecording = false; // recording status false
        StartCoroutine(DictationInputManager.StopRecording()); // end recording
        renderer.material.color = Color.green;
        dictationOutputText.color = Color.green;
		/*if (stepCount == 0)
		{
			data["JuxtopiaTask"]["Steps"][stepCount]["Name"] = "TASK: " + eventData.DictationResult;
			data["JuxtopiaTask"]["Steps"][stepCount]["DisplayAnchoredText"][0]["Text"] = "Welcome to \"" + eventData.DictationResult + "\" Training! You can go back and forth in the steps by saying \"Previous\" and \"Next\"";
			data["JuxtopiaTask"]["Name"] = eventData.DictationResult;
			data["JuxtopiaTask"]["ResourcesPath"] = "test";
		}
		else
		{
			data["JuxtopiaTask"]["Steps"][stepCount] = blankStep;

			data["JuxtopiaTask"]["Steps"][stepCount]["Name"] = "Step " + stepCount.ToString();
			data["JuxtopiaTask"]["Steps"][stepCount]["DisplayAnchoredText"][0]["Text"] = eventData.DictationResult;
		}*/

	}

	/// <summary>
	/// When tutorial is complete, save.
	/// </summary>
	public void OnFinish()
	{
		dictationOutputText.text = "Tutorial end";
		stepCount++;
		//data["JuxtopiaTask"]["Steps"][stepCount] = blankStep;
		//data["JuxtopiaTask"]["Steps"][stepCount]["Name"] = "Task Completed";
		//data["JuxtopiaTask"]["Steps"][stepCount]["DisplayAnchoredText"][0]["Text"] = "Thank you for completing this training session!";
		//Debug.LogWarning(data.ToString());
		stepCountDisplayText.text = "Tutorial Complete";

	}

	/// <summary>
	/// Toggle recording (begin if not recording, end if still recording)
	/// </summary>
	private void ToggleRecording()
    {
        if (isRecording)
        {
            isRecording = false;
            StartCoroutine(DictationInputManager.StopRecording());
            Debug.LogWarning("recording stopped");
            dictationOutputText.color = Color.green;
        }
        else
        {
            isRecording = true; // set recording status to true
            StartCoroutine(DictationInputManager.StartRecording(5f, 2f, 30)); // begin recording
            dictationOutputText.color = Color.red;
            renderer.material.color = Color.red;
        }
    }

    /// <summary>
    /// When dictation manager gets hypothesis, set text to that hypothesis.
    /// </summary>
    public void OnDictationHypothesis(DictationEventData eventData)
    {
        dictationOutputText.text = eventData.DictationResult;
    }

    /// <summary>
    /// When dictation manager gets result, set text to that result.
    /// </summary>
    public void OnDictationResult(DictationEventData eventData)
    {
        dictationOutputText.text = eventData.DictationResult;
    }

    /// <summary>
    /// If dictation manager encounters error, end recording.
    /// </summary>
    public void OnDictationError(DictationEventData eventData)
    {
        isRecording = false; // set recording status to false
        dictationOutputText.color = Color.red;
        renderer.material.color = Color.red;
        StartCoroutine(DictationInputManager.StopRecording()); // end recording
    }
}
