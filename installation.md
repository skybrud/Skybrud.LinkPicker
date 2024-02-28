---
order: 1
---

# Installation

## NuGet package

This is really the recommended method for installing packages. When using the NuGet package format, references to the correct DLLs are automatically set up in your Visual Studio project, and NuGet will take care of installing any dependencies as well. You can install this package in Visual Studio in the package manager console:

<div class="highlight nuget"><pre><span>PM&gt;&nbsp;</span>Install-Package Skybrud.LinkPicker</pre></div>

Alternatively you can [**see the package on the NuGet website**](https://www.nuget.org/packages/Skybrud.LinkPicker).

## Umbraco package

To install the Umbraco package, you should first [**download the package file from Our Umbraco**](https://our.umbraco.com/packages/backoffice-extensions/skybrudlinkpicker/), and then install it in your Umbraco installation via the **Packages** section.

When installing as an Umbraco package, the files will be copied to the right location inside your Umbraco installation, but you should add references to the included DLLs manually should you need to reference classes and similar within the package.

## ZIP archive

As a third option you can [**download a ZIP file from GitHub**](https://github.com/skybrud/Skybrud.LinkPicker/releases). To install the package from the ZIP file, unzip it and move the contents to the root directory of your web application. Similar to the Umbraco package, you will have to add references to the DLLs manually.