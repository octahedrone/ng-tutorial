using Application.ScriptEditor;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace YamlFormatter;

public static class YamlConverter
{
    public static string Serialize(ScriptEditorScriptStep root)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var serializable = new YamlAdventureScriptStep
        {
            Text = root.Text,
            Options = Convert(root.Options)
        };

        return serializer.Serialize(serializable);
    }

    private static YamlAdventureScriptStepsDictionary Convert(List<ScriptEditorScriptStep> options)
    {
        if (!(options is { Count: > 0 }))
            return null;

        var result = new YamlAdventureScriptStepsDictionary();
        
        options.ForEach(x => result.AddWithOrder(x.OptionText, new YamlAdventureScriptStep
        {
            Text = x.Text,
            OrderNumber = x.OrderNumber,
            Options = Convert(x.Options)
        }));

        return result;
    }

    public static ScriptEditorScriptStep Deserialize(string sourceString)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var root = deserializer.Deserialize<YamlAdventureScriptStep>(sourceString);

        return new ScriptEditorScriptStep
        {
            Text = root.Text,
            Options = root.Options is { Count: > 0 }
                ? root.Options.Select(x => Convert(x.Value, x.Key)).ToList()
                : null
        };
    }

    private static ScriptEditorScriptStep Convert(YamlAdventureScriptStep source, string optionText)
    {
        return new ScriptEditorScriptStep
        {
            Text = source.Text,
            OptionText = optionText,
            OrderNumber = source.OrderNumber,
            Options = source.Options is { Count: > 0 }
                ? source.Options.Select(x => Convert(x.Value, x.Key)).ToList()
                : null
        };
    }
}