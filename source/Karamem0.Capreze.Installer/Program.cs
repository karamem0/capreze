//
// Copyright (c) 2019-2026 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/main/LICENSE
//

using System.Diagnostics;
using System.Text;
using WixSharp;

WixExtension.UI.PreferredVersion = "5.0.2";

var project = new Project("Capreze", new InstallDir(@"%LocalAppData%\Programs\Capreze", new Files(@"..\..\artifact\capreze\*.*")))
{
    ControlPanelInfo = new ProductInfo()
    {
        Manufacturer = "karamem0"
    },
    Encoding = Encoding.UTF8,
    GUID = new Guid("e5e36352-6460-4916-bfba-7a13d69aa501"),
    Scope = InstallScope.perUser,
    LicenceFile = @".\LICENSE.rtf",
    MajorUpgrade = new MajorUpgrade()
    {
        AllowDowngrades = true
    },
    Version = new Version(
        FileVersionInfo.GetVersionInfo(typeof(Program).Assembly.Location)
            .FileVersion ??
        "0.0.0.0"
    )
};

var executable = project
    .ResolveWildCards()
    .FindFile(f => f.Name.EndsWith("Capreze.exe", StringComparison.OrdinalIgnoreCase) || f.Name.EndsWith("Capreze.dll", StringComparison.OrdinalIgnoreCase))
    .FirstOrDefault();
executable?.Shortcuts =
[
    new FileShortcut("Capreze", @"%ProgramMenu%")
];

_ = Compiler.BuildMsi(project, @"..\..\artifact\capreze_installer\capreze.msi");
