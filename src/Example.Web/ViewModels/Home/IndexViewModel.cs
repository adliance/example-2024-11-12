using System.ComponentModel.DataAnnotations;

namespace Example.Web.ViewModels.Home;

public class IndexViewModel
{
    [Required] public string EmailAddress { get; set; } = "";
    [Required] public string FirstName { get; set; } = "";
    [Required] public string LastName { get; set; } = "";
    public bool ShowSuccessMessage { get; set; }
    public bool ShowErrorMessage { get; set; }
}
