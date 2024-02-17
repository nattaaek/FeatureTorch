using Enums;
using Exceptions;
using Models;

public class GherkinParser
{
    private GherkinLexer _lexer;

    public GherkinParser(string input)
    {
        _lexer = new GherkinLexer(input);
    }

    public Feature ParseFeature() {
        var feature = new Feature();

        ConsumeToken(TokenType.Feature);
        feature.Title = ConsumeFeatureTitle();

        feature.Scenarios.Add(ParseScenario()); 

        ConsumeToken(TokenType.EOF); 
        return feature;
    }

    private Scenario ParseScenario() {
        var scenario = new Scenario();

        ConsumeToken(TokenType.Scenario); 
        scenario.Title = ConsumeScenarioTitle(); 

        ConsumeToken(TokenType.Given); 
        scenario.Steps.Add(ConsumeStepText()); 

        return scenario; 
    }

    private void ConsumeToken(TokenType expectedType) {
        var token = _lexer.GetNextToken();
        if (token.Type != expectedType) {
            throw new Exception($"Unexpected token {token.Type}, expected {expectedType}");
        }
    }

    private string? ConsumeFeatureTitle() {
        var token = _lexer.GetNextToken() ?? throw new GherkinParseException("Unexpected end of input or invalid feature format.");
        return token.Text; 
    } 

    private string? ConsumeScenarioTitle() {
        var token = _lexer.GetNextToken() ?? throw new GherkinParseException("Unexpected end of input or invalid scenario format."); // Get the next token from the lexer 
        return token.Text; 
    }

    private string ConsumeStepText() {
        var token = _lexer.GetNextToken() ?? throw new GherkinParseException("Unexpected end of input or invalid step format.");
        return token.Text ?? ""; 
    }
}
