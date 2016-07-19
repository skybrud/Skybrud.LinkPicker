angular.module('umbraco').directive('skybrudLinkpicker', ['dialogService', 'skybrudLinkPickerService', function (dialogService, p) {
    return {
        scope: {
            links: '=',
            config: '='
        },
        transclude: true,
        restrict: 'E',
        replace: true,
        templateUrl: '/App_Plugins/Skybrud.LinkPicker/Views/LinkPickerDirective.html',
        link: function (scope) {

            // Make sure "links" is an array
            if (!scope.links || !Array.isArray(scope.links)) {
                scope.links = [];
            }
            
            if (scope.config) {
                scope.cfg = scope.config;
            } else {
                scope.cfg = {};
            }

            // Restore configuration not specified (can probably be made prettier)
            if (!scope.cfg.limit) scope.cfg.limit = 0;
            if (!scope.cfg.types) scope.cfg.types = {};
            if (scope.cfg.types.url === undefined) scope.cfg.types.url = true;
            if (scope.cfg.types.content === undefined) scope.cfg.types.content = true;
            if (scope.cfg.types.media === undefined) scope.cfg.types.media = true;
            if (scope.cfg.showTable == undefined) scope.cfg.showTable = false;
            if (!scope.cfg.columns) scope.cfg.columns = {};
            if (scope.cfg.columns.type === undefined) scope.cfg.columns.type = true;
            if (scope.cfg.columns.content === undefined) scope.cfg.columns.content = true;
            if (scope.cfg.columns.id === undefined) scope.cfg.columns.id = true;
            if (scope.cfg.columns.name === undefined) scope.cfg.columns.name = true;
            if (scope.cfg.columns.url === undefined) scope.cfg.columns.url = true;
            if (scope.cfg.columns.target === undefined) scope.cfg.columns.target = true;

            // Set the "mode" property if not already present
            scope.links.forEach(function (link) {
                if (!link.mode) link.mode = (link.id ? (link.url && link.url.indexOf('/media/') === 0 ? 'media' : 'content') : 'url');
            });

            scope.addLink = function () {
                p.addLink(function (link) {

                    // Make sure "links" is an array (again)
                    if (!scope.links || !Array.isArray(scope.links)) {
                        scope.links = [];
                    }

                    scope.links.push(link);

                });
            };

            scope.editLink = function (link, index) {
                p.editLink(link, function (newLink) {
                    scope.links[index] = newLink;
                });
            };

            scope.removeLink = function (index) {
                var temp = [];
                for (var i = 0; i < scope.links.length; i++) {
                    if (index != i) temp.push(scope.links[i]);
                }
                scope.links = temp;
            };

            scope.sortableOptions = {
                axis: 'y',
                cursor: 'move',
                handle: '.handle'
            };

        }
    };
}]);
