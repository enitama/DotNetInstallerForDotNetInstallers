using System;
using System.Linq;
using System.Reflection;

namespace DotnetInstallerForDotnetInstallers.Prerequisites
{
    public enum KnownWindowsAppSDKs
    {
        [WindowsAppSDKVersionDetails("1.2.230313.1", "2000.802.31.0")]
        Version_1_2_5,
        [WindowsAppSDKVersionDetails("1.2.230217.4", "2000.777.2143.0")]
        Version_1_2_4,
    }

    internal class WindowsAppSDKVersionDetailsAttribute : Attribute
    {
        public string FullVersion { get; protected set; }
        public string PackageVersion { get; protected set; }

        public WindowsAppSDKVersionDetailsAttribute(string fullVersion, string packageVersion)
        {
            FullVersion = fullVersion;
            PackageVersion = packageVersion;
        }
    }

    public class WindowsAppSDKPrerequisite : IPrerequisite, IPrerequisiteInternal
    {
        Core.WindowsAppSDKPrerequisite _corePrerequisite;

        public WindowsAppSDKPrerequisite(KnownWindowsAppSDKs knownVersion)
        {
            var knownVersionDetails = typeof(KnownWindowsAppSDKs).GetMember(knownVersion.ToString()).First().GetCustomAttribute<WindowsAppSDKVersionDetailsAttribute>();
            _corePrerequisite = new Core.WindowsAppSDKPrerequisite(knownVersionDetails.FullVersion, knownVersionDetails.PackageVersion);
        }

        public WindowsAppSDKPrerequisite(string fullVersion, string packageVersion)
        {
            _corePrerequisite = new Core.WindowsAppSDKPrerequisite(fullVersion, packageVersion);
        }

        public DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites.IPrerequisite GetCorePrerequisite() => _corePrerequisite;

        public string DisplayName => _corePrerequisite.DisplayName;

        public bool IsInstalled() => _corePrerequisite.IsInstalled();
    }
}
