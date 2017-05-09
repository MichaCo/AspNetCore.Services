@echo off
cd %~dp0
CALL Consul_Install.cmd
@Consul\consul.exe agent --dev -log-level=info -datacenter eu-west