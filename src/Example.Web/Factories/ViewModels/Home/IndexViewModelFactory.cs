using Example.Web.Models.Database;
using Example.Web.ViewModels.Home;
using Adliance.Buddy.Crypto;
using Microsoft.EntityFrameworkCore;

namespace Example.Web.Factories.ViewModels.Home;

public class IndexViewModelFactory(ILogger<IndexViewModelFactory> logger, Db db)
{
    public IndexViewModel Create()
    {
        return new IndexViewModel();
    }

    public async Task<IndexViewModel> HandleRegistrationAsync(IndexViewModel viewModel)
    {
        var emailSalt = string.Empty;
        var emailHash = Crypto.HashV2(viewModel.EmailAddress, out emailSalt);

        var hashValues = (await db.Registrations.ToListAsync()).Select(r => (r.EmailHash, r.EmailHashSalt));
        if (hashValues.Any(r => Crypto.VerifyHashV2(viewModel.EmailAddress, r.EmailHash, r.EmailHashSalt)))
        {
            viewModel.ShowErrorMessage = true;
            logger.LogInformation($"Registration of {viewModel.FirstName} {viewModel.LastName} was denied. EmailAddress address is already in use.");
        }

        else
        {
            var registration = new Registration
            {
                EmailHash = emailHash,
                EmailHashSalt = emailSalt,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                CreatedUtc = DateTime.UtcNow
            };
            db.Registrations.Add(registration);
            await db.SaveChangesAsync();
            viewModel.ShowSuccessMessage = true;

            logger.LogInformation($"Registration of {viewModel.FirstName} {viewModel.LastName} stored in database with ID {registration.Id}.");
        }

        return viewModel;
    }
}
