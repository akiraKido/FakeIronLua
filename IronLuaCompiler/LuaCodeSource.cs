using System;
using System.Collections.Generic;
using System.Linq;
using Revn.DotParse;

namespace IronLuaCompiler
{

    internal class LuaCodeSource : StringLineSourceBase
    {
        private readonly IReadOnlyList<string> _lines;

        private readonly int _line;
        private readonly int _pos;

        public LuaCodeSource(string text)
        {
            _lines = text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
            _line = 0;
            _pos = 0;
        }

        private LuaCodeSource(IReadOnlyList<string> lines, int line, int pos)
        {
            _lines = lines;
            _line = line;
            _pos = pos;
        }

        public override string Peek()
        {
            return _line < _lines.Count ? _lines[_line].Substring(_pos) : null;
        }

        public override ISource<string> ToNext()
        {
            return new LuaCodeSource(_lines, _line + 1, 0);
        }

        /// <summary>
        /// 指定した文字数分先にすすめます
        /// </summary>
        /// <param name="count">なん文字消費したか</param>
        /// <returns></returns>
        public override ISource<string> ToNext(int count)
        {
            int lineIndex = _line;
            string line = _lines[_line];
            int index = _pos + count;

            while (true)
            {
                if (index >= line.Length)
                {
                    index = 0;
                    if (++lineIndex >= _lines.Count) break;
                    line = _lines[lineIndex];
                }
                if (!char.IsWhiteSpace(line[index])) break;
                index++;
            }
            return new LuaCodeSource(_lines, lineIndex, index);
        }
    }

}