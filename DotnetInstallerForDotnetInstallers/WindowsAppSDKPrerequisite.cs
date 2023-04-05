using DotnetRuntimeBootstrapper.AppHost.Core.Platform;
using DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites;
using DotnetRuntimeBootstrapper.AppHost.Core.Utils;
using System;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace DotnetInstallerForDotnetInstallers
{
    internal class WindowsAppSDKPrerequisite : IPrerequisite
    {
        private string _displayVersion;
        private string _packageVersionString;
        private PackageVersion _packageVersion;

        public string DisplayName => $"Windows App SDK {_displayVersion} (";

        public WindowsAppSDKPrerequisite(string displayVersion, string packageVersion)
        {
            _displayVersion = displayVersion;
            _packageVersionString = packageVersion;
            var parsedVersion = new Version(packageVersion);
            _packageVersion = new PackageVersion { Major = (ushort)parsedVersion.Major, Minor = (ushort)parsedVersion.Minor, Build = (ushort)parsedVersion.Build, Revision = (ushort)parsedVersion.Revision };
        }

        public IPrerequisiteInstaller DownloadInstaller(Action<double>? handleProgress)
        {
            var downloadUrl = $"https://aka.ms/windowsappsdk/1.2/latest/windowsappruntimeinstall-{OperatingSystemEx.ProcessorArchitecture.GetMoniker()}.exe";
            var filePath = FileEx.GenerateTempFilePath(Url.TryExtractFileName(downloadUrl) ?? "installer.exe");

            Http.DownloadFile(downloadUrl, filePath, handleProgress);

            return new WindowsAppSDKPrerequisiteInstaller(this, filePath);
        }

        public bool IsInstalled()
        {
            // TODO: Use the actual processor architecture.
            // TODO: Do we need to check for package type other than framework?
            return IsPackageRegisteredForCurrentUser("Microsoft.WindowsAppRuntime.1.2_8wekyb3d8bbwe", _packageVersion, Windows.System.ProcessorArchitecture.X64, PackageTypes.Framework);
        }

        // Below code from https://github.com/microsoft/WindowsAppSDK/discussions/2437#discussioncomment-2679118
        public static bool IsPackageRegisteredForCurrentUser(string packageFamilyName, PackageVersion minVersion, Windows.System.ProcessorArchitecture architecture, PackageTypes packageType)
        {
            ulong minPackageVersion = ToVersion(minVersion);

            foreach (var p in new PackageManager().FindPackagesForUserWithPackageTypes(string.Empty, packageFamilyName, packageType))
            {
                // Is the package architecture compatible?
                if (p.Id.Architecture != architecture)
                {
                    continue;
                }

                // Is the package version sufficient for our needs?
                ulong packageVersion = ToVersion(p.Id.Version);
                if (packageVersion < minPackageVersion)
                {
                    continue;
                }

                // Success!
                return true;
            }

            // No qualifying package found
            return false;
        }

        private static ulong ToVersion(PackageVersion packageVersion)
        {
            return ((ulong)packageVersion.Major << 48) |
                   ((ulong)packageVersion.Minor << 32) |
                   ((ulong)packageVersion.Build << 16) |
                   ((ulong)packageVersion.Revision);
        }
    }
}
