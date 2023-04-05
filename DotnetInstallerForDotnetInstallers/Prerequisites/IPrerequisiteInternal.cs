namespace DotnetInstallerForDotnetInstallers.Prerequisites
{
    internal interface IPrerequisiteInternal : IPrerequisite
    {
        DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites.IPrerequisite GetCorePrerequisite();
    }
}
