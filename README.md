## Overview

A console application for configuring DNS mappings for the Sportik Desktop application.

## Release installation

To install the application, download the latest exe from the [Releases](https://github.com/Yaroslav-Yuriichuk/Sportik.DnsUtility/releases) page.
Run the executable as Administrator.

## Build

To build the application, you need to have the following tools installed:
- Windows SDK
- Visual Studio 2022 with following workloads:
  - .NET 9+ SDK

To build the application, follow these steps:
- Clone the repository:
  ```bash
  https://github.com/Yaroslav-Yuriichuk/Sportik.DnsUtility.git
- Open the solution file in IDE.
- Select the configuration you want to build.
- Set correct code signing certificate thumbprint in `Sportik.DnsUtility.csproj`.
- Click "Build" -> "Build Solution" to build the application or run directly in IDE.
