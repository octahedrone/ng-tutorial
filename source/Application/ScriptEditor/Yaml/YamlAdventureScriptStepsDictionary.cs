using System.Collections;

namespace YamlFormatter;

public class YamlAdventureScriptStepsDictionary : IDictionary<string, YamlAdventureScriptStep>
{
    private readonly IDictionary<string, YamlAdventureScriptStep> _innerDictionary = new Dictionary<string, YamlAdventureScriptStep>();

    public IEnumerator<KeyValuePair<string, YamlAdventureScriptStep>> GetEnumerator()
    {
        return _innerDictionary.OrderBy(x => x.Value.OrderNumber).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_innerDictionary.OrderBy(x => x.Value.OrderNumber)).GetEnumerator();
    }

    public void Add(KeyValuePair<string, YamlAdventureScriptStep> item)
    {
        item.Value.OrderNumber = Count;
        _innerDictionary.Add(item);
    }

    public void Clear()
    {
        _innerDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, YamlAdventureScriptStep> item)
    {
        return _innerDictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, YamlAdventureScriptStep>[] array, int arrayIndex)
    {
        _innerDictionary.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, YamlAdventureScriptStep> item)
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

    public void Add(string key, YamlAdventureScriptStep value)
    {
        value.OrderNumber = Count;
        _innerDictionary.Add(key, value);
    }

    public void AddWithOrder(string key, YamlAdventureScriptStep value)
    {
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

    public bool TryGetValue(string key, out YamlAdventureScriptStep value)
    {
        return _innerDictionary.TryGetValue(key, out value);
    }

    public YamlAdventureScriptStep this[string key]
    {
        get => _innerDictionary[key];
        set
        {
            value.OrderNumber = Count;
            _innerDictionary[key] = value;
        }
    }

    public ICollection<string> Keys => _innerDictionary.Keys;

    public ICollection<YamlAdventureScriptStep> Values => _innerDictionary.Values;
}