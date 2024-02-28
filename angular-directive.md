---
order: 5
teaser: Read more about the Angular directive both the property editor and grid editor are based on.
---

# Angular Directive

In relation to the backoffice, the main logic of the link picker has been isolated into an Angular directive that can be used in your custom Angular views.

Below is an example of the view for the property editor:

```HTML
<div class="SkybrudPropertyEditors LinkPicker" ng-class="{SingleLinkPicker: model.config.config.limit == 1, MultiLinkPicker: model.config.config.limit != 1}">
    <skybrud-linkpicker value="model.value" config="model.config.config"></skybrud-linkpicker>
</div>
```

The model of the link picker list is specified through the `value` attribute - you can simply pass a variable with an empty JavaScript object, and the link picker directive will make sure to set the correct properties.

In a similar way, the configuration can be specified through the `config` attribute. The value specified through this attribute is a JavaScript object similar to the `config` object in the grid editor configuration as shown above.