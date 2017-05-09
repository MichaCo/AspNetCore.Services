@echo off
cd %~dp0

SETLOCAL
SET CACHED=Consul\consul.zip

IF EXIST %CACHED% goto :run
echo Downloading Consul
IF NOT EXIST @Consul md Consul
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12;Invoke-WebRequest 'http://releases.hashicorp.com/consul/0.8.1/consul_0.8.1_windows_amd64.zip' -OutFile '%CACHED%'"

@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Expand-Archive %CACHED%"

:run

