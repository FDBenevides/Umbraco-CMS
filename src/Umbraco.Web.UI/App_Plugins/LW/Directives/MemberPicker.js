angular.module('umbraco').directive('lwMemberpicker', function () {
    return {
        scope: {
            model: '=' // allows data to be passed into directive (from the view using this directive)
        },
        //transclude: true,
        //require: "^form",
        restrict: 'E',
        //replace: true,
        templateUrl: 'views/propertyeditors/memberpicker/memberpicker.html'
        //,
        //controller: function () {
        //    this.tabs = [];
        //    this.test = [];
        //},
        //link: function (scope) {
        //    var t = "";
        //}        
    };

});