using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstudosCSharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            // readOnly struct
            var point = new Point
            {
                X = 1,
                Y = 2
            };

            Console.WriteLine($"-------- structs readOnly -------- {Environment.NewLine + point.ToString()}");
            Console.WriteLine(string.Empty);

            // property pattern
            var tax = ComputeSalesTax(new Address { State = "WA" }, 10m);
            Console.WriteLine($"-------- property pattern -------- {Environment.NewLine + tax}");
            Console.WriteLine(string.Empty);

            // tupple Pattern
            Console.WriteLine($"-------- tupple pattern -------- {Environment.NewLine + RockPaperScissors(string.Empty, "ronaldo")}");
            Console.WriteLine(Environment.NewLine + RockPaperScissors("rock", "paper"));
            Console.WriteLine(string.Empty);

            // positional patterns
            var quadrant = GetQuadrant(new Point2(-1, -4));
            Console.WriteLine($"-------- positional pattern -------- {Environment.NewLine + quadrant.ToString()}");
            Console.WriteLine(string.Empty);

            // local functions
            Console.WriteLine($"-------- local functions -------- {Environment.NewLine}");
            Console.WriteLine(LocalFunctionReturnInteger() + Environment.NewLine);
            Console.WriteLine(LocalFunctionAdd() + Environment.NewLine);

            int LocalFunctionReturnInteger()
            {
                int y;
                LocalFunction();
                return y;

                void LocalFunction() => y = 0;
            }

            int LocalFunctionAdd()
            {
                int y = 5;
                int x = 7;
                return Add(x, y);

                static int Add(int left, int right) => left + right;
            }

            // fluxos assíncronos
            Console.WriteLine($"-------- fluxos assincronos -------- {Environment.NewLine}");
            
            ForeachAssincrono().GetAwaiter();

            Console.WriteLine(string.Empty);

            // indíces e intervalos
            Console.WriteLine($"-------- indíces e intervalos -------- {Environment.NewLine}");

            var words = GetWords();
            Console.WriteLine($"the last word is {words[^1]}"); // writes dog

            var quickBrownFoxRange = words[1..4];
            Console.WriteLine($"the words quickBrownFoxRange is {string.Join(',', quickBrownFoxRange)}"); // writes quick, brown, fox

            var lazyDog = words[^2..^0];
            Console.WriteLine($"the words lazyDog is {string.Join(',', lazyDog)}"); // get two last words, lazy and dog

            var allWords = words[..]; // contains "The" through "dog".
            var firstPhrase = words[..4]; // contains "The" through "fox"
            var lastPhrase = words[6..]; // contains "the", "lazy" and "dog"

            Console.WriteLine($"all words {string.Join(',', allWords)}");
            Console.WriteLine($"firstPhrase words {string.Join(',', firstPhrase)}");
            Console.WriteLine($"lastPhrase words {string.Join(',', lastPhrase)}");

            Range phrase = 1..4;
            var textRange = words[phrase];
            Console.WriteLine($"textRange words 1 to 4 ( {string.Join(',', textRange)} )"); // get quick, brown, fox
            Console.WriteLine(string.Empty);

            /// null operators
            /// O C# 8,0 apresenta o operador de atribuição de União nula ??= . 
            /// Você pode usar o ??= operador para atribuir o valor do seu operando 
            /// à direita para seu operando à esquerda somente se o operando esquerdo 
            /// for avaliado como null .
            Console.WriteLine($"--------  null operators -------- {Environment.NewLine}");

            List<int> numbers = null;
            int? i = null;

            numbers ??= new List<int>();
            numbers.Add(i ??= 17);
            numbers.Add(i ??= 20);

            Console.WriteLine($"numbers: {string.Join(" ", numbers)}");  // output: 17 17
            Console.WriteLine(i);  // output: 17
            Console.WriteLine(string.Empty);

            // Tipos construídos não gerenciados (structs com generics)
            Console.WriteLine($"--------  Tipos construídos não gerenciados (structs com generics) -------- {Environment.NewLine}");

            var coord = new Coords<String>
            {
                X = "Ronaldo",
                Y = "Corinthians"
            };

            Console.WriteLine(coord.ToString());

            Span<Coords<int>> coordinates = stackalloc[]
            {
                new Coords<int> { X = 0, Y = 0 },
                new Coords<int> { X = 0, Y = 3 },
                new Coords<int> { X = 4, Y = 0 }
            };

            Console.WriteLine($"spanCoord1 = {coordinates[0]}");
            Console.WriteLine($"spanCoord2 = {coordinates[1]}");
            Console.WriteLine($"spanCoord3 = {coordinates[2]}");

            Console.WriteLine(string.Empty);

            /// Stackalloc em expressões aninhadas
            /// A partir do C# 8,0, se o resultado de uma expressão stackalloc 
            /// for do System.Span<T> System.ReadOnlySpan<T> tipo ou, 
            /// você poderá usar a stackalloc expressão em outras expressões
            Console.WriteLine($"--------  Stackalloc em expressões aninhadas -------- {Environment.NewLine}");

            Span<int> numbersStackAllock = stackalloc[] { 1, 2, 3, 4, 5, 6 };
            var indRange = numbersStackAllock.IndexOfAny(stackalloc[] { 2, 4, 6, 8 });
            var ind5Posicao4 = numbersStackAllock.IndexOfAny(stackalloc[] { 5 });

            Console.WriteLine("ind5 stackAlloc: " + ind5Posicao4);  // output: 4
            Console.WriteLine("indRange stackAlloc: " + indRange);  // output: 1
            Console.WriteLine(string.Empty);

            // Ordem da cadeias de interpolação de strings (@ e $)
            Console.WriteLine($"--------  Ordem da cadeias de interpolação de strings (@ e $) -------- {Environment.NewLine}");
            Console.WriteLine($@"Ordem normal - 
                $@ - ok");
            Console.WriteLine(@$"Ordem invertida - 
                @$ - ok");
        }

        private static async Task ForeachAssincrono()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }
        }

        // indíces e intervalos - GetWords
        private static string[] GetWords()
        {
            return new string[]
            {
                            // index from start    index from end
                "The",      // 0                   ^9
                "quick",    // 1                   ^8
                "brown",    // 2                   ^7
                "fox",      // 3                   ^6
                "jumped",   // 4                   ^5
                "over",     // 5                   ^4
                "the",      // 6                   ^3
                "lazy",     // 7                   ^2
                "dog"       // 8                   ^1
            };
        }

        // readonly struct point
        struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }
            public readonly double Distance => Math.Sqrt(X * X + Y * Y);

            public readonly override string ToString() =>
                $"({X}, {Y}) is {Distance} from the origin";
        }

        // property object pattern
        static decimal ComputeSalesTax(Address location, decimal salePrice) =>
            location switch
            {
                { State: "WA" } => salePrice * 0.06M,
                { State: "MN" } => salePrice * 0.075M,
                { State: "MI" } => salePrice * 0.05M,
                //  other cases removed for brevity...
                _ => 0M
            };

        // tupplePattern
        static string RockPaperScissors(string first, string second)
            => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };

        // positional patterns
        static Quadrant GetQuadrant(Point2 point) => point switch
        {
            (0, 0) => Quadrant.Origin,
            var (x, y) when x > 0 && y > 0 => Quadrant.One,
            var (x, y) when x < 0 && y > 0 => Quadrant.Two,
            var (x, y) when x < 0 && y < 0 => Quadrant.Three,
            var (x, y) when x > 0 && y < 0 => Quadrant.Four,
            var (_, _) => Quadrant.OnBorder,
            _ => Quadrant.Unknown
        };

        // fluxos assincronos
        static async IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(10);
                yield return i;
            }
        }
    }

    // class to property object pattern
    class Address
    {
        public string State;
    }

    // class to positional patterns
    class Point2
    {
        public int X { get; }
        public int Y { get; }

        public Point2(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) =>
            (x, y) = (X, Y);
    }

    enum Quadrant
    {
        Unknown,
        Origin,
        One,
        Two,
        Three,
        Four,
        OnBorder
    }

    // Tipos construídos não gerenciados (structs com generics)
    struct Coords<T>
    {
        public T X;
        public T Y;

        public override string ToString()
        {
            return $"X = {X}; Y = {Y}";
        }
    }
}
