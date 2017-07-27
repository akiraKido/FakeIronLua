namespace IronLuaCompiler.LuaObjects
{

    internal class LuaParameter : LuaObject
    {
        public LuaParameter(string name)
        {
            Name = name;
        }

        internal string Name { get; }

        internal override string GenerateIl()
        {
            return Name;
        }
    }

}