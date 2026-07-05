using Slope.Editors.UiEditors;
using Slope.Metadata.Attributes;
using System.Reflection;

namespace Slope.Editors;

public class ObjectEditor<T> where T : new()
{
    public T Create()
    {
        var obj = new T();
        foreach (var property in typeof(T).GetProperties())
        {
            var editor = InputFieldFactory.Get(property.PropertyType);

            var caption = 
                property.GetCustomAttribute<PromptAttribute>()?.Text ??
                property.GetCustomAttribute<TableFieldAttribute>()?.Caption ?? 
                property.Name;

            var value = editor.Read(caption, null);
            property.SetValue(obj, value, null);
        }

        return obj;
    }

    public void Edit(T obj)
    {
        if (obj == null)
        {
            return;
        }

        foreach (var property in typeof(T).GetProperties())
        {
            var editor = InputFieldFactory.Get(property.PropertyType);

            var caption =
                property.GetCustomAttribute<PromptAttribute>()?.Text ??
                property.GetCustomAttribute<TableFieldAttribute>()?.Caption ??
                property.Name;

            var currentValue = property.GetValue(obj);

            var value = editor.Read(caption, currentValue);
            property.SetValue(obj, value, null);
        }
    }
}
