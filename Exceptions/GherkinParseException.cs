using System;

namespace Exceptions;

public class GherkinParseException(string message) : Exception(message) 
{
}