angular.module("umbraco").controller("LW.HorseSales.createController",
    function ($scope, $rootScope, $routeParams, $location, $timeout, navigationService, notificationsService, formHelper, datatypeResource, horseSalesResource) {

        $scope.page = {
            loading: true
        };

        $scope.test = "123";
        $scope.horseRequest = {
            "member": [
                //{ "id": "1073", "name": "name" }, { "id": "1074", "name": "name2" }
            ]
        };
        //$scope.clientsRenderModel = {};
        
        //$scope.clients = {};
        //$scope.clients.value = $scope.horseRequest.client.map(function (item) { return item.id; }).join(",");

        //$scope.$watch('memberPicker', function () {
        //    if ($scope.memberPicker != undefined) {
        //        $scope.client = $scope.memberPicker[0].value;
        //    }
        //}, true);

        //datatypeResource.getByName('Member Picker').then(function (result) {
        //    $scope.memberPicker = [
        //        {
        //            alias: 'member',
        //            label: 'Client',
        //            view: result.data.view,
        //            config: result.data.config,
        //            value: $scope.client
        //        }
        //    ];

        //    $scope.loaded = true;
        //});
        $scope.save = function () {

            //TODO:
            if (!$scope.page.loading && formHelper.submitForm({ scope: $scope, statusMessage: undefined, action: undefined })) {
                $scope.page.loading = true;

                if (!$scope.horseRequest.member[0].value) {
                    $scope.horseRequest.memberRequired = true;
                } else {
                    $scope.horseRequest.memberRequired = false;

                    $scope.saveModel = {
                        "MemberId": $scope.horseRequest.member[0].value,
                        "Name": $scope.horseRequest.name
                    };

                    //TODO:
                    horseSalesResource.create($scope.saveModel).then(function (response) {
                        $scope.horseRequestCreateForm.$dirty = false;

                        var horseRequest = response.data;
                        if (horseRequest && horseRequest.Id) {
                            notificationsService.success("Request successfully created", horseRequest.Name);
                            $location.path("/horseSales/horseSalesTree/edit/" + horseRequest.Id);
                        }
                    })
                }

                $scope.page.loading = false;
            }

            //$scope.$broadcast("formSubmitting", { scope: $scope, action: undefined });

            //$scope.loaded = true;
        };

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

        if ($routeParams.id == -1) {
            $scope.horseRequest.member.push({});

            $scope.page.loading = false;
        } else {
            $scope.horseRequest.member.push({
                "value": $routeParams.id
            });

            $scope.page.loading = false;
        }
    });