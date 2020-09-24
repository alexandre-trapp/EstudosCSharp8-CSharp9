using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
            Console.WriteLine($"-------- Tipos de registro 'record', herança, equals, deconstruct, etc -------- {Environment.NewLine}");

            var person = new Person("Alexandre", "Trapp");
            var teacher = new Teacher("Martin", "Fowler", "Consultant");
            var student = new Student("Ronaldo", "Fenomeno", 1);
            var student2 = new Student("Ronaldo", "Fenomeno", 1);

            Console.WriteLine($"person = {person}");
            Console.WriteLine($"teacher = {teacher}");
            Console.WriteLine($"student = {student}");
            Console.WriteLine($"student equals teacher (with == operator)? - {teacher == student}");
            Console.WriteLine($"student equals student2 (with == operator)? - {student == student2}");
            Console.WriteLine($"student equals teacher (with .Equals operator)? - {teacher.Equals(student)}");
            Console.WriteLine($"student equals student2 (with .Equals operator)? - {student.Equals(student2)}");

            Console.WriteLine($"Pet cat:  - {new Pet("Gato")}");
            Console.WriteLine($"Pet dog:  - {new Dog("Tobi")}");

            // deconstruct record person
            var (first, last) = person;
            Console.WriteLine($"deconstruct: first = {first}; last = {last}");

            // expression with in records
            Person brother = person with { FirstName = "Paul" };
            Console.WriteLine($"expression with - brother: {brother}");

            Console.WriteLine(string.Empty);

            // structs com setters soment init (inicializa somente uma vez (imutável))
            Console.WriteLine($"-------- Setters somente Init -------- {Environment.NewLine}");

            var weatherNow = new WeatherObservation
            {
                RecordedAt = DateTime.Now,
                TemperatureInCelsius = 20,
                PressureInMillibars = 998.0m
            };

            // Error! CS8852.
            //weatherNow.TemperatureInCelsius = 18;

            Console.WriteLine("weatherNow: " + weatherNow);
            Console.WriteLine(string.Empty);

            // Instruções de nível superior (consultar classe NivelSuperiorInstruction.cs)
            Console.WriteLine("-------- Instruções de nível superior (consultar classe TopLevelProgram.cs) --------");
            Console.WriteLine("Descomentar classe TopLevelProgram.cs e executar");
            Console.WriteLine(string.Empty);

            // Melhorias na correspondência de padrões
            Console.WriteLine($"-------- Melhorias na correspondência de padrões -------- {Environment.NewLine}");

            Console.WriteLine("A is == A? " + IsLetter('A'));
            Console.WriteLine("b is > A? " + IsLetter('b'));
            Console.WriteLine("y is < z? " + IsLetter('y'));
            Console.WriteLine("Z == Z? " + IsLetter('Z'));

            Console.WriteLine("2 Is letter? " + IsLetter('2'));
            Console.WriteLine("Is .? " + IsLetter('.'));
            Console.WriteLine("Is ,? " + IsLetter(';'));

            bool? objeto = null;

            if (objeto is null)
                Console.WriteLine("objeto nulo");

            objeto = true;
            if (objeto is not null)
                Console.WriteLine("objeto nao nulo");

            Console.WriteLine(string.Empty);

            // recursos de criação e finalização de objetos
            Console.WriteLine($"-------- recursos de criação e finalização de objetos -------- {Environment.NewLine}");

            List<WeatherObservation> _observations = new();
            Console.WriteLine("_observations: " + _observations.GetType().ToString());
            Console.WriteLine(string.Empty);
        }

        // Melhorias na correspondência de padrões
        static bool IsLetter(char c) =>
            c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

        static bool IsLetterOrSeparator(char c) =>
            c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',';
    }


    // tipos de registro "record"

    // forma tradicional
    //public record Person
    //{
    //    public string LastName { get; }
    //    public string FirstName { get; }

    //    public Person(string first, string last) => (FirstName, LastName) = (first, last);
    //}

    // forma reduzida
    public record Person(string FirstName, string LastName);

    // forma tradicional
    //public record Teacher : Person
    //{
    //    public string Subject { get; }

    //    public Teacher(string first, string last, string sub)
    //        : base(first, last) => Subject = sub;
    //}

    // forma reduzida
    public record Teacher(string FirstName, string LastName, string Subject)
            : Person(FirstName, LastName);

    // sealed para indicar que esse record não pode ser mais herdado por ninguém

    //forma tradiconal
    //public sealed record Student : Person
    //{
    //    public int Level { get; }

    //    public Student(string first, string last, int level) : base(first, last) => Level = level;
    //}

    // forma reduzida
    public sealed record Student(string FirstName, string LastName, int Level)
        : Person(FirstName, LastName);


    // exemplos com métodos
    public record Pet(string Name)
    {
        public void ShredTheFurniture() =>
            Console.WriteLine("Shredding furniture");
    }

    public record Dog(string Name) : Pet(Name)
    {
        public void WagTail() =>
            Console.WriteLine("It's tail wagging time");

        public override string ToString()
        {
            StringBuilder s = new();
            base.PrintMembers(s);

            return $"{s} is a dog";
        }
    }

    // structs com setters soment init (inicializa somente uma vez (imutável))
    public struct WeatherObservation
    {
        public DateTime RecordedAt { get; init; }
        public decimal TemperatureInCelsius { get; init; }
        public decimal PressureInMillibars { get; init; }

        public override string ToString() =>
            $"At {RecordedAt:h:mm tt} on {RecordedAt:M/d/yyyy}: " +
            $"Temp = {TemperatureInCelsius}, with {PressureInMillibars} pressure";
    }

    // classe para recursos de criação e finalização de objetos
    public class WeatherForecast { } 
}
