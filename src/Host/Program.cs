

using Application.Installers;
using Domain.Installers;
using Infrastructure.Installers;

var app = await ApplicationInstaller.InstallApplicationAsync(args, options =>
{
    options.InstallDomain();
    options.InstallApplication();
    options.InstallInfrastructure();
});

await ApplicationInstaller.RunAsync(app);
