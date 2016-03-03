var myApp = angular.module('myApp', []);

myApp.controller('mainController', function ($scope, $http,$location) {

    $scope.usuario;
    $scope.password;


    $http.get('/home/GetProducts')
        .success(function (result) {
            $scope.products = result;
        })
        .error(function (data) {
            console.log(data);
        })


    $scope.login = function () {

        
        $http.post('/home/logearse', { user: $scope.usuario, pass: $scope.password })
            .success(function (data) {
               
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })

        
        
    }

});