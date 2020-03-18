using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.FeatureManagement;

namespace Joonasw.AzureAppConfigurationFeatureFlags.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFeatureManager _featureManager;

        public IndexModel(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public bool FeatureAEnabled { get; set; }
        public bool FeatureBEnabled { get; set; }

        public async Task OnGetAsync()
        {
            FeatureAEnabled = await _featureManager.IsEnabledAsync("FeatureA");
            FeatureBEnabled = await _featureManager.IsEnabledAsync("FeatureB");
        }
    }
}
