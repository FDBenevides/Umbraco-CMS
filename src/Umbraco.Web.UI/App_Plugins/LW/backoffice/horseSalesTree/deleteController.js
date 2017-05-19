angular.module("umbraco").controller("LW.HorseSales.deleteController",
    function ($scope, $rootScope, $routeParams, $timeout, navigationService, notificationsService, horseSalesResource) {
        $scope.performDelete = function () {
            horseSalesResource.deleteById($scope.currentNode.id).then(function () {
                navigationService.hideNavigation();
                navigationService.syncTree({ tree: 'horseSalesTree', path: [-1, $scope.id], forceReload: true }).then(
                    function (syncArgs) {
                        navigationService.reloadNode(syncArgs.node);
                    });
                notificationsService.success("Successfully deleted.");
            })
        };
        $scope.cancelDelete = function () {
            navigationService.hideNavigation();
        };
    });