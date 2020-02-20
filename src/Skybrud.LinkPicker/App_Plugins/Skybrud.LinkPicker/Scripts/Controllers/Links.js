angular.module("umbraco").controller("Skybrud.LinkPicker.PartialView.Controller", function ($scope, editorService) {

    function parseUmbracoLink(link) {
        if (!link) return null;
        link.type = link.isMedia ? "media" : link.id > 0 ? "content" : "url";
        delete link.isMedia;
        return link;
    }

    $scope.sortableOptions = {
        distance: 10,
        tolerance: "pointer",
        scroll: true,
        zIndex: 6000,
        disabled: false,
        containment: "parent",
        placeholder: "linkpicker-sortable-placeholder"
    };

    $scope.addLink = function (ct) {

        editorService.linkPicker({
            submit: function (model) {

                // Skip adding the item if the user didn't specify a valid link
                if (!model.target || !model.target.url) {
                    editorService.close();
                    return;
                }

                // Initialize the properties of the link item
                var properties = {};
                properties.link = parseUmbracoLink(model.target);

                // Populate the text property if present
                if (model.target.name) properties.text = model.target.name;

                editorService.close();

                // Add the item ("true" means the item should be opened for editing)
                $scope.addItem(ct, properties, true);

            },
            close: function () {
                editorService.close();
            }
        });

    };

});