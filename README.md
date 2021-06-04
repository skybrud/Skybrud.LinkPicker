Skybrud.LinkPicker
==================

[Looking for the Umbraco 7 version of the package?](https://github.com/skybrud/Skybrud.LinkPicker/tree/dev-v7)

**Skybrud.LinkPicker** is a package for Umbraco 8 that introduces a property editor for adding or editing a single link. Together with [**Skybrud.Umbraco.Elements**](https://github.com/skybrud/Skybrud.Umbraco.Elements) or similar packages, it may also be used to create lists of links.











Skybrud.LinkPicker is a small package that adds a single property editor to Umbraco that can be used for picking a link - either being content, media or a custom URL.

![image](https://user-images.githubusercontent.com/3634580/120788631-d6eeac00-c530-11eb-9ac0-7e1d02f97f0a.png)
*The destination property shows the link picker editor in use. When clicking to add or edit a link, the editor utilizes Umbraco's build-in link picker overlay.*

## Installation

1. [**NuGet Package**][NuGetPackage]  
Install this NuGet package in your Visual Studio project. Makes updating easy.

<!--1. [**Umbraco package**][UmbracoPackage]  
Install the package through the Umbraco backoffice.-->

1. [**ZIP file**][GitHubRelease]  
Grab a ZIP file of the latest release; unzip and move the contents to the root directory of your web application.

[NuGetPackage]: https://www.nuget.org/packages/Skybrud.LinkPicker
[UmbracoPackage]: https://our.umbraco.org/projects/backoffice-extensions/skybrudlinkpicker/
[GitHubRelease]: https://github.com/skybrud/Skybrud.LinkPicker/releases

## Skybrud.Umbraco.Elements

If you've used the Umbraco 7 version of this package, you may recall that it featured a property editor for managing a list of links rather that a property editor for a single link as in the Umbraco 8.

With the link list property editor, you could add all the links you wanted, but each item in the list would just be a link without the support for any additional properties. As Umbraco 7 matured, and Umbraco 8 came closer, we released this was limiting our capatabilities for building better websites - eg. a link could for instance include extra properties for improving the accessbility of a website, but this wasn't possible with the old link list property editor.

When we're building link lists in Umbraco 8, we're now instead relying on our own [**Skybrud.Umbraco.Elements**](https://github.com/skybrud/Skybrud.Umbraco.Elements) package. The package then allows developers to set up properties and grid elements based on content types and `IPublishedElement`.

![link](https://user-images.githubusercontent.com/3634580/85072079-c70f7800-b1b8-11ea-98ae-b65f3741c5ac.gif)

In the GIF above, the link item has an additonal property for the link text, but other properties - like an icon - could make sense as well.

To use the link list view provided by this package, you can set the **View** option to `/App_Plugins/Skybrud.LinkPicker/Views/Partials/Links.html` when creating your link list data type:

![image](https://user-images.githubusercontent.com/3634580/85072388-59b01700-b1b9-11ea-8158-182e189af9d7.png)

## Umbraco Block List

The new Block List in Umbraco uses the same principles as our Elements editor described above. Ideally this package will also provide a custom view for the Block List in the future, making it more friendly to create and manage a link list with the block list.




