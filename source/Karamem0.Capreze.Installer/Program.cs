//
// Copyright (c) 2019-2025 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WixSharp;

namespace Karamem0.Capreze
{

    public static class Program
    {

        private static void Main()
        {
            var project = new Project("Capreze", new InstallDir(@"%LocalAppData%\Programs\Capreze", new Files(@"..\..\artifact\capreze\*.*")))
            {
                ControlPanelInfo = new ProductInfo()
                {
                    Manufacturer = "karamem0"
                },
                Encoding = Encoding.UTF8,
                GUID = new Guid("e5e36352-6460-4916-bfba-7a13d69aa501"),
                InstallPrivileges = InstallPrivileges.limited,
                InstallScope = InstallScope.perUser,
                LicenceFile = @".\LICENSE.rtf",
                MajorUpgrade = new MajorUpgrade()
                {
                    AllowDowngrades = true
                },
                Version = new Version(
                    FileVersionInfo.GetVersionInfo(
                            System.Reflection.Assembly.GetEntryAssembly()
                                .Location
                        )
                        .FileVersion
                )
            };
            project
                .ResolveWildCards()
                .FindFile(f => f.Name.EndsWith("Capreze.exe"))
                .First()
                .Shortcuts = new[]
            {
                new FileShortcut("Capreze", @"%ProgramMenu%")
            };
            _ = Compiler.BuildMsi(project, @"..\..\artifact\capreze.msi");
        }

    }

}
