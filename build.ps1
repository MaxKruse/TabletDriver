function BuildTabletDriver {
    param (
        [parameter(Mandatory=$true)]
        [bool] $release,

        [parameter(Mandatory=$false)]
        [bool] $rebuild = $true
    )
    process {
        [string] $cdRaw = Get-Location
        [string] $cd = ($cdRaw).Replace('Microsoft.PowerShell.Core\FileSystem::', '')
        [string] $sln = Join-Path -Path $cd -ChildPath 'TabletDriver.sln'
        [string] $build = Join-Path -Path $cd -ChildPath 'Build'
        [string] $configuration = $(if ($release) {'Release'} else {'Debug'})

        Write-Host "Current directory detected as $($cd)" -ForegroundColor Yellow

        if ($release) {
            $build += '\Release'
        }
        else {
            $build += '\Debug'
        }

        [string] $target = $(if ($rebuild) {'Rebuild'} else {'Build'})
        [string] $configuration = $(if ($release) {'Release'} else {'Debug'})
        Write-Host "$($target)ing $($configuration) for '$($cd)'..." -ForegroundColor Green
        MSBuild $sln $target $configuration

        # File system operations
        XCopy "$($cd)\TabletDriverGUI\bin\$($configuration)\*" "$($build)\*"
        XCopy "$($cd)\TabletDriverService\bin\$($configuration)\*" "$($build)\bin\*"
        XCopy "$($cd)\VMulti Installer GUI\bin\$($configuration)\*" "$($build)\bin\*"
        XCopy "$($cd)\TabletDriverService\config\*" "$($build)\config\*"
        XCopy "$($cd)\TabletDriverService\tools\*" "$($build)\tools\*"
        7Zip "$($build)\*" "$($build)\build.zip"
    }
}
function MSBuild {
    param (
        [parameter(Mandatory=$true)]
        [string] $path,

        [parameter(Mandatory=$true)]
        [string] $target,

        [parameter(Mandatory=$true)]
        [string] $configuration
    )
    process {
        $msb = 'C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe'
        & $msb /t:$target /p:Configuration=$configuration /m
    }
}
function XCopy {
    param (
        [parameter(Mandatory=$true)]
        [string] $source,

        [parameter(Mandatory=$true)]
        [string] $target
    )
    process {
        & xcopy.exe /C /Y $source $target
    }
}
function 7Zip {
    param (
        [parameter(Mandatory=$true)]
        [string] $source,

        [parameter(Mandatory=$true)]
        [string] $target
    )
    process {
        if (Test-Path "$($env:ProgramFiles)\7-Zip\7z.exe" -PathType Leaf) {
            & "$($env:ProgramFiles)\7-Zip\7z.exe" a -tzip $target $source
        }
        else {
            Write-Host '7-Zip is not installed, unable to compress build files.'
        }
    }
}

$r = Read-Host -Prompt "Build as release (y/n)"
BuildTabletDriver $($r -eq "y") $true