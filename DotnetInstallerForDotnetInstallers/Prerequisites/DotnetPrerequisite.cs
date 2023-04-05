using DotnetRuntimeBootstrapper.AppHost.Core.Dotnet;
using DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites;
using System;
using System.Collections.Generic;

namespace DotnetInstallerForDotnetInstallers.Prerequisites
{
    public enum KnownDotnetPrerequisiteType
    {
        AspNetCore,
        NETCore,
        WindowsDesktop,
    }

    public class DotnetPrerequisite : IPrerequisite, IPrerequisiteInternal
    {
        private static readonly Dictionary<KnownDotnetPrerequisiteType, string> PrerequisiteStrings = new Dictionary<KnownDotnetPrerequisiteType, string>
        {
            { KnownDotnetPrerequisiteType.AspNetCore, "Microsoft.AspNetCore.App" },
            { KnownDotnetPrerequisiteType.NETCore, "Microsoft.NETCore.App" },
            { KnownDotnetPrerequisiteType.WindowsDesktop, "Microsoft.WindowsDesktop.App" },
        };

        DotnetRuntimePrerequisite _corePrerequisite;

        public DotnetPrerequisite(KnownDotnetPrerequisiteType type, Version version)
        {
            _corePrerequisite = new DotnetRuntimePrerequisite(new DotnetRuntime(PrerequisiteStrings[type], version));
        }

        public DotnetPrerequisite(string type, Version version)
        {
            _corePrerequisite = new DotnetRuntimePrerequisite(new DotnetRuntime(type, version));
        }

        public DotnetRuntimeBootstrapper.AppHost.Core.Prerequisites.IPrerequisite GetCorePrerequisite() => _corePrerequisite;

        public string DisplayName => _corePrerequisite.DisplayName;

        public bool IsInstalled() => _corePrerequisite.IsInstalled();
    }
}
