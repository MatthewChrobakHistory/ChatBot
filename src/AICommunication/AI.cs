using System.Collections.Generic;

namespace AICommunication
{
    public class AI
    {
        public List<Concept> Concepts = new List<Concept>();

        private string _question = "what where who";
        private string _beingWord = "am are is";
        private string _pronouns = "he she it I this that there then you";
        private string _validation = "am are is do does will";

        public void CheckInput(string input) {
            string[] inputwords = input.ToLower().Split(' ');

            // Check for specific sentence types.
            if (inputwords.Length == 3) {
                // Look for concepts
                if (_question.Contains(inputwords[0]) && _beingWord.Contains(inputwords[1])) {
                    string concept = inputwords[2];
                }
            } else {
                // Look for validation.
                if (_validation.Contains(inputwords[0]) && _pronouns.Contains(inputwords[1])) {

                }
            }
        }
    }
}
