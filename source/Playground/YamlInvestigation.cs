using FluentAssertions;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlFormatter;

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

        var script = deserializer.Deserialize<YamlAdventureScriptStep>(sourceString);

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