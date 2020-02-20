angular.module("umbraco").controller("Skybrud.LinkPicker.Link.Controller", function ($scope, editorService) {

    function parseUmbracoLink(link) {
        if (!link) return null;
        link.type = (link.isMedia ? "media" : link.id > 0 ? "content" : "url");
        delete link.isMedia;
        return link;
    }

    $scope.addLink = function () {
        $scope.editLink();
    };

    $scope.editLink = function () {

        var target = $scope.model.value ? angular.copy($scope.model.value) : null;

        if (target && target.type === "media") {
            target.isMedia = true;
        }

        editorService.linkPicker({
            currentTarget: target,
            hideTarget: $scope.model.config && $scope.model.config.hideTarget === true,
            submit: function (model) {
                $scope.model.value = parseUmbracoLink(model.target);
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        });
    };

    $scope.removeLink = function() {
        $scope.model.value = null;
    };

});