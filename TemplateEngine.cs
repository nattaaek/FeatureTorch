using System.Text;
using Models;

public class TemplateEngine 
{
    private readonly string _templatePath;

    public TemplateEngine(string templatePath) {
        _templatePath = templatePath;
    }

    public string GenerateHtml(Feature feature) {
        var templateContent = File.ReadAllText(_templatePath);

        templateContent = templateContent.Replace("##FEATURE_TITLE##", feature.Title);
        templateContent = templateContent.Replace("##SCENARIOS##", GenerateScenarioHtml(feature.Scenarios));

        return templateContent;
    }

    private string GenerateScenarioHtml(List<Scenario> scenarios) {
        var scenarioBlocks = new StringBuilder();

        foreach(var scenario in scenarios) 
        {
            scenarioBlocks.Append("<h2>" + scenario.Title + "</h2>");
            scenarioBlocks.Append("<ul>");

            foreach(var step in scenario.Steps) {
                scenarioBlocks.Append("<li>" + step + "</li>"); 
            }

            scenarioBlocks.Append("</ul>");
        }

        return scenarioBlocks.ToString();
    }
}
