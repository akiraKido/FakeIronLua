using System.Linq;
using IronLuaCompiler.LuaObjects;
using Revn.DotParse;
using static Revn.DotParse.Parsers.StringParsers;

namespace IronLuaCompiler
{
    public class IronLuaParser
    {
        private static readonly Parser<string, string> Identifier
            = Regex("[a-zA-Z][a-zA-Z0-9]*");

        private static readonly Parser<string, LuaObject> LuaNumber
            = Regex("[0-9|.]+")
                .Map(n => new LuaNumber(decimal.Parse(n)) as LuaObject);

        private static readonly Parser<string, LuaObject> LuaString
            = Regex("\".*\"")
                .Map(s => new LuaString(s.Substring(1, s.Length - 2)) as LuaObject);

        private static readonly Parser<string, LuaObject> Literal
            = LuaNumber.Or(LuaString);

        private static readonly Parser<string, LuaObject> LuaAssignmentExpression
            = Identifier
                .SeqT(Char('=')).Map(item => item.a)
                .SeqT(Literal)
                .Map(item => new LuaAssignmentExpression(item.a, item.b) as LuaObject);

        private static readonly Parser<string, LuaObject> Expression
            = LuaAssignmentExpression;

        private static readonly Parser<string, LuaObject[]> TrailingParameter
            = Char(',').SeqT(Identifier).Map(item => new LuaParameter(item.b) as LuaObject).Any();

        private static readonly Parser<string, LuaObject[]> Parameter
            = Identifier
                .Map(item => new LuaParameter(item) as LuaObject).CanFail()
                .SeqT(TrailingParameter)
                .Map(item => new[] {item.a}.Concat(item.b).ToArray());

        private static readonly Parser<string, LuaFunction[]> LuaFunction
            = Skip("function")
                .SeqT(Identifier)        .Map(item => item.b)
                .SeqT(Char('('))         .Map(item => item.a)
                .SeqT(Parameter)         .Map(item => new {Name = item.a, Identifiers = item.b})
                .SeqT(Char(')'))         .Map(item => item.a)
                .SeqT(Expression.Many()) .Map(item => new {item.a.Name, item.a.Identifiers, Expressions = item.b})
                .SeqT(Match("end"))      .Map(item => item.a)
                .Map(item => new[] {new LuaFunction(item.Name, item.Identifiers, item.Expressions)});

        public static string Parse(string s)
        {
            var hoge = (LuaFunction(new LuaCodeSource(s)) as Success<string, LuaFunction[]>)?.Value.First();
            return hoge?.GenerateIl();
        }
    }

}