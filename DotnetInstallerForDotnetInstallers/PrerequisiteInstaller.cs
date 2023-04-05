using DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites;
using DotnetRuntimeBootstrapper.AppHost.Core.Utils;
using System;

namespace DotnetInstallerForDotnetInstallers
{
    public enum PrerequisiteInstallerStatus
    {
        NotStarted,
        Success,
        RebootRequired
    }

    public class PrerequisiteInstaller
    {
        IPrerequisiteInstaller _installer;

        internal PrerequisiteInstaller(IPrerequisiteInstaller installer)
        {
            _installer = installer;
            Result = PrerequisiteInstallerStatus.NotStarted;
        }

        public PrerequisiteInstallerStatus Result { get; internal set; }

        public PrerequisiteInstaller Run()
        {
            var result = _installer.Run();
            switch (result)
            {
                case PrerequisiteInstallerResult.Success:
                    Result = PrerequisiteInstallerStatus.Success;
                    break;
                case PrerequisiteInstallerResult.RebootRequired:
                    Result = PrerequisiteInstallerStatus.RebootRequired;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return this;
        }

        public PrerequisiteInstaller TryDelete()
        {
            FileEx.TryDelete(_installer.FilePath);
            return this;
        }

        public string FilePath => _installer.FilePath;

        public string DisplayName => _installer.Prerequisite.DisplayName;
    }
}
