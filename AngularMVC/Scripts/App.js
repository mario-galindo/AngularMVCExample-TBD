var myApp = angular.module('myApp', ['ngRoute']);

myApp.config(function ($routeProvider, $locationProvider) {

    $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
    }).hashPrefix('!');

    $routeProvider
        .when('/',{

            templateUrl:'/Templates/Home.html',
            controller: 'mainController'
        })

        .when('/login', {

            templateUrl: '/Templates/login.html',
            controller: 'mainController'
        })

        .when('/Editor', {

            templateUrl: '/Templates/Editor.html',
            controller: 'mainController'
        })

        

    	.otherwise({

    	    redirectTo: '/'
    	});
            
    
});

myApp.controller('mainController', function ($scope, $http,$location,$window) {

    $scope.usuario;
    $scope.password;


    $http.get('/home/GetProducts')
        .success(function (result) {
            $scope.products = result;
        })
        .error(function (data) {
            console.log(data);
        })

    /*
    $scope.login = function () {

        
        $http.post('/home/logearse', { user: $scope.usuario, pass: $scope.password })
            .success(function (data) {
               
                console.log(data);
            })
            .error(function (error) {
                console.log(error);
            })

        
        
    }*/

    $scope.login = function () {


        $http.post('/login/logearse', { user: $scope.usuario, pass: $scope.password })
            .success(function (data) {

                if (data == "True") {

                    console.log(data);
                    $window.location = "/";

                    $.bootstrapGrowl("Conectado", {
                        type: 'success'
                    });


                } else {
                    console.log("usuario Incorrecto");
                    $.bootstrapGrowl("Usuario Incorrecto", {
                        type: 'danger'
                    });
                }
                
                
            })
            .error(function (error) {
                console.log(error);
            })



    }

});