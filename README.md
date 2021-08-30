# WakeOnLan
[![.NET Core Desktop](https://github.com/Dai0526/WakeOnLan/actions/workflows/dotnet-desktop.yml/badge.svg?branch=main)](https://github.com/Dai0526/WakeOnLan/actions/workflows/dotnet-desktop.yml)


## Description
A GUI applicaiton for IT and network admin to monitor computer/server status, and turn on PC remotly.

### Configuration File
Application will read an xml file called `WoL.xml` to load computer infos. The xml structure is as follow:  

```xml
<?xml version="1.0" encoding="utf-8"?>
<Computers>
    <Node id = "DeveloperPC1" ip = "192.168.0.1" mac = "11-22-33-44-55-66" description = "Tom's Computer"/>
    <Node id = "BuildPC"  ip = "192.168.0.2" mac = "11-11-22-FF-FF-FF" description = "Nightly Build Machine"/>
</Computers>

```
