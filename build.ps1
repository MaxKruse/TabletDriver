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
        [string] $configuration = $(if ($release) {'Release'} else {'Debug'})

        Write-Host "Current directory detected as $($cd)" -ForegroundColor Cyan
        
        [string] $build = Join-Path -Path $cd -ChildPath 'Build'
        $build = Join-Path -Path $build -ChildPath $configuration
        
        if (Test-Path $build) {
            Write-Host "Cleaning up directory '$($build)'..." -ForegroundColor Yellow
            Wipe $build
        }

        [string] $target = $(if ($rebuild) {'Rebuild'} else {'Build'})
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
        [string] $progFiles = ${env:ProgramFiles(x86)}
        [string] $vsBase = Join-Path -Path $progFiles -ChildPath '\Microsoft Visual Studio'

        if (Test-Path $(Join-Path -Path $vsBase -ChildPath '2019')) {
            $ver = '2019'
        }
        elseif (Test-Path $(Join-Path -Path $vsBase -ChildPath '2017')) {
            $ver = '2017'
        }
        else {
            Write-Host 'Failed to detect Visual Studio install. Defaulting to newest (VS2019), build will likely fail.' -ForegroundColor Red
            $ver = '2019'
        }
        Write-Host "MSBuild (Visual Studio $($ver))" -ForegroundColor Cyan
        $msb = "$($vsBase)\$($ver)\Community\MSBuild\Current\Bin\MSBuild.exe"
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
function Wipe {
    param (
        [parameter(Mandatory=$true)]
        [string] $directory
    )
    process {
        Get-ChildItem -Path $directory -Recurse | Remove-Item -Force -Recurse -ErrorAction SilentlyContinue
        Remove-Item $directory -Force -ErrorAction SilentlyContinue
    }
}

$r = Read-Host -Prompt "Build as release (y/n)"
BuildTabletDriver $($r -eq "y") $true