@echo off
cd /d %~dp0
dotnet restore
dotnet build -c Release

cmd /C start RunWebsite.cmd
cmd /C start Consul_Run_Dev.cmd
timeout /T 5
cmd /C start RunService5200.cmd
timeout /T 5
cmd /C start RunService5300.cmd
