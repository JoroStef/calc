namespace Slope.Editors.UiEditors;

public static class InputFieldFactory
{
    public static IInputField Get(Type type)
    {
        if (type == typeof(string))
            return StringField.Instance;

        if (type == typeof(double))
            return DoubleField.Instance;

        //if (type == typeof(int))
        //    return IntField.Instance;

        //if (type.IsEnum)
        //    return EnumField.Instance;

        throw new NotSupportedException(type.Name);
    }
}
