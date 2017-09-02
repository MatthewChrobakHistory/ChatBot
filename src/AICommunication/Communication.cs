using System;
using System.Collections.Generic;

namespace AICommunication
{
    public class Communication
    {
        private string _dir = Program.StartupPath + "knownphrases\\";
        public Dictionary<string, Phrase> Phrases = new Dictionary<string, Phrase>();
        public List<string> keys = new List<string>();

        public Communication() {
            if (!System.IO.Directory.Exists(_dir)) {
                System.IO.Directory.CreateDirectory(_dir);
            }

            Load();
        }

        private void Load() {
            foreach (string file in System.IO.Directory.GetFiles(_dir, "*.txt")) {
                string phrase = file.Remove(0, _dir.Length);
                phrase = phrase.Remove(phrase.Length - 4);
                phrase = phrase.Replace('¿', '?');

                keys.Add(phrase);
                Phrases.Add(phrase, new Phrase(System.IO.File.ReadAllLines(file)));
            }
        }

        public void Save() {
            foreach (var phrase in Phrases) {
                System.IO.File.WriteAllLines(_dir + phrase.Key.Replace('?', '¿') + ".txt", phrase.Value.responses.ToArray());
            }
        }

        public void Check(string input, bool learned) {

            if (Phrases.ContainsKey(input)) {
                HandleResponse(input);
            } else if (input.ToLower().StartsWith("do you") || input.ToLower().StartsWith("should you") || input.ToLower().StartsWith("can you")) {
                switch (RNG.Get(0, 2)) {
                    case 1:
                        Program.Write("Yes!");
                        break;
                    case 0:
                        Program.Write("No.");
                        break;
                }
            } else {
                string[] words = input.ToLower().Split(' ');
                int[] count = new int[2];
                string key = "";

                // Sift through all the phrases.
                for (int x = 0; x < keys.Count; x++) {
                    var phrase = keys[x];

                    // Count[1] is the count of the phrase we're currently examining.
                    // Count[0] is the highest count.
                    count[1] = 0;

                    if (phrase.Length >= input.Length) {
                        for (int i = 0; i < words.Length; i++) {
                            if (phrase.ToLower().Contains(words[i])) {
                                count[1]++;
                            }
                        }
                    }

                    // If the current count is bigger than the highest count, 
                    // transfer over all the data we need.
                    if (count[1] > count[0]) {
                        key = phrase;
                        count[0] = count[1];
                    }
                }


                float chance = count[0] / words.Length;
                if (chance >= 0.15f) {
                    HandleResponse(key);
                    return;
                }

                if (!learned) {
                    Phrases.Add(input, new Phrase(new string[] { "" }));
                    keys.Add(input);
                }

                if (RNG.Get(0, 100) <= 75) {
                    AskAQuestion();
                } else {
                    Program.Write("I am still in my learning stages. Keep asking questions!");
                }
                
            }
        }

        public void HandleResponse(string key) {
            if (key != "") {
                string[] responses = Phrases[key].responses.ToArray();

                if (responses.Length != 0) {
                    Program.Write(responses[RNG.Get(0, responses.Length)]);

                    if (RNG.Get(0, 100) <= 75) {
                        AskAQuestion();
                    }
                }
            }
        }

        public void AskAQuestion() {
            Program.Learn = true;
            Program.Write(keys[RNG.Get(0, keys.Count)]);
        }
    }
}
