---
order: 2
teaser: Read more about how to use the link picker in your content types.
---

# Property Editor

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