using System;
using AVFoundation;
namespace App12
{
	public class TextToSpeechImplementation
	{
		public TextToSpeechImplementation()
		{
		}

		static public void Speak(string text)
		{
			var speechSynthesizer = new AVSpeechSynthesizer();

			var speechUtterance = new AVSpeechUtterance(text)
			{
				Rate = AVSpeechUtterance.MaximumSpeechRate / 3,
				Voice = AVSpeechSynthesisVoice.FromLanguage("en-US"),
				Volume = 0.5f,
				//PitchMultiplier = -1.0f
			};

			speechSynthesizer.SpeakUtterance(speechUtterance);
		}
	}
}
