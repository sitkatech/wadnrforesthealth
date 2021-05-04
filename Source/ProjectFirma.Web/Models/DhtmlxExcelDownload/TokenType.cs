namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public enum TokenType
    {

        //Non terminal tokens:
        _NONE_ = 0,
        _UNDETERMINED_ = 1,

        //Non terminal tokens:
        Start = 2,
        ComplexExpr = 3,
        Params = 4,
        ArrayElems = 5,
        FuncCall = 6,
        Array = 7,
        Range = 8,
        Expr = 9,

        //Terminal tokens:
        PARENOPEN = 10,
        PARENCLOSE = 11,
        BRACEOPEN = 12,
        BRACECLOSE = 13,
        COMMA = 14,
        COLON = 15,
        SEMICOLON = 16,
        FUNC = 17,
        ERR = 18,
        SHEETNAME = 19,
        ADDRESS = 20,
        NULL = 21,
        BOOL = 22,
        NUMBER = 23,
        STRING = 24,
        OP = 25,
        EOF = 26,
        WSPC = 27
    }
}