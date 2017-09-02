using System.Collections.Generic;

namespace AICommunication
{
    public class Phrase
    {
        public List<string> responses = new List<string>();

        public Phrase(string[] contents) {
            foreach (string response in contents) {
                if (response != "") {
                    AddResponse(response);
                }
            }
        }

        public void AddResponse(string response) {
            if (!responses.Contains(response)) {
                responses.Add(response);
            }
        }
    }
}
