using DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites;
using DotnetRuntimeBootstrapper.AppHost.Core.Utils;
using System;

namespace DotnetInstallerForDotnetInstallers
{
    internal class WindowsAppSDKPrerequisiteInstaller : IPrerequisiteInstaller
    {
        WindowsAppSDKPrerequisite _prerequisite;

        public IPrerequisite Prerequisite => _prerequisite;
        public string FilePath { get; init; }

        public WindowsAppSDKPrerequisiteInstaller(WindowsAppSDKPrerequisite prerequisite, string filePath)
        {
            _prerequisite = prerequisite;
            FilePath = filePath;
        }

        public PrerequisiteInstallerResult Run()
        {
            var exitCode = CommandLine.Run(
                FilePath,
                // https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/deploy-unpackaged-apps#option-1-use-the-installer
                new[] { "--quiet" },
                true);

            if (exitCode != 0)
            {
                throw new ApplicationException(
                    $"Failed to install {Prerequisite.DisplayName}. " +
                    $"Exit code: {exitCode}. " +
                    "Please try to install this component manually."
                );
            }

            return PrerequisiteInstallerResult.Success;
        }
    }
}
