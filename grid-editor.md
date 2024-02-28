---
order: 3
---

# Grid editor

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