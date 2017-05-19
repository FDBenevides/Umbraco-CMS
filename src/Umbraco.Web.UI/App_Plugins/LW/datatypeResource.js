angular.module('umbraco.resources').factory('datatypeResource',
    function ($http) {
        return {
            getByName: function (name) {
                return $http.get("backoffice/LW/DataTypeApi/GetByName?name=" + name);
            }
        }
    }
);