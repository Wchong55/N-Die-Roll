using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using KKSpeech;

public class RecordingCanvas : MonoBehaviour
{
    public Button startRecordingButton;
    public Text resultText;

    public GameObject interaction;
    private ARTapToPlaceObject dieManager;

    public GameObject diceController;
    private DiceController roll;

  void Start()
  {
    dieManager = interaction.GetComponent<ARTapToPlaceObject>();
    roll = diceController.GetComponent<DiceController>();

    if (SpeechRecognizer.ExistsOnDevice())
    {
      SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
      listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
      listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
      listener.onErrorDuringRecording.AddListener(OnError);
      listener.onErrorOnStartRecording.AddListener(OnError);
      listener.onFinalResults.AddListener(OnFinalResult);
      listener.onPartialResults.AddListener(OnPartialResult);
      listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
      SpeechRecognizer.RequestAccess();
    }
    else
    {
      resultText.text = "Sorry, but this device doesn't support speech recognition";
      startRecordingButton.enabled = false;
    }


  }

    void Update()
    {
        if (resultText.text == "add" || resultText.text == "add die" || resultText.text == "add dye" || resultText.text == "die add" || resultText.text == "dye add" || resultText.text == "spawn" || resultText.text == "spawn die" || resultText.text == "spawn dye" || resultText.text == "die spawn" || resultText.text == "dye spawn")
        {
            dieManager.PlaceObject();
            resultText.text = "";
        }

        if (resultText.text == "remove" || resultText.text == "remove die" || resultText.text == "remove dye" || resultText.text == "die remove" || resultText.text == "dye remove")
        {
            dieManager.RemoveObject();
            resultText.text = "";
        }

        if (resultText.text == "roll" || resultText.text == "roll die" || resultText.text == "roll dye" || resultText.text == "die roll" || resultText.text == "dye roll")
        {
            roll.rollDice();
            resultText.text = "";
        }

    }

  public void OnFinalResult(string result)
  {
    startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
    resultText.text = result;
    //word = resultText.text;
    startRecordingButton.enabled = true;
  }

  public void OnPartialResult(string result)
  {
    resultText.text = result;
  }

  public void OnAvailabilityChange(bool available)
  {
    startRecordingButton.enabled = available;
    if (!available)
    {
      resultText.text = "Speech Recognition not available";
    }
    else
    {
      resultText.text = "Say something :-)";
    }
  }

  public void OnAuthorizationStatusFetched(AuthorizationStatus status)
  {
    switch (status)
    {
      case AuthorizationStatus.Authorized:
        startRecordingButton.enabled = true;
        break;
      default:
        startRecordingButton.enabled = false;
        resultText.text = "Cannot use Speech Recognition, authorization status is " + status;
        break;
    }
  }

  public void OnEndOfSpeech()
  {
    startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
  }

  public void OnError(string error)
  {
    Debug.LogError(error);
    startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
    startRecordingButton.enabled = true;
  }

  public void OnStartRecordingPressed()
  {
    if (SpeechRecognizer.IsRecording())
    {
#if UNITY_IOS && !UNITY_EDITOR
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "Stopping";
			startRecordingButton.enabled = false;
#elif UNITY_ANDROID && !UNITY_EDITOR
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
#endif
    }
    else
    {
      SpeechRecognizer.StartRecording(true);
      startRecordingButton.GetComponentInChildren<Text>().text = "Stop Recording";
      resultText.text = "Say something :-)";
    }
  }
}
