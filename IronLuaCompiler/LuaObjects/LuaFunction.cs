using System.Collections.Generic;
using System.Linq;

namespace IronLuaCompiler.LuaObjects
{

    internal class LuaFunction : LuaObject
    {
        internal string Name { get; }
        internal IEnumerable<LuaParameter> Parameters { get; }
        internal IEnumerable<LuaExpression> Expressions { get; }
        
        public LuaFunction(string name, IEnumerable<LuaObject> parameters, IEnumerable<LuaObject> expressions)
        {
            Name = name;
            Parameters = parameters as IEnumerable<LuaParameter>;
            Expressions = expressions as IEnumerable<LuaExpression>;
        }
        
        internal override string GenerateIl()
        {
            return $"{Name}-{Parameters.FirstOrDefault().GenerateIl()}-{Expressions.FirstOrDefault().GenerateIl()}";
        }
    }

}