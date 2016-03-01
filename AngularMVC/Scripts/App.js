var myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http) {

    $http.get('/home/GetProducts')
        .success(function (result) {
            $scope.products = result;
        })
        .error(function (data) {
            console.log(data);
        })

});