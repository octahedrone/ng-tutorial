using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Playground;

public class YamlInvestigation
{
    [Fact]
    public void CheckYamlWithStringKeysIsDeserializedProperly()
    {
        var sourceString = @"
text: 'Do I want a Doughnut?'
options:
    ""No I don't"":
        text: 'May be I want an apple?'
    'Yes':
        text: 'Do i deserve it?'
        options:
            'No':
                text: 'Is it a good one?'
            'Yes':
                text: 'Are you sure?'
";
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var script = deserializer.Deserialize<AdventureScriptStep>(sourceString);

        script.Text.Should().Be("Do I want a Doughnut?");
        script.Options.Count.Should().Be(2);
        script.Options.Should().ContainKeys("No I don't", "Yes");
        script.Options["No I don't"].OrderNumber.Should().Be(0);
        script.Options["No I don't"].Text.Should().Be("May be I want an apple?");
        script.Options["Yes"].OrderNumber.Should().Be(1);
        script.Options["Yes"].Text.Should().Be("Do i deserve it?");
        script.Options["Yes"].Options.Count.Should().Be(2);
        script.Options["Yes"].Options.Should().ContainKeys("No", "Yes");
        script.Options["Yes"].Options["No"].Text.Should().Be("Is it a good one?");
        script.Options["Yes"].Options["Yes"].Text.Should().Be("Are you sure?");
    }
}

public class AdventureScriptStep
{
    public string Text { get; set; }
    public int OrderNumber { get; set; }
    public AdventureScriptStepsDictionary Options { get; set; }
}

public class AdventureScriptStepsDictionary : IDictionary<string, AdventureScriptStep>
{
    private readonly IDictionary<string, AdventureScriptStep> _innerDictionary = new Dictionary<string, AdventureScriptStep>();

    public IEnumerator<KeyValuePair<string, AdventureScriptStep>> GetEnumerator()
    {
        return _innerDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_innerDictionary).GetEnumerator();
    }

    public void Add(KeyValuePair<string, AdventureScriptStep> item)
    {
        item.Value.OrderNumber = Count;
        _innerDictionary.Add(item);
    }

    public void Clear()
    {
        _innerDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, AdventureScriptStep> item)
    {
        return _innerDictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, AdventureScriptStep>[] array, int arrayIndex)
    {
        _innerDictionary.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, AdventureScriptStep> item)
    {
        return _innerDictionary.Remove(item);
    }

    public int Count
    {
        get => _innerDictionary.Count;
    }

    public bool IsReadOnly
    {
        get => _innerDictionary.IsReadOnly;
    }

    public void Add(string key, AdventureScriptStep value)
    {
        value.OrderNumber = Count;
        _innerDictionary.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _innerDictionary.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _innerDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out AdventureScriptStep value)
    {
        return _innerDictionary.TryGetValue(key, out value);
    }

    public AdventureScriptStep this[string key]
    {
        get => _innerDictionary[key];
        set
        {
            value.OrderNumber = Count;
            _innerDictionary[key] = value;
        }
    }

    public ICollection<string> Keys => _innerDictionary.Keys;

    public ICollection<AdventureScriptStep> Values => _innerDictionary.Values;
}