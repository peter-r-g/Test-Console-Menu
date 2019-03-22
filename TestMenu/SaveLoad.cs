using System;
using System.IO;
using System.Collections.Generic;

namespace TestMenu
{
    class SaveLoad
    {
        public static void SavePeopleToFile(string filePath, List<Person> people)
        {
            string[] lines = new string[people.Count];

            int i = 0;
            foreach (Person person in people)
            {
                lines[i] = $"{person.name}<END_ATTRIBUTE>{person.gender}<END_ATTRIBUTE>{person.age}<END_ATTRIBUTE>{person.address}<END_ATTRIBUTE>";
                i++;
            }

            File.WriteAllLines(filePath, lines);
        }

        public static List<Person> LoadPeopleFromFile(string filePath)
        {
            List<Person> people = new List<Person>();

            if (!File.Exists(filePath))
                return people;

            string[] lines = File.ReadAllLines(filePath);

            for (int i=0; i<lines.Length; i++)
            {
                string line = lines[i];
                string[] words = line.Split(new string[] { "<END_ATTRIBUTE>" }, StringSplitOptions.RemoveEmptyEntries);

                Person loadedPerson = new Person();

                loadedPerson.name = words[0];
                loadedPerson.gender = words[1];
                int.TryParse(words[2], out loadedPerson.age);
                loadedPerson.address = words[3];

                people.Add(loadedPerson);
            }

            return people;
        }
    }
}
