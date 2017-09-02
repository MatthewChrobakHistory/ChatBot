using System.Collections.Generic;

namespace AICommunication
{
    public enum ConceptType
    {
        Person,
        Place,
        Thing,
        Idea,
        Length
    }

    public enum Notiontype
    {
        Statement,
        Question
    }

    public class Concept
    {
        public string Name = "";
        public List<Notion> Notions = new List<Notion>();
        public ConceptType Type = ConceptType.Thing;

        public Concept(string name, ConceptType type) {
            this.Name = name;
            this.Type = type;
        }

        public void AddNotion(string notion) {
            var type = Notiontype.Statement;
            if (notion.EndsWith("?")) {
                type = Notiontype.Question;
            }
            Notions.Add(new Notion(notion, type));
        }

        public static string ConceptToString(ConceptType type) {
            switch (type) {
                case ConceptType.Person:
                    return "person";
                case ConceptType.Place:
                    return "place";
                case ConceptType.Idea:
                    return "idea";
                default:
                    return "thing";
            }
        }
    }

    public class Notion
    {
        public string Sentence = "";
        public Notiontype Type = Notiontype.Statement;

        public Notion(string sentence, Notiontype type) {
            this.Sentence = sentence;
            this.Type = type;
        }
    }
}
