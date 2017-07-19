using System;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Speech;
using Android.Speech.Tts;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsAssistant.Assistant;
using XamarinFormsAssistant.Assistant.Global.Animations;
using XamarinFormsAssistant.Mocks;
using XamarinFormsAssistant.Prototype.Application;

namespace XamarinFormsAssistant
{
    [Activity(Label = "XamarinFormsAssistant", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FormsApplicationActivity, TextToSpeech.IOnInitListener, IRecognitionListener
    {
        public bool VolumeDownPressed { get; set; }
        private SpeechRecognizer Recognizer { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this,bundle);

            OperatingSystemMethods.activityContext = this;
            DependencyService.Register<OperatingSystemMethods>();

            //MOCK PRECEPTOS
            using (StreamReader streamReader = new StreamReader(Assets.Open("Preceptos.txt"), Encoding.GetEncoding("iso-8859-1")))
            {
                while (streamReader.Peek() >= 0)
                {
                    MockPreceptos.AddPrecepto(streamReader.ReadLine());
                }
            }
            //////

            try
            {
                LoadApplication(new MyApp());
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            switch (keyCode)
            {
                case Keycode.VolumeDown:
                    if (!VolumeDownPressed)
                    {
                        VolumeDownPressed = true;
                        InitSpeechIntent();
                        MessagingCenter.Send(AssistantAnimations.BackgroundColor.OnName, AssistantAnimations.BackgroundColor.OnName);
                        MessagingCenter.Send(AssistantAnimations.AssistantCube.OnName, AssistantAnimations.AssistantCube.OnName);
                        MessagingCenter.Send(AssistantAnimations.AssistantCubeAppear.OnName, AssistantAnimations.AssistantCubeAppear.OnName);
                    }
                    return true;
            }
            return base.OnKeyDown(keyCode, e);
        }


        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            switch (keyCode)
            {
                case Keycode.VolumeDown:
                    VolumeDownPressed = false;
                    MessagingCenter.Send(AssistantAnimations.BackgroundColor.OffName, AssistantAnimations.BackgroundColor.OffName);
                    MessagingCenter.Send(AssistantAnimations.AssistantCube.OffName, AssistantAnimations.AssistantCube.OffName);
                    MessagingCenter.Send(AssistantAnimations.AssistantCubeAppear.OffName, AssistantAnimations.AssistantCubeAppear.OffName);
                    return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public void InitSpeechIntent()
        {
            Recognizer?.Destroy();
            SpeechTextResult = "";

            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "Hable ahora...");
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 500);
            voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 6000);
            voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);

            Recognizer = SpeechRecognizer.CreateSpeechRecognizer(this);
            Recognizer.SetRecognitionListener(this);

            Recognizer.StartListening(voiceIntent);
        }

        public void OnBeginningOfSpeech()
        {

        }

        public void OnBufferReceived(byte[] buffer)
        {

        }

        public void OnEndOfSpeech()
        {
        }

        public void OnError(SpeechRecognizerError error)
        {
        }

        public void OnEvent(int eventType, Bundle @params)
        {

        }

        public void OnPartialResults(Bundle partialResults)
        {

        }

        public void OnReadyForSpeech(Bundle @params)
        {

        }

        public string SpeechTextResult { get; set; } = "";
        public float CurrentDb { get; set; }
        public void OnResults(Bundle results)
        {
            var SpeechMatches = results.GetStringArrayList(SpeechRecognizer.ResultsRecognition);
            var SpeechToText = SpeechMatches?.FirstOrDefault();
            if (SpeechToText != null)
            {
                Assistant.Assistant.GetInstance().Interpret(SpeechToText);
            }

        }

        private uint _nextSpeechEnd = 0;
        public void OnRmsChanged(float rmsdB)
        {
        }

        public void OnInit(OperationResult status)
        {

        }

    }

    public class OperatingSystemMethods : IOperatingSystemMethods
    {
        public static Activity activityContext;
        public void Vibrate(long milliseconds)
        {
           Android.OS.Vibrator.FromContext(activityContext).Vibrate(milliseconds);
        }
    }

    public interface IOperatingSystemMethods
    {
        void Vibrate(long milliseconds);
    }
}