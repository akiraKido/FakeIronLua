using System.Collections.Generic;
using Revn.DotParse;

namespace IronLuaCompiler
{

    internal static class CombinatorExtensions
    {
        internal static Parser<TSource, TResult> CanFail<TSource, TResult>(this Parser<TSource, TResult> parser)
            => source =>
            {
                var parseResult = parser(source);
                return parseResult.IsFailed
                    ? parseResult.Source.ToSuccess(default(TResult))
                    : parseResult;
            };

        internal static Parser<TSource, TResult[]> Any<TSource, TResult>(this Parser<TSource, TResult> parser)
            => source =>
            {
                var parseResult = parser(source);
                if (parseResult.IsFailed)
                {
                    return source.ToSuccess(new TResult[] { });
                }

                var results = new List<TResult>();
                while (parseResult.IsSuccess)
                {
                    results.Add(parseResult.Value);
                    parseResult = parser(parseResult.Source);
                }
                return parseResult.Source.ToSuccess(results.ToArray());
            };

        internal static ParseResult<TSource, TResult> ToSuccess<TSource, TResult>(this ISource<TSource> source, TResult value) 
            => new Success<TSource, TResult>(value, source);
    }

}