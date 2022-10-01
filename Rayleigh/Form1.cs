using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Rayleigh
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine r = new SpeechRecognitionEngine();
        SpeechSynthesizer s = new SpeechSynthesizer();

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "hallo", "wie geht es dir", "Mir geht es gut", "Wie viel Uhr ist es", "Welcher Tag ist heute" 
            ,"Test" ,"Stopp"});

            GrammarBuilder gbuilder = new GrammarBuilder();
            gbuilder.Append(commands);

            Grammar grammar = new Grammar(gbuilder);

            r.LoadGrammar(grammar);
            r.SetInputToDefaultAudioDevice();
            r.SpeechRecognized += recEngine_SpeechRecognized;

            r.RecognizeAsync(RecognizeMode.Multiple);
            s.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Adult);

            s.SpeakAsync("Hallo, ich heiße Rayleigh. Es freut mich dich kennen zu lernen");

        }
        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "Rayleigh":
                    s.SpeakAsync("Willkommen");
                    break;
                case "hallo":
                    s.SpeakAsync("Hallo Nutzer");
                    break;
                case "wie geht es dir":
                    s.SpeakAsync("Mir geht es Gut und wie geht es dir");
                    break;
                case "Mir geht es gut":
                    s.SpeakAsync("Das freut mich");
                    break;
                case "Wie viel Uhr ist es":
                    s.SpeakAsync("Es ist" + DateTime.Now.ToString("HH") + " Uhr " + DateTime.Now.ToString("mm"));
                    break;
                case "Welcher Tag ist heute":
                    s.SpeakAsync("Heute ist der" + DateTime.Now.ToString("d"));
                    break;
                case "Test":
                    s.SpeakAsync("Hallo Nutzer ich mache jetzt einen Testdurchlauf Test beginnt Jetzt");
                    s.SpeakAsync("Mir geht es Gut und wie geht es dir");
                    s.SpeakAsync("Es ist" + DateTime.Now.ToString("HH") + " Uhr " + DateTime.Now.ToString("mm"));
                    s.SpeakAsync("Heute ist der" + DateTime.Now.ToString("d"));
                    break;
                case "Stopp":
                    Application.Exit(); 
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToLongTimeString();
            lbDate.Text = DateTime.Now.ToLongDateString();
        }
    }
}
