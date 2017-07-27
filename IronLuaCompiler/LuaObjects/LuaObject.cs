namespace IronLuaCompiler.LuaObjects
{

    internal abstract class LuaObject
    {
        internal abstract string GenerateIl();
    }

    internal class LuaNumber : LuaObject
    {
        internal LuaNumber(decimal value)
        {
            Value = value;
        }

        internal decimal Value { get; }

        internal override string GenerateIl()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class LuaString : LuaObject
    {
        internal LuaString(string value)
        {
            Value = value;
        }

        internal string Value { get; }

        internal override string GenerateIl()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class LuaVariable : LuaObject
    {
        internal LuaVariable(string name)
        {
            Name = name;
        }

        internal string Name { get; }

        internal override string GenerateIl()
        {
            throw new System.NotImplementedException();
        }
    }

}