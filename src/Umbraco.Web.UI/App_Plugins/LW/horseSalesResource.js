angular.module("umbraco.resources")
.factory("horseSalesResource", function ($http) {
    return {
        create:function(horseRequest){
            return $http.post("backoffice/LW/HorseSalesApi/Create", angular.toJson(horseRequest));
        },
        getById: function (id) {
            return $http.get("backoffice/LW/HorseSalesApi/GetById?id=" + id);
        },
        save: function (horseRequest) {
            return $http.post("backoffice/LW/HorseSalesApi/PostSave", angular.toJson(horseRequest));
        },
        getByMemberId: function (id) {

        },
        getAll: function () {

        },
        deleteById: function (id) {
            return $http.delete("backoffice/LW/HorseSalesApi/DeleteById?id=" + id);
        }
    };
});