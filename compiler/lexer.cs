namespace compiler {
    class Lexer {
        int position;
        string text;
        char current {
            get {
                if (position >= text.Length) {
                    return '\0';
                }
                return text[position];
            }
        }
        public Lexer(string text) {
            this.text = text;
        }
        private void Next() {
            ++position;
        }
        public SyntaxToken NextToken() {
            if (position >= text.Length) {
                return new SyntaxToken(Syntaxkind.EndOfFile, position, "\0", null);
            }
            if (char.IsWhiteSpace(current)) {
                var start = position;
                while (char.IsWhiteSpace(current)) {
                    Next();
                }
                var length = position - start;
                var subtext = text.Substring(start, length);
                return new SyntaxToken(Syntaxkind.Whitespace, start, subtext, null);
            }
            if (char.IsDigit(current)) {
                var start = position;
                while (char.IsDigit(current)) {
                    Next();
                }
                var length = position - start;
                var subtext = text.Substring(start, length);
                int.TryParse(subtext, out var value);
                return new SyntaxToken(Syntaxkind.Number, start, subtext, value);
            }

            if (current == '+'){
                return new SyntaxToken (Syntaxkind.Plus, position++, "+", null);
            }
            else if (current == '-') {
                return new SyntaxToken(Syntaxkind.Minus, position++, "-", null);
            }
            else if (current == '*') {
                return new SyntaxToken(Syntaxkind. Star, position++, "*", null);
            }
            else if (current == '/') {
                return new SyntaxToken(Syntaxkind.Slash, position++, "/", null);
            }
            else if (current == '(') {
                return new SyntaxToken(Syntaxkind. OpenParenthesis, position++, "(", null);
            }
            else if (current == ')') {
                return new SyntaxToken(Syntaxkind.CloseParenthesis, position++, ")", null);
            }
            else {
                return new SyntaxToken(Syntaxkind.Bad, position++, text.Substring(position - 1, 1), null);
            }
        }
    }
}