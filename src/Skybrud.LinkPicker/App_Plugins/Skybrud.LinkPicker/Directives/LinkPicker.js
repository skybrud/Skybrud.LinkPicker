angular.module('umbraco').directive('skybrudLinkpicker', function () {
    return {
        scope: {
            links: '=',
            config: '='
        },
        transclude: true,
        restrict: 'E',
        replace: true,
        templateUrl: '/App_Plugins/Skybrud.LinkPicker/Views/LinkPickerDirective.html',
        link: function (scope, dialogService) {

            // Make sure "links" is an array
            if (!scope.links || !Array.isArray(scope.links)) {
                scope.links = [];
            }
            
            if (scope.config) {
                scope.cfg = scope.config;
            } else {
                scope.cfg = {};
            }

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

            console.log(JSON.stringify(scope.cfg, null, '  '));

            // Set the "mode" property if not already present
            scope.links.forEach(function (link) {
                if (!link.mode) link.mode = (link.id ? (link.url && link.url.indexOf('/media/') === 0 ? 'media' : 'content') : 'url');
            });

            function parseUmbracoLink(e) {
                return {
                    id: e.id || 0,
                    name: e.name || '',
                    url: e.url,
                    target: e.target || '_self',
                    mode: (e.id ? (e.isMedia ? 'media' : 'content') : 'url')
                };
            }

            scope.addLink = function () {
                dialogService.closeAll();
                dialogService.linkPicker({
                    callback: function (e) {
                        if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                        scope.links.push(parseUmbracoLink(e));
                        dialogService.closeAll();
                    }
                });
            };

            scope.editLink = function (link, index) {
                dialogService.closeAll();
                if (link.mode == 'media') {
                    dialogService.mediaPicker({
                        callback: function (e) {
                            if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                            scope.links[index] = parseUmbracoLink(e);
                            dialogService.closeAll();
                        }
                    });
                } else {
                    dialogService.linkPicker({
                        currentTarget: {
                            id: link.id,
                            name: link.name,
                            url: link.url,
                            target: link.target
                        },
                        callback: function (e) {
                            if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                            scope.links[index] = parseUmbracoLink(e);
                            dialogService.closeAll();
                        }
                    });
                }
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
});