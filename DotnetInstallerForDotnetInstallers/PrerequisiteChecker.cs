using DotnetInstallerForDotnetInstallers.Prerequisites;
using System.Collections.Generic;
using System.Linq;

namespace DotnetInstallerForDotnetInstallers
{
    public class PrerequisiteChecker
    {
        IEnumerable<IPrerequisite> _prerequisites;

        public PrerequisiteChecker(params IPrerequisite[] prerequisites)
        {
            _prerequisites = prerequisites;
        }

        public IEnumerable<PrerequisiteDownloader> CheckPrerequisites()
        {
            return _prerequisites.Where(x => !x.IsInstalled()).Select(x => new PrerequisiteDownloader(x));
        }
    }
}
