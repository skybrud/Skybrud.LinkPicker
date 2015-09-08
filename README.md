# Skybrud.LinkPicker

Skybrud.LinkPicker is a small package that adds a single property editor to Umbraco 7 that can be used as either a single link picker or as a multi link picker depending on configuration.

Besides working like most other link pickers, it can also be configured to show a detailed list of the selected links (eg. as the table in the screenshot below).

![Screenshot of Skybrud.LinkPicker](https://cloud.githubusercontent.com/assets/3634580/9728573/2ab5caf0-5609-11e5-87e8-d7585378107e.png)

### Installation

This package will be released on both Nuget and Our Umbraco ;)

### Usage

The link picker (both single and multi) saves an array of link picker items. Technically the single link picker is a multi link picker, but limited to one item.

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

The `GetLinkPickerItem` method will simply return the first item of a link picker list, or `null` if the list is empty.

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
