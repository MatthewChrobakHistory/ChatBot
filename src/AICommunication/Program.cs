using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AICommunication
{
    public static class Program
    {
        private static Communication _coms = new Communication();
        public static bool Learn = false;
        private static string _learnPhrase = "";

        public static string StartupPath = AppDomain.CurrentDomain.BaseDirectory;

        static void Main(string[] args) {
            Program.Write("Hello!");
            bool learned = false;

            while (true) {
                string input = Console.ReadLine();

                learned = false;

                if (input == "/adminsave") {
                    _coms.Save();
                    Learn = false;
                } else {
                    if (Learn) {
                        if (!_coms.Phrases[_learnPhrase].responses.Contains(input)) {
                            _coms.Phrases[_learnPhrase].AddResponse(input);
                        }
                        learned = true;
                        Learn = false;
                    }

                    _coms.Check(input, learned);
                }
            }
        }

        public static void Write(string output) {
            Console.WriteLine("ChatBot: " + output);
            _learnPhrase = output;
        }
    }
}
