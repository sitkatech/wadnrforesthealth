namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public partial class Parser
    {
        private Scanner scanner;
        private ParseTree tree;

        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
        }

        public ParseTree Parse(string input)
        {
            tree = new ParseTree();
            return Parse(input, tree);
        }

        public ParseTree Parse(string input, ParseTree tree)
        {
            scanner.Init(input);

            this.tree = tree;
            ParseStart(tree);
            tree.Skipped = scanner.Skipped;

            return tree;
        }

        private void ParseStart(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Start), "Start");
            parent.Nodes.Add(node);



            ParseComplexExpr(node);


            tok = scanner.Scan(TokenType.EOF);
            if (tok.Type != TokenType.EOF)
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EOF.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
            n = node.CreateNode(tok, tok.ToString());
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseComplexExpr(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.ComplexExpr), "ComplexExpr");
            parent.Nodes.Add(node);



            ParseExpr(node);


            tok = scanner.LookAhead(TokenType.OP);
            while (tok.Type == TokenType.OP)
            {


                tok = scanner.Scan(TokenType.OP);
                if (tok.Type != TokenType.OP)
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OP.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                n = node.CreateNode(tok, tok.ToString());
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);


                ParseExpr(node);
                tok = scanner.LookAhead(TokenType.OP);
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseParams(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Params), "Params");
            parent.Nodes.Add(node);



            tok = scanner.LookAhead(TokenType.NULL, TokenType.BOOL, TokenType.NUMBER, TokenType.STRING, TokenType.PARENOPEN, TokenType.BRACEOPEN, TokenType.SHEETNAME, TokenType.ADDRESS, TokenType.ERR, TokenType.FUNC);
            if (tok.Type == TokenType.NULL
                || tok.Type == TokenType.BOOL
                || tok.Type == TokenType.NUMBER
                || tok.Type == TokenType.STRING
                || tok.Type == TokenType.PARENOPEN
                || tok.Type == TokenType.BRACEOPEN
                || tok.Type == TokenType.SHEETNAME
                || tok.Type == TokenType.ADDRESS
                || tok.Type == TokenType.ERR
                || tok.Type == TokenType.FUNC)
            {
                ParseComplexExpr(node);
            }


            tok = scanner.LookAhead(TokenType.COMMA);
            while (tok.Type == TokenType.COMMA)
            {


                tok = scanner.Scan(TokenType.COMMA);
                if (tok.Type != TokenType.COMMA)
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.COMMA.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                n = node.CreateNode(tok, tok.ToString());
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);


                ParseComplexExpr(node);
                tok = scanner.LookAhead(TokenType.COMMA);
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseArrayElems(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.ArrayElems), "ArrayElems");
            parent.Nodes.Add(node);



            tok = scanner.LookAhead(TokenType.NULL, TokenType.BOOL, TokenType.NUMBER, TokenType.STRING, TokenType.PARENOPEN, TokenType.BRACEOPEN, TokenType.SHEETNAME, TokenType.ADDRESS, TokenType.ERR, TokenType.FUNC);
            if (tok.Type == TokenType.NULL
                || tok.Type == TokenType.BOOL
                || tok.Type == TokenType.NUMBER
                || tok.Type == TokenType.STRING
                || tok.Type == TokenType.PARENOPEN
                || tok.Type == TokenType.BRACEOPEN
                || tok.Type == TokenType.SHEETNAME
                || tok.Type == TokenType.ADDRESS
                || tok.Type == TokenType.ERR
                || tok.Type == TokenType.FUNC)
            {
                ParseComplexExpr(node);
            }


            tok = scanner.LookAhead(TokenType.COMMA, TokenType.SEMICOLON);
            while (tok.Type == TokenType.COMMA
                || tok.Type == TokenType.SEMICOLON)
            {


                tok = scanner.LookAhead(TokenType.COMMA, TokenType.SEMICOLON);
                switch (tok.Type)
                {
                    case TokenType.COMMA:
                        tok = scanner.Scan(TokenType.COMMA);
                        if (tok.Type != TokenType.COMMA)
                            tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.COMMA.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        n = node.CreateNode(tok, tok.ToString());
                        node.Token.UpdateRange(tok);
                        node.Nodes.Add(n);
                        break;
                    case TokenType.SEMICOLON:
                        tok = scanner.Scan(TokenType.SEMICOLON);
                        if (tok.Type != TokenType.SEMICOLON)
                            tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SEMICOLON.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        n = node.CreateNode(tok, tok.ToString());
                        node.Token.UpdateRange(tok);
                        node.Nodes.Add(n);
                        break;
                    default:
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                        break;
                }


                ParseComplexExpr(node);
                tok = scanner.LookAhead(TokenType.COMMA, TokenType.SEMICOLON);
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseFuncCall(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.FuncCall), "FuncCall");
            parent.Nodes.Add(node);



            tok = scanner.Scan(TokenType.FUNC);
            if (tok.Type != TokenType.FUNC)
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.FUNC.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
            n = node.CreateNode(tok, tok.ToString());
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);


            tok = scanner.Scan(TokenType.PARENOPEN);
            if (tok.Type != TokenType.PARENOPEN)
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PARENOPEN.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
            n = node.CreateNode(tok, tok.ToString());
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);


            tok = scanner.LookAhead(TokenType.NULL, TokenType.BOOL, TokenType.NUMBER, TokenType.STRING, TokenType.PARENOPEN, TokenType.BRACEOPEN, TokenType.SHEETNAME, TokenType.ADDRESS, TokenType.ERR, TokenType.FUNC, TokenType.COMMA);
            if (tok.Type == TokenType.NULL
                || tok.Type == TokenType.BOOL
                || tok.Type == TokenType.NUMBER
                || tok.Type == TokenType.STRING
                || tok.Type == TokenType.PARENOPEN
                || tok.Type == TokenType.BRACEOPEN
                || tok.Type == TokenType.SHEETNAME
                || tok.Type == TokenType.ADDRESS
                || tok.Type == TokenType.ERR
                || tok.Type == TokenType.FUNC
                || tok.Type == TokenType.COMMA)
            {
                ParseParams(node);
            }


            tok = scanner.Scan(TokenType.PARENCLOSE);
            if (tok.Type != TokenType.PARENCLOSE)
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PARENCLOSE.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
            n = node.CreateNode(tok, tok.ToString());
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseArray(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Array), "Array");
            parent.Nodes.Add(node);



            tok = scanner.Scan(TokenType.BRACEOPEN);
            if (tok.Type != TokenType.BRACEOPEN)
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BRACEOPEN.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
            n = node.CreateNode(tok, tok.ToString());
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);


            ParseArrayElems(node);


            tok = scanner.Scan(TokenType.BRACECLOSE);
            if (tok.Type != TokenType.BRACECLOSE)
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BRACECLOSE.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
            n = node.CreateNode(tok, tok.ToString());
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseRange(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Range), "Range");
            parent.Nodes.Add(node);



            tok = scanner.LookAhead(TokenType.SHEETNAME);
            if (tok.Type == TokenType.SHEETNAME)
            {
                tok = scanner.Scan(TokenType.SHEETNAME);
                if (tok.Type != TokenType.SHEETNAME)
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SHEETNAME.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                n = node.CreateNode(tok, tok.ToString());
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
            }


            tok = scanner.LookAhead(TokenType.ADDRESS, TokenType.ERR);
            switch (tok.Type)
            {
                case TokenType.ADDRESS:
                    tok = scanner.Scan(TokenType.ADDRESS);
                    if (tok.Type != TokenType.ADDRESS)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ADDRESS.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                case TokenType.ERR:
                    tok = scanner.Scan(TokenType.ERR);
                    if (tok.Type != TokenType.ERR)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ERR.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                    break;
            }


            tok = scanner.LookAhead(TokenType.COLON);
            if (tok.Type == TokenType.COLON)
            {


                tok = scanner.Scan(TokenType.COLON);
                if (tok.Type != TokenType.COLON)
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.COLON.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                n = node.CreateNode(tok, tok.ToString());
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);


                tok = scanner.LookAhead(TokenType.ADDRESS, TokenType.ERR);
                switch (tok.Type)
                {
                    case TokenType.ADDRESS:
                        tok = scanner.Scan(TokenType.ADDRESS);
                        if (tok.Type != TokenType.ADDRESS)
                            tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ADDRESS.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        n = node.CreateNode(tok, tok.ToString());
                        node.Token.UpdateRange(tok);
                        node.Nodes.Add(n);
                        break;
                    case TokenType.ERR:
                        tok = scanner.Scan(TokenType.ERR);
                        if (tok.Type != TokenType.ERR)
                            tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ERR.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        n = node.CreateNode(tok, tok.ToString());
                        node.Token.UpdateRange(tok);
                        node.Nodes.Add(n);
                        break;
                    default:
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                        break;
                }
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseExpr(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Expr), "Expr");
            parent.Nodes.Add(node);

            tok = scanner.LookAhead(TokenType.NULL, TokenType.BOOL, TokenType.NUMBER, TokenType.STRING, TokenType.PARENOPEN, TokenType.BRACEOPEN, TokenType.SHEETNAME, TokenType.ADDRESS, TokenType.ERR, TokenType.FUNC);
            switch (tok.Type)
            {
                case TokenType.NULL:
                    tok = scanner.Scan(TokenType.NULL);
                    if (tok.Type != TokenType.NULL)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.NULL.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                case TokenType.BOOL:
                    tok = scanner.Scan(TokenType.BOOL);
                    if (tok.Type != TokenType.BOOL)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BOOL.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                case TokenType.NUMBER:
                    tok = scanner.Scan(TokenType.NUMBER);
                    if (tok.Type != TokenType.NUMBER)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.NUMBER.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                case TokenType.STRING:
                    tok = scanner.Scan(TokenType.STRING);
                    if (tok.Type != TokenType.STRING)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.STRING.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                case TokenType.PARENOPEN:


                    tok = scanner.Scan(TokenType.PARENOPEN);
                    if (tok.Type != TokenType.PARENOPEN)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PARENOPEN.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);


                    ParseExpr(node);


                    tok = scanner.Scan(TokenType.PARENCLOSE);
                    if (tok.Type != TokenType.PARENCLOSE)
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PARENCLOSE.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                    n = node.CreateNode(tok, tok.ToString());
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    break;
                case TokenType.BRACEOPEN:
                    ParseArray(node);
                    break;
                case TokenType.SHEETNAME:
                case TokenType.ADDRESS:
                case TokenType.ERR:
                    ParseRange(node);
                    break;
                case TokenType.FUNC:
                    ParseFuncCall(node);
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                    break;
            }

            parent.Token.UpdateRange(node.Token);
        }


    }
}