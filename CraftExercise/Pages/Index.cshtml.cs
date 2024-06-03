using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CraftExercise.Models;
using CraftExercise.Services;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    private readonly GitHubService _gitHubService;
    private readonly FreshdeskService _freshdeskService;

    public IndexModel(GitHubService gitHubService, FreshdeskService freshdeskService)
    {
        _gitHubService = gitHubService;
        _freshdeskService = freshdeskService;
    }

    [BindProperty]
    public string GitHubUsername { get; set; }

    [BindProperty]
    public string FreshdeskSubdomain { get; set; }

    public string Message { get; set; }

    public async Task<IActionResult>
    OnPost()
    {
        try
        {
            var githubUser = await _gitHubService.GetGitHubUser(GitHubUsername);

            var freshdeskContact = new FreshdeskContact
            {
                Name = githubUser.Name,
                Email = githubUser.Email
            };

            var freshdeskApiKey = Environment.GetEnvironmentVariable("FRESHDESK_TOKEN");
            var result = await _freshdeskService.CreateOrUpdateContact(freshdeskContact, FreshdeskSubdomain, freshdeskApiKey);

            Message = result ? "Contact created/updated successfully!" : "Failed to create/update contact.";
        }
        catch (Exception ex)
        {
            Message = $"Error: {ex.Message}";
        }

        return Page();
    }
}
