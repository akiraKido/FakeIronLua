using System.Collections.Generic;
using System.Linq;

namespace IronLuaCompiler.LuaObjects
{

    internal class LuaFunction : LuaObject
    {
        internal string Name { get; }
        internal IEnumerable<LuaObject> Parameters { get; }
        internal IEnumerable<LuaObject> Expressions { get; }
        
        public LuaFunction(string name, IEnumerable<LuaObject> parameters, IEnumerable<LuaObject> expressions)
        {
            Name = name;
            Parameters = parameters;
            Expressions = expressions;
        }
        
        internal override string GenerateIl()
        {
            return $"{Name}-{Parameters.FirstOrDefault()?.GenerateIl()}-{Expressions.FirstOrDefault()?.GenerateIl()}";
        }
    }

}