using DotnetInstallerForDotnetInstallers.Prerequisites;
using System;

namespace DotnetInstallerForDotnetInstallers
{
    public class PrerequisiteDownloader
    {
        IPrerequisite _prerequisite;

        internal PrerequisiteDownloader(IPrerequisite prerequisite)
        {
            _prerequisite = prerequisite;
        }

        public PrerequisiteInstaller Download(Action<PrerequisiteDownloader, double>? handleProgress)
        {
            return new PrerequisiteInstaller(
                ((IPrerequisiteInternal)_prerequisite).GetCorePrerequisite().DownloadInstaller(handleProgress != null ? progress => handleProgress(this, progress) : null));
        }

        public string DisplayName => _prerequisite.DisplayName;
    }
}
