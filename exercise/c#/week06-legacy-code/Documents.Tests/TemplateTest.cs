namespace Documents.Tests;

public class TemplateTest
{
    [Fact]
    public Task Test_WhenSuccess()
    {
        Template[] templates =
        [
            Template.FindTemplateFor("DEER", "IndividualProspect"),
            Template.FindTemplateFor("DEER", "LegalProspect"),
            Template.FindTemplateFor("AUTP", "IndividualProspect"),
            Template.FindTemplateFor("AUTM", "LegalProspect"),
            Template.FindTemplateFor("SPEC", "All"),
            Template.FindTemplateFor("GLPP", "IndividualProspect"),
            Template.FindTemplateFor("GLPM", "LegalProspect")
        ];
        return Verify(templates);
    }

    [Fact]
    public void Test_WhenFailure()
    {
        var act = () 
            => Template.FindTemplateFor("FAIL", "Certainly");
        Assert.Throws<ArgumentException>(act);
    }
}
