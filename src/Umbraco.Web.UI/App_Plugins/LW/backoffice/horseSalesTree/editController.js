angular.module("umbraco").controller("LW.HorseSales.editController",
    function ($scope, $rootScope, $routeParams, $location, $timeout, appState, navigationService, notificationsService, angularHelper, horseSalesResource, datatypeResource) {

        //setup scope vars
        $scope.defaultButton = null;
        $scope.subButtons = [];

        $scope.page = {};
        $scope.page.loading = true;
        $scope.page.saving = false;
        console.log("Loading started ($scope.page.loading variable: " + $scope.page.loading + " )");
        $scope.page.menu = {};
        $scope.page.menu.currentNode = null;
        $scope.page.menu.currentSection = appState.getSectionState("currentSection");
        $scope.page.listViewPath = null;
        $scope.page.isNew = $routeParams.create;
        $scope.page.buttonGroupState = "init";

        $scope.linkPicker = {
            "config": {
                "showTable": true
            }
        };

        function initTabsConfig() {
            $scope.content = {
                "tabs": [{
                    "id": 1,
                    "active": true,
                    "label": "Info",
                    "alias": "Tab1",
                    "properties": []
                },
                {
                    "id": 2,
                    "active": true,
                    "label": "Suggestions",
                    "alias": "Tab2",
                    "properties": []
                },
                {
                    "id": 3,
                    "active": true,
                    "label": "Final List",
                    "alias": "Tab3",
                    "properties": []
                }]
            };
        }

        function loadDataTypeResources(horseRequest) {

            return new Promise(function (resolve, reject) {
                if (horseRequest && horseRequest.Tabs) {
                    var processed = 0;
                    var toProcess = 0;

                    for (tab in horseRequest.Tabs) {
                        toProcess += horseRequest.Tabs[tab].length;
                    }

                    for (tab in horseRequest.Tabs) {

                        for(let prop of horseRequest.Tabs[tab]) {
                            var datatypeConfig;

                            if (prop.loadResource) {
                                datatypeResource.getByName(prop.view).then(function (result) {
                                    datatypeConfig = result.data;
                                    prop.view = datatypeConfig.view;
                                    prop.config = datatypeConfig.config;

                                    processed++; //increments processed properties counter
                                    if (processed === toProcess)
                                        resolve(horseRequest);
                                });
                            } else {
                                processed++; //increments processed properties counter
                                if (processed === toProcess)
                                    resolve(horseRequest);
                            }
                        }
                    }
                }
            });
        }

        initTabsConfig();

        if ($routeParams.id == -1) {
            $location.path("/horseSales/horseSalesTree/create/-1");
            //$scope.horseRequest = {
            //    "name": "test2",
            //    "id": $routeParams.id,
            //    "clientId": '',
            //    "docType": $routeParams.doctype,
            //    "create": $routeParams.create
            //};
            //$scope.page.loading = false;
        } else {
            horseSalesResource.getById($routeParams.id).then(function (response) {
                loadDataTypeResources(response.data).then(function (horseRequest) {


                    var propHorseLinks = horseRequest.Tabs["tab2"].find(item=>item.propertyAlias == "HorseLinksObj")
                    if (propHorseLinks)
                        propHorseLinks.config = $scope.linkPicker;
                    var propFinalHorseLinks = horseRequest.Tabs["tab3"].find(item=>item.propertyAlias == "FinalHorseLinksObj")
                    if (propFinalHorseLinks)
                        propFinalHorseLinks.config = $scope.linkPicker;

                    $scope.horseRequest = horseRequest;

                    $scope.page.loading = false;
                    console.log("Loading finished ($scope.page.loading variable: " + $scope.page.loading + " )");
                });
            });
        }

        $scope.save = function () {
            $scope.page.saving = true;
            console.log("Saving started ($scope.page.saving variable: " + $scope.page.saving + " )");


            saveModel = {};
            _.each($scope.horseRequest.Tabs, function (tab) {
               _.each(tab, function (prop) {
                    saveModel[prop.propertyAlias] = prop.value;
                })
            });
            saveModel.HorseLinks = JSON.stringify(saveModel.HorseLinksObj);
            saveModel.FinalHorseLinks = JSON.stringify(saveModel.FinalHorseLinksObj);
            saveModel.Name = $scope.horseRequest.Name;
            saveModel.MemberName = $scope.horseRequest.MemberName;

            horseSalesResource.save(saveModel).then(function (response) {
                loadDataTypeResources(response.data).then(function (horseRequest) {

                    var propHorseLinks = horseRequest.Tabs["tab2"].find(item=>item.propertyAlias == "HorseLinksObj")
                    if (propHorseLinks)
                        propHorseLinks.config = $scope.linkPicker;
                    var propFinalHorseLinks = horseRequest.Tabs["tab3"].find(item=>item.propertyAlias == "FinalHorseLinksObj")
                    if (propFinalHorseLinks)
                        propFinalHorseLinks.config = $scope.linkPicker;

                    $scope.horseRequest = horseRequest;
                    $scope.page.saving = false;
                    console.log("Loading finished ($scope.page.saving variable: " + $scope.page.saving + " )");

                    notificationsService.success("Request successfully saved", $scope.horseRequest.Name);
                    //angularHelper.getCurrentForm($scope).$setDirty();
                    if($scope.horseRequestForm)
                        $scope.horseRequestForm.$dirty = false;
                    else if (horseRequestForm)
                        horseRequestForm.$dirty = false;

                    navigationService.syncTree({ tree: 'horseSalesTree', path: [-1, $scope.id], forceReload: true }).then(
                        function (syncArgs) {
                            navigationService.reloadNode(syncArgs.node);
                        });
                });
            },
            function (response) {
                notificationsService.error("Save failed for " + $scope.horseRequest.Name);
                $scope.page.saving = false;
                console.log("Loading finished ($scope.page.saving variable: " + $scope.page.saving + " )");
            });
        }


    });