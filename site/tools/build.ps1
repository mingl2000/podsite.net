#!/usr/bin/env pwsh
<#
.SYNOPSIS
Build script for the Modern Podcast static site generator

.DESCRIPTION
Compiles and runs the static site generator to produce output in site/output
#>

param(
    [switch]$Clean,
    [switch]$Help
)

if ($Help) {
    Write-Host "Usage: ./build.ps1 [-Clean]"
    Write-Host "Options:"
    Write-Host "  -Clean    Remove output directory before building"
    Write-Host "  -Help     Show this help message"
    exit 0
}

$ErrorActionPreference = 'Stop'

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$SiteRoot = Split-Path -Parent $ScriptDir
$OutputDir = Join-Path $SiteRoot 'output'

Write-Host "Building Modern Podcast static site..." -ForegroundColor Cyan

if ($Clean -and (Test-Path $OutputDir)) {
    Write-Host "Cleaning output directory..." -ForegroundColor Yellow
    Remove-Item -Path $OutputDir -Recurse -Force
}

# Ensure output directory exists
New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null

# Build and run the generator
Write-Host "Building static site generator..." -ForegroundColor Cyan
dotnet build $ScriptDir -c Release --nologo --verbosity minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host "Running generator..." -ForegroundColor Cyan
$GeneratorExe = Join-Path $ScriptDir 'bin/Release/net8.0/GenerateStaticSite.exe'
& $GeneratorExe

if ($LASTEXITCODE -ne 0) {
    Write-Host "Generator failed!" -ForegroundColor Red
    exit 1
}

Write-Host "Build complete! Output in: $OutputDir" -ForegroundColor Green
