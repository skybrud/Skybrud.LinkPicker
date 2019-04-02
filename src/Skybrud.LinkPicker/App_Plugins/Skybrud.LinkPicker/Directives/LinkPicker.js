angular.module("umbraco").directive("skybrudLinkpicker", ["dialogService", "skybrudLinkPickerService", function (dialogService, p) {
    return {
        scope: {
            value: "=",
            config: "="
        },
        transclude: true,
        restrict: "E",
        replace: true,
        templateUrl: "/App_Plugins/Skybrud.LinkPicker/Views/LinkPickerDirective.html?v=3",
        link: function (scope) {
            
            var v = Umbraco.Sys.ServerVariables.application.version.split(".");
            scope.umbVersion = parseFloat(v[0] + "." + (v[1].length === 1 ? "0" + v[1] : v[1]));

            function initValue() {

                // Initialize an empty model if no value at all
                if (!scope.value) {
                    scope.value = {
                        title: "",
                        items: []
                    };
                }

                // Convert the value if an array (legacy)
                if (Array.isArray(scope.value)) {
                    scope.value = {
                        title: "",
                        items: scope.value
                    };
                }

                // Set the "mode" property if not already present
                scope.value.items.forEach(function (link) {
                    if (!link.mode) link.mode = (link.id ? (link.url && link.url.indexOf("/media/") === 0 ? "media" : "content") : "url");
                });

            }

            function initConfig() {

                scope.cfg = scope.config ? scope.config : {};

                // Restore configuration not specified (can probably be made prettier)
                if (!scope.cfg.limit) scope.cfg.limit = 0;
                if (!scope.cfg.types) scope.cfg.types = {};
                if (scope.cfg.types.url === undefined) scope.cfg.types.url = true;
                if (scope.cfg.types.content === undefined) scope.cfg.types.content = true;
                if (scope.cfg.types.media === undefined) scope.cfg.types.media = true;
                if (scope.cfg.view == undefined) scope.cfg.view = "preview";
                if (scope.cfg.showTable === true) scope.cfg.view = "table";
                if (scope.cfg.showTable === false) scope.cfg.view = "list";
                if (!scope.cfg.columns) scope.cfg.columns = {};
                if (scope.cfg.columns.type === undefined) scope.cfg.columns.type = true;
                if (scope.cfg.columns.content === undefined) scope.cfg.columns.content = true;
                if (scope.cfg.columns.id === undefined) scope.cfg.columns.id = true;
                if (scope.cfg.columns.name === undefined) scope.cfg.columns.name = true;
                if (scope.cfg.columns.url === undefined) scope.cfg.columns.url = true;
                if (scope.cfg.columns.target === undefined) scope.cfg.columns.target = true;

            }

            scope.addLink = function () {

                // Open Umbraco's link picker overlay
                scope.linkPickerOverlay = {
                    view: "linkpicker",
                    show: true,
                    submit: function (model) {

                        // Append the link to the list
                        scope.value.items.push(p.parseUmbracoLink(model.target));

                        // Hide/reset the overlay
                        scope.linkPickerOverlay.show = false;
                        scope.linkPickerOverlay = null;

                    }
                };

            };

            scope.editLink = function (link, index) {

                // Set the target based on "link"
                var target = {
                    name: link.name,
                    url: link.url,
                    target: link.target,
                    anchor: link.anchor
                };

                // Set the ID if "mode" is "content"
                if (link.id && link.mode === "content") {
                    target.id = link.id;
                }

                // Open Umbraco's link picker overlay
                scope.linkPickerOverlay = {
                    view: "linkpicker",
                    currentTarget: target,
                    show: true,
                    submit: function (model) {

                        // Append the link to the list
                        scope.value.items[index] = p.parseUmbracoLink(model.target);

                        // Hide/reset the overlay
                        scope.linkPickerOverlay.show = false;
                        scope.linkPickerOverlay = null;

                    }
                };

            };

            scope.removeLink = function (index) {
                var temp = [];
                for (var i = 0; i < scope.value.items.length; i++) {
                    if (index !== i) temp.push(scope.value.items[i]);
                }
                scope.value.items = temp;
            };

            scope.sortableOptions = {
                axis: "y",
                cursor: "move",
                handle: ".handle",
                tolerance: "pointer",
                placeholder: "linkpicker-sortable-placeholder",
                containment: "parent"
            };

            scope.sortableOptionsList = {
                axis: "y",
                cursor: "move",
                handle: ".handle",
                tolerance: "pointer",
                containment: "parent"
            };

            scope.sortableOptionsPreview = {
                distance: 10,
                tolerance: "pointer",
                scroll: true,
                zIndex: 6000,
                disabled: false
            };

            initValue();
            initConfig();

        }
    };
}]);
