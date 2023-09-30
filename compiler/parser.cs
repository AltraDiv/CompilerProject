using System;
using System.Security.Principal;

namespace compiler {
    class Parser {
        SyntaxToken[] arroftokens;
        int position;
        SyntaxToken current => Peek(0);
        public Parser(string text) {
            var lexer = new Lexer(text);
            var loftokens = new List<SyntaxToken>();
            SyntaxToken token;
            do {
                token = lexer.NextToken();
                if (token.Kind != Syntaxkind.Bad && token.Kind != Syntaxkind.Whitespace) {
                    loftokens.Add(token);
                }
                token = lexer.NextToken();
            } while (token.Kind != Syntaxkind.EndOfFile);
            arroftokens = loftokens.ToArray();
        }

        SyntaxToken Peek(int offset) {
            var index = position + offset;
            int len = arroftokens.Length;
            if (index >= len) {
                return arroftokens[len - 1];
            }
            return arroftokens[index];
        }
    }
    abstract class Expression {
        public abstract Syntaxkind kind {get;}
        public abstract void print();
        public abstract SyntaxToken eval();
        public abstract void set_var(string name, int num);
        public abstract void unset_var(string name);
    }

    class Lone_Int : Expression{
        SyntaxToken num;
        public Lone_Int(SyntaxToken num){
            this.num = num;
        }
        public override SyntaxToken eval() {return num;}
        public override void print() {Console.WriteLine(num);}
        public override void set_var(string name, int num){}
        public override void unset_var(string name) {}
    }

    class Binary : Expression{
        Expression rhs;
        Expression lhs;
        SyntaxToken func;
        public Binary(Expression rhs, Expression lhs, SyntaxToken func) {
            this.rhs = rhs;
            this.lhs = lhs;
            this.func = func;
        }
        public override void set_var(string name, int num) {
            lhs.set_var(name, num);
            rhs.set_var(name, num);
        }
        public override void unset_var(string name) {
            lhs.unset_var(name);
            rhs.unset_var(name);
        }
        public override int eval() {
            if (func == "+") {
                return lhs.eval() + rhs.eval();
            }
            else if (func == "-") {
                return lhs.eval() - rhs.eval();
            }
            else if (func == "/") {
                return lhs.eval() / rhs.eval();
            }
            else if (func == "*") {
                return lhs.eval() * rhs.eval();
            }
            return 0;
        }
        public override void print() {
            if (func == "+") {
                Console.WriteLine("(");
                lhs.print();
                Console.WriteLine(" + ");
                rhs.print();
                Console.WriteLine(")");
            }
            else if (func == "-") {
                Console.WriteLine("(");
                lhs.print();
                Console.WriteLine(" - ");
                rhs.print();
                Console.WriteLine(")");
            }
            else if (func == "/") {
                Console.WriteLine("(");
                lhs.print();
                Console.WriteLine(" / ");
                rhs.print();
                Console.WriteLine(")");
            }
            else if (func == "*") {
                Console.WriteLine("(");
                lhs.print();
                Console.WriteLine(" * ");
                rhs.print();
                Console.WriteLine(")");
            }
        }
    }
    
    class Unary : Expression {
        bool neg;
        bool abs;
        Expression lhs;
        Unary(bool neg, bool abs, Expression lhs) {
            this.neg = neg;
            this.abs = abs;
            this.lhs = lhs;
        }
        public override void set_var(string name, int num) {
            lhs.set_var(name, num);
        }
        public override void unset_var(string name) {
            lhs.unset_var(name);
        }
        public override int eval() {
            if (neg) {
                return lhs.eval() * -1;
            }
            if (abs) {
                if (lhs.eval() < 0) {
                    return lhs.eval() * -1;
                }
            }
            return lhs.eval();
        }
        public override void print() {
            if (neg) {
                Console.WriteLine("-");
                lhs.print();
            }
            if (abs) {
                Console.WriteLine("|");
                lhs.print();
                Console.WriteLine("|");
            }
            
        }
    }
    class Variables : Expression {
        string var;
        int value;
        bool set;
        Variables(string var, int value, bool set) {
            this.var = var;
            this.value = value;
            this.set = set;
        }
        public override void set_var(string name, int num) {
            if (var == name) {
                value = num;
                set = true;
            }
        }
        public override void unset_var(string name) {
            if (name == var) {
                set = false;
            }
        }
        public override int eval() {
            if (set) {
                return value;
            }
            else {
                throw new Exception();
            }
            return 0;
        }

        public override void print() {
            if (set) {
                Console.WriteLine(value);
            }
            else {
                Console.WriteLine(var);
            }
        }
    }
}



