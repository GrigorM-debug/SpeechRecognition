using System.Speech.Recognition;

namespace SpeechRecognition
{
    internal class Program
    {
        static StreamWriter fileWriter;
        static SpeechRecognitionEngine recognizer;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the language code (e.g., en-US, fr-FR, es-ES): ");
            string languageCode = Console.ReadLine();

            string filePath = "voicetypedtext.txt";

            fileWriter = new StreamWriter(filePath);


            recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo(languageCode));

            // Create and load a dictation grammar.  
            recognizer.LoadGrammar(new DictationGrammar());

            // Add a handler for the speech recognized event.  
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            // Configure input to the speech recognizer.  
            recognizer.SetInputToDefaultAudioDevice();

            Console.WriteLine($"Start speaking in {languageCode}. Press Enter to stop.");

            // Start asynchronous, continuous speech recognition.  
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            // Keep the console window open.  
            Console.ReadLine();

            recognizer.RecognizeAsyncStop();
            fileWriter.Close();

            Console.WriteLine("Voice typing stopped. File saved as " + filePath);
        }

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Recognized text: " + e.Result.Text);
            fileWriter.WriteLine(e.Result.Text);
        }
    }
}
