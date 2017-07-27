namespace IronLuaCompiler.LuaObjects
{

    internal class LuaExpression : LuaObject
    {
        internal override string GenerateIl()
        {
            return "";
        }
    }

    internal class LuaAssignmentExpression : LuaExpression
    {
        public LuaAssignmentExpression(string name, LuaObject assignee)
        {
            Name = name;
            Assignee = assignee;
        }
        internal string Name { get; }
        internal LuaObject Assignee { get; }
    }

}