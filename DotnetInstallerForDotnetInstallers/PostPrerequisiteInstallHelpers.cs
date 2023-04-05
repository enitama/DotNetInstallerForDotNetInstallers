using DotnetRuntimeBootstrapper.AppHost.Core.Platform;

namespace DotnetInstallerForDotnetInstallers
{
    public class PostPrerequisiteInstallHelpers
    {
        public static void Reboot() => OperatingSystemEx.Reboot();
    }
}
