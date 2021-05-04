namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class Token
    {
        private int startpos;
        private int endpos;
        private string text;
        private object value;

        public int StartPos
        {
            get { return startpos; }
            set { startpos = value; }
        }

        public int Length
        {
            get { return endpos - startpos; }
        }

        public int EndPos
        {
            get { return endpos; }
            set { endpos = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public TokenType Type;

        public Token()
            : this(0, 0)
        {
        }

        public Token(int start, int end)
        {
            Type = TokenType._UNDETERMINED_;
            startpos = start;
            endpos = end;
            Text = ""; // must initialize with empty string, may cause null reference exceptions otherwise
            Value = null;
        }

        public void UpdateRange(Token token)
        {
            if (token.StartPos < startpos) startpos = token.StartPos;
            if (token.EndPos > endpos) endpos = token.EndPos;
        }

        public override string ToString()
        {
            if (Text != null)
                return Type.ToString() + " '" + Text + "'";
            else
                return Type.ToString();
        }
    }
}