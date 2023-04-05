namespace DotnetInstallerForDotnetInstallers.Prerequisites
{
    public interface IPrerequisite
    {
        string DisplayName { get; }

        bool IsInstalled();
    }
}
