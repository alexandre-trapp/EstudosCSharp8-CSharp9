using System;

namespace EstudosCSharp9
{
    /// <summary>
    /// Estou usando .Net Core 5 (Preview)
    /// Exemplos construídos com base na documentação do C# 9
    /// em https://docs.microsoft.com/pt-br/dotnet/csharp/whats-new/csharp-9
    /// </summary
    class Program
    {
        static void Main(string[] args)
        {
            // Tipos de registro "record" e herança
            Console.WriteLine($"-------- Tipos de registro 'record' e herança -------- {Environment.NewLine}");

            var person = new Person("Alexandre", "Trapp");
            var teacher = new Teacher("Martin", "Fowler", "Consultant");

            Console.WriteLine($"person = {person}");
            Console.WriteLine($"teacher = {teacher}");
            Console.WriteLine(string.Empty);
        }
    }

    // tipos de registro "record"
    public record Person
    {
        public string LastName { get; }
        public string FirstName { get; }

        public Person(string first, string last) => (FirstName, LastName) = (first, last);
    }

    public record Teacher : Person
    {
        public string Subject { get; }

        public Teacher(string first, string last, string sub)
            : base(first, last) => Subject = sub;
    }
}
