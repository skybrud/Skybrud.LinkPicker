Skybrud.LinkPicker
==================

Skybrud.LinkPicker is a small package that adds a single property editor to Umbraco 7 that can be used as either a single link picker or as a multi link picker depending on configuration.

<!--Besides working like most other link pickers, it can also be configured to show a detailed list of the selected links (eg. as the table in the screenshot below).

![Screenshot of Skybrud.LinkPicker](https://cloud.githubusercontent.com/assets/3634580/9728573/2ab5caf0-5609-11e5-87e8-d7585378107e.png)-->

## Links

- <a href="#installation">Installation</a>
- <a href="#using-the-property-editor">Using the property editor</a>
- <a href="#using-the-grid-editor">Using the grid editor</a>
- <a href="#using-the-link-picker-in-your-own-projects">Using the link picker in your own projects</a>

## Installation

1. [**NuGet Package**][NuGetPackage]  
Install this NuGet package in your Visual Studio project. Makes updating easy.

1. [**Umbraco package**][UmbracoPackage]  
Install the package through the Umbraco backoffice.

1. [**ZIP file**][GitHubRelease]  
Grab a ZIP file of the latest release; unzip and move the contents to the root directory of your web application.

[NuGetPackage]: https://www.nuget.org/packages/Skybrud.LinkPicker
[UmbracoPackage]: https://our.umbraco.org/projects/backoffice-extensions/skybrudlinkpicker/
[GitHubRelease]: https://github.com/skybrud/Skybrud.LinkPicker/releases

## Using the property editor

The link picker (both single and multi) saves a list of link picker items. Technically the single link picker is a multi link picker, but limited to one item.

The link picker can be configured to be shown as either a standard unordered list (default):

![image](https://cloud.githubusercontent.com/assets/3634580/16986321/1da83912-4e86-11e6-9177-a6f72dfd43e7.png)

Or as a table:

![image](https://cloud.githubusercontent.com/assets/3634580/16986288/e8cdbbae-4e85-11e6-976d-2c2d8994b5b9.png)

To get a list of link picker items, you could simply use `GetPropertyValue` as you're used to:

```C#
LinkPickerList linkPickerList1 = Model.GetPropertyValue("multiLinkPicker") as LinkPickerList;
```

or using generics:


```C#
LinkPickerList linkPickerList2 = Model.GetPropertyValue<LinkPickerList>("multiLinkPicker");
```

The package also comes with two extension methods - one for getting the list of link picker items like above:

```C#
LinkPickerList linkPickerList3 = Model.GetLinkPickerList("multiLinkPicker");
```

or for getting a single link picker item:

```C#
LinkPickerItem linkPickerItem1 = Model.GetLinkPickerItem("singleLinkPicker");
```

The `GetLinkPickerItem` method will simply return the first item of a link picker list, or an empty link item if the list is empty.

To summary what's mentioned above, have a look at the Razor example below (which has the proper imports etc.):

```C#
@using Skybrud.LinkPicker
@using Skybrud.LinkPicker.Extensions
@inherits UmbracoViewPage<IPublishedContent>

@{

    // Using either a regular cast or a safe cast
    LinkPickerList linkPickerList1 = Model.GetPropertyValue("multiLinkPicker") as LinkPickerList;
    
    // Using Umbraco and generics
    LinkPickerList linkPickerList2 = Model.GetPropertyValue<LinkPickerList>("multiLinkPicker");

    // Using the build-in extension method
    LinkPickerList linkPickerList3 = Model.GetLinkPickerList("multiLinkPicker");

    // Using the build-in extension method
    LinkPickerItem linkPickerItem1 = Model.GetLinkPickerItem("singleLinkPicker");
    
    <p>--@linkPickerList1--</p>
    <p>--@linkPickerList2--</p>
    <p>--@linkPickerList3--</p>
    <p>--@linkPickerItem1--</p>
    
}
```

While the array of items can be accessed through the `Items` property, there is also a `HasItems` property for checking whether the list has any items.

If enabled (through the prevalue options), the property editor also supports specifying a title - eg. if the links are to be shown in a box or similar.

The title can be accessed through the `Title` property of a `LinkPickerList` instance. Also there is a `HasTitle` property for checking whether a title has been specified.

## Using the grid editor

This package also supports a adding a link picker as a grid control in the Umbraco grid. Since you most likely want to configure the link picker your self, you have to add your own `package.manifest` with the details about the editor.

With both `showTable` and `title.show` enabled, the grid editor will look like this:

![image](https://cloud.githubusercontent.com/assets/3634580/16986016/9b70c604-4e84-11e6-94c0-a32dac9e4b19.png)

In it's simplest form (default options), the JSON for the editor can look like this:

```JSON
{
    "name": "Related links",
    "alias": "skybrud.linkPicker.related",
    "view": "/App_Plugins/Skybrud.LinkPicker/Views/LinkPickerGridEditor.html",
    "icon": "icon-link"
}
```

The full configuration for the link picker looks like this:

```JSON
{
    "name": "Related links",
    "alias": "skybrud.linkPicker.related",
    "view": "/App_Plugins/Skybrud.LinkPicker/Views/LinkPickerGridEditor.html",
    "icon": "icon-link",
    "config": {
        "title": {
            "show": true,
            "placeholder": "Related links"
        },
        "limit": 0,
        "types": {
            "url": true,
            "content": true,
            "media": true
        },
        "showTable": true,
        "columns": {
            "type": true,
            "id": true,
            "name": true,
            "url": true,
            "target": true
        }
    }
}
```

### Skybrud.Umbraco.GridData
The link picker also works with our <a href="https://github.com/skybrud/Skybrud.Umbraco.GridData" target="_blank"><strong>Skybrud.Umbraco.GridData</strong></a> package.

Given that you have an instance of `GridControl` representing a control with the link picker, the `Value` property will expose an instance of `GridControlLinkPickerValue`, while the `Editor.Config` property will expose an instance of `GridEditorLinkPickerConfig` for the editor configuration.

## Using the link picker in your own projects

In relation to the backoffice, the main logic of the link picker has been isolated into an Angular directive that can be used in your custom Angular views.

Below is an example of the view for the property editor:

```HTML
<div class="SkybrudPropertyEditors LinkPicker" ng-class="{SingleLinkPicker: model.config.config.limit == 1, MultiLinkPicker: model.config.config.limit != 1}">
    <skybrud-linkpicker value="model.value" config="model.config.config">Sponsored by omgbacon.dk</skybrud-linkpicker>
</div>
```

The model of the link picker list is specified through the `value` attribute - you can simply pass a variable with an empty JavaScript object, and the link picker directive will make sure to set the correct properties.

In a similar way, the configuration can be specified through the `config` attribute. The value specified through this attribute is a JavaScript object similar to the `config` object in the grid editor configuration as shown above.
