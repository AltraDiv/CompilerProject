using System;

namespace compiler
{
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(">");
            var line = Console.ReadLine();
            var lexer = new Lexer(line);
            while (true) {
                var token = lexer.NextToken();
                if (token.Kind == Syntaxkind.EndOfFile) {
                    break;
                }
                Console.Write(token.Kind + ":  " + token.Text);
                if (token.Value != null) {
                    Console.Write(", " + token.Value);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}

