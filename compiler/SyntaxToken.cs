namespace compiler {
    enum Syntaxkind {
        Number,
        Whitespace,
        Plus,
        Minus,
        Star,
        Slash,
        OpenParenthesis,
        CloseParenthesis,
        Bad,
        EndOfFile
    }
    class SyntaxToken {
        public SyntaxToken(Syntaxkind kind, int position, string text, object value) {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
        public Syntaxkind Kind { get;}
        public int Position { get; }
        public string Text {get;}
        public object Value {get;}
    }
}