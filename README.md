# ASP.NET Core app using Azure App Configuration as source of feature flag state

You can click the button below to deploy to your Azure subscription:

- App Configuration, Free tier
- App Service plan, Shared tier
- Web App

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fjuunas11%2FJoonasw.AzureAppConfigurationFeatureFlags%2Fmaster%2FARM%2Fazuredeploy.json)

After deploying the resources, you can publish the app to the Web App.

Then you need to find the new App Configuration resource in Azure Portal and add two features from the **Feature Manager** tab:

- FeatureA
- FeatureB

You can set their state to On or Off.

The application should be able to access the configuration store and load the feature flags.
It will also refresh them every 30 minutes (this is adjustable in Program.cs).