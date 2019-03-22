using System;
using System.IO;
using System.Collections.Generic;

namespace TestMenu
{
    class Program
    {
        public const bool DEBUG = false;
        public const string DATA_DIRECTORY_NAME = "data";

        static int Main(string[] args)
        {
            bool programRunning = true;

            List<Person> people = LoadData();

            while (programRunning)
            {
                bool exit = MainMenu(people);

                if (exit)
                    programRunning = false;
            }

            SaveData(people);

            return 0;
        }

        static List<Person> LoadData()
        {
            bool loaded = false;

            if (!Directory.Exists(AppContext.BaseDirectory + DATA_DIRECTORY_NAME))
                Directory.CreateDirectory(AppContext.BaseDirectory + DATA_DIRECTORY_NAME);

            string[] files = Directory.GetFiles(AppContext.BaseDirectory + DATA_DIRECTORY_NAME);

            Console.Clear();
            Console.WriteLine("Loading data...");

            int i = 1;

            foreach (string file in files)
            {
                Console.WriteLine($"\t{i}) {file}");
                i++;
            }
            Console.WriteLine($"\t{i}) New");

            while (!loaded)
            {
                Console.WriteLine("Choose a file to load in");
                int input = Util.GetConsoleNumberInput();

                if (input != -1 && input < files.Length+1)
                    return SaveLoad.LoadPeopleFromFile(files[input-1]);
                else if (input == i)
                    return new List<Person>();
                else
                    Console.WriteLine("Did not get a valid option. Try again...");
            }

            return null;
        }

        static void SaveData(List<Person> people)
        {
            bool saved = false;

            if (!Directory.Exists(AppContext.BaseDirectory + DATA_DIRECTORY_NAME))
                Directory.CreateDirectory(AppContext.BaseDirectory + DATA_DIRECTORY_NAME);

            string[] files = Directory.GetFiles(AppContext.BaseDirectory + DATA_DIRECTORY_NAME);

            Console.Clear();
            Console.WriteLine("Saving data...");

            int i = 1;

            foreach (string file in files)
            {
                Console.WriteLine($"\t{i}) {file}");
                i++;
            }
            Console.WriteLine($"\t{i}) New");

            while (!saved)
            {
                Console.WriteLine("Choose a file to save in");
                int input = Util.GetConsoleNumberInput();

                if (input != -1 && input < files.Length + 1)
                {
                    SaveLoad.SavePeopleToFile(files[input - 1], people);
                    return;
                }
                else if (input == i)
                {
                    Console.Clear();
                    Console.WriteLine("Enter a name for the file");
                    string fileName = Console.ReadLine();
                    SaveLoad.SavePeopleToFile(AppContext.BaseDirectory + DATA_DIRECTORY_NAME + "\\" + fileName + ".txt", people);
                    return;
                }
                else
                    Console.WriteLine("Did not get a valid option. Try again...");
            }
        }

        static bool MainMenu(List<Person> people)
        {
            Console.Clear();
            Console.WriteLine("Person records menu");
            Console.WriteLine("Please enter a number relating to the choices below:");
            Console.WriteLine("\t1) View records");
            Console.WriteLine("\t2) Add a record");
            Console.WriteLine("\t3) Delete a record");
            Console.WriteLine("\t4) Exit");

            int input = Util.GetConsoleNumberInput();

            switch (input)
            {
                case 1:
                    ViewRecords(people);
                    break;
                case 2:
                    Person newPerson = AddRecord();

                    if (newPerson != null)
                        people.Add(newPerson);
                    break;
                case 3:
                    int personToRemove = DeleteRecord(people);

                    if (personToRemove != -1)
                        people.RemoveAt(personToRemove);
                    break;
                case 4:
                    return true;
                default:
                    Console.WriteLine("That is not a valid choice!");
                    Util.PauseConsole();
                    break;
            }

            return false;
        }

        static void ViewRecords(List<Person> people)
        {
            Console.Clear();

            if (people.Count == 0)
                Console.WriteLine("No records!");
            else
            {
                foreach (Person person in people)
                {
                    Console.WriteLine(person.ToString());
                }
            }

            Util.PauseConsole();
        }

        static Person AddRecord()
        {
            Person newPerson = new Person();
            string input;

            Console.Clear();
            Console.WriteLine("You are currently adding a new record...");
            Console.WriteLine("Type an exclamation mark (0) to cancel");

            Console.WriteLine("Please enter the persons name: ");

            input = Console.ReadLine();
            if (input == "0")
                return null;

            newPerson.name = input;

            Console.WriteLine("Please enter the persons gender: ");

            input = Console.ReadLine();
            if (input == "0")
                return null;

            newPerson.gender = input;

            bool gettingAge = true;
            int ageInput = 0;

            while (gettingAge)
            {
                Console.WriteLine("Please enter the persons age: ");

                ageInput = Util.GetConsoleNumberInput();

                if (ageInput == 0)
                    return null;
                else if (ageInput != -1)
                    break;
                else
                    Console.WriteLine("That was not a valid input! Try again...");
            }

            newPerson.age = ageInput;

            Console.WriteLine("Please enter the persons address: ");

            input = Console.ReadLine();
            if (input == "0")
                return null;

            newPerson.address = input;

            return newPerson;
        }

        static int DeleteRecord(List<Person> people)
        {
            int i = 1;

            Console.Clear();

            foreach (Person person in people)
            {
                Console.Write(i + ") ");
                Console.WriteLine(person.ToString());
                i++;
            }

            Console.WriteLine("You are currently deleting a record...");
            Console.WriteLine("Type 0 to cancel");

            bool finished = false;

            while (!finished)
            {
                int input = Util.GetConsoleNumberInput();

                if (input == 0)
                    return -1;

                if (input <= people.Count && people[input-1] != null)
                    return input-1;
                else
                    Console.WriteLine("Could not find a record with that number. Please try again...");
            }

            return -1;
        }
    }
}
