# Capreze

[日本語で見る](README.ja-jp.md)

A window size auto-adjust tool for pixel-perfect captures

[![.github/workflows/trigger-on-main.yml](https://github.com/karamem0/capreze/actions/workflows/trigger-on-main.yml/badge.svg)](https://github.com/karamem0/capreze/actions/workflows/trigger-on-main.yml)
[![License](https://img.shields.io/github/license/karamem0/capreze.svg)](https://github.com/karamem0/capreze/blob/main/LICENSE)

## Screenshot

![Screenshot](./assets/Screenshot.png)

## Features

This tool automatically detects the difference (offset) between the displayed window size and the actual image size to improve capture accuracy, allowing you to capture at the exact specified size. In multi-monitor environments, corrections can be applied for each monitor on which the target window appears.

- Automatic detection of window and captured image border difference (offset)
- Multi-monitor support
- Assistance for precise size setting at exact pixel units

## Dependencies

- [.NET Core 8.0](https://dotnet.microsoft.com/download/dotnet-core/8.0)
- [Microsoft.ApplicationInsights.WorkerService](https://www.nuget.org/packages/Microsoft.ApplicationInsights.WorkerService/2.22.0) (2.22.0)
- [Microsoft.Extensions.Configuration.Json](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/8.0.0) (8.0.0)
- [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/8.0.0) (8.0.0)
- [Microsoft.Extensions.Hosting](https://www.nuget.org/packages/Microsoft.Extensions.Hosting/8.0.0) (8.0.0)
- [Microsoft.Xaml.Behaviors.Wpf](https://www.nuget.org/packages/Microsoft.Xaml.Behaviors.Wpf/1.1.122) (1.1.122)
- [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/8.0.6) (8.0.6)
- [TinyMapper](https://www.nuget.org/packages/TinyMapper/3.0.3) (3.0.3)
