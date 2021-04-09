angular.module("umbraco").controller("Skybrud.LinkPicker.Elements.Controller", function ($scope, editorService) {

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
        placeholder: "linkpicker-sortable-placeholder",
        stop: function() {
            $scope.sync();
        }
    };

    $scope.addLink = function (ct) {

        var linkProperty = ct.getPropertyType(x => x.editor === "Skybrud.LinkPicker.Link");
        var textProperty = ct.getPropertyType("text");

        if (!linkProperty) {
            console.error("No property type found with editor 'Skybrud.LinkPicker.Link'");
            return;
        }

        editorService.linkPicker({
            hideTarget: linkProperty.config && linkProperty.config.hideTarget === true,
            submit: function (model) {

                // Close the overlay
                editorService.close();

                // Skip adding the item if the user didn't specify a valid link
                if (!model.target || !model.target.url) return;

                // Initialize the properties of the link item
                var properties = {};
                properties[linkProperty.alias] = parseUmbracoLink(model.target);

                // Populate the text property if present
                if (model.target.name && textProperty) properties[textProperty.alias] = model.target.name;

                // Add the item
                $scope.addItemFromContentType(ct, properties);

            },
            close: function () {
                editorService.close();
            }
        });

    };

});