namespace TestMenu
{
    class Person
    {
        public string name;
        public string gender;
        public int age;
        public string address;

        public override string ToString()
        {
            return $"Name: {this.name}, Gender: {this.gender}, Age: {this.age}, Address: {this.address}";
        }
    }
}
