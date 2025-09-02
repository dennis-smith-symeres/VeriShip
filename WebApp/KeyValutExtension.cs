using System.Security.Cryptography.X509Certificates;
using Azure.Identity;

namespace VeriShip.WebApp;

public static class KeyVaultExtension
{
    public static WebApplicationBuilder AddkeyVault(this WebApplicationBuilder builder)
    {
        var certificateThumbprint = builder.Configuration["AzureAd:CertificateThumbprint"];
        var isDevelopment = builder.Environment.IsDevelopment() 
                            || builder.Environment.EnvironmentName == "Testing" ;
        if (certificateThumbprint == null) return builder;
        
        var x509Certificate = GetX509Certificate(isDevelopment,certificateThumbprint);

        builder.Configuration.AddAzureKeyVault(
            new Uri($"https://{builder.Configuration["AzureAd:KeyVaultName"]}.vault.azure.net/"),
            new ClientCertificateCredential(
                builder.Configuration["AzureAd:TenantId"],
                builder.Configuration["AzureAd:ClientId"],
                x509Certificate
            )
        );

        return builder;
    }

    private static X509Certificate2 GetX509Certificate(bool useLocationCurrentUser, string thumbprint)
    {
        var storeLocation = useLocationCurrentUser
            ? StoreLocation.CurrentUser
            : StoreLocation.LocalMachine;

        using var x509Store = new X509Store(storeLocation);
        x509Store.Open(OpenFlags.ReadOnly);

        return x509Store.Certificates
            .Find(
                X509FindType.FindByThumbprint,
                thumbprint,
                false)
            .OfType<X509Certificate2>()
            .Single();
    }
}