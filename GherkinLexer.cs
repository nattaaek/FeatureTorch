using System.Text.RegularExpressions;
using Enums;
using Models;

public class GherkinLexer {
    private string _input;
    private int _position;

    public GherkinLexer(string input) {
        _input = input;
        _position = 0;
    }

    public Token GetNextToken() {
        if (_position >= _input.Length) return new Token { Type = TokenType.EOF };

        if (_input[_position..].StartsWith("Feature:")) { 
            return new Token { Type = TokenType.Feature };
        } else if (_input[_position..].StartsWith("Scenario:")) {
            return new Token { Type = TokenType.Scenario }; 
        }  else if (Regex.IsMatch(_input[_position..], @"^(Given|When|Then):", RegexOptions.IgnoreCase)) {
            var lineEndIndex = _input.IndexOfAny(['\r', '\n'], _position);
            var match = Regex.Match(_input[_position..lineEndIndex], @"^(Given|When|Then):", RegexOptions.IgnoreCase);

            _position += match.Length; 
            return new Token { Type = GetStepTokenType(match.Groups[1].Value), Text = match.Value };
        } 

        _position++; 
        return GetNextToken(); 
    }

    private static TokenType GetStepTokenType(string keyword) {
        return keyword.ToLowerInvariant() switch
        {
            "given" => TokenType.Given,
            "when" => TokenType.When,
            "then" => TokenType.Then,
            _ => throw new ArgumentException("Invalid step keyword"),
        };
    }
}