using Slope.Models.UiEditors;
using System.Reflection;

namespace Slope.Attributes;

public class ObjectEditor<T> where T : class, new()
{
    public T Create()
    {
        var obj = new T();
        foreach (var property in typeof(T).GetProperties())
        {
            var editor = InputFieldFactory.Get(property.PropertyType);

            var caption = property.GetCustomAttribute<PromptAttribute>()?.Text ?? property.Name;

            var value = editor.Read(caption, null);
            property.SetValue(obj, value, null);
        }

        return obj;
    }

    public void Edit(T obj)
    {
        foreach (var property in typeof(T).GetProperties())
        {
            var editor = InputFieldFactory.Get(property.PropertyType);

            var caption = property.GetCustomAttribute<PromptAttribute>()?.Text ?? property.Name;
            var currentValue = property.GetValue(obj);

            var value = editor.Read(caption, currentValue);
            property.SetValue(obj, value, null);
        }
    }
}
