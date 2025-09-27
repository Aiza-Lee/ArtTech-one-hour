[Setup]
AppName=ArtTech One Hour
AppVersion=1.0
AppPublisher=ArtTech Studio
DefaultDirName={autopf}\ArtTechOneHour
DefaultGroupName=ArtTech One Hour
OutputDir=.\installer
OutputBaseFilename=ArtTechOneHourSetup
Compression=lzma
SolidCompression=yes
SetupIconFile=Icon.ico
UninstallDisplayIcon={app}\ArtTech-one-hour.exe
VersionInfoVersion=1.0.0.0
VersionInfoCompany=Nightingale Studio
VersionInfoDescription=ArtTech One Hour Game
VersionInfoCopyright=Copyright 2025 ArtTech Studio
PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
WizardStyle=modern
WizardSizePercent=120

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "license_en.txt"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "C:\FILES\Projects\Unity\ArtTech-one-hour\Build\*"; DestDir: "{app}"; Flags: recursesubdirs createallsubdirs

[Icons]
Name: "{group}\ArtTech One Hour"; Filename: "{app}\ArtTech-one-hour.exe"
Name: "{group}\Uninstall ArtTech One Hour"; Filename: "{uninstallexe}"
Name: "{commondesktop}\ArtTech One Hour"; Filename: "{app}\ArtTech-one-hour.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\ArtTech-one-hour.exe"; Description: "Run ArtTech One Hour"; Flags: nowait postinstall skipifsilent

[Code]
function InitializeUninstall(): Boolean;
begin
  Result := MsgBox('Are you sure you want to uninstall ArtTech One Hour?', mbConfirmation, MB_YESNO) = IDYES;
end;
