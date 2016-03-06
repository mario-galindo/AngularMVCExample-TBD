﻿var myApp = angular.module('myApp', ['ngRoute', 'LocalStorageModule']);

//Para reforzar el nameSpace y no lea otros datos
myApp.config(['localStorageServiceProvider', function (localStorageServiceProvider) {

    localStorageServiceProvider.setPrefix('ls');
}])

myApp.config(function ($routeProvider, $locationProvider) {

    $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
    }).hashPrefix('!');

    $routeProvider
        .when('/',{


            resolve: {

                "check": function ($location, localStorageService,$window) {
                    if (!localStorageService.get('Sesion')) {
                        //$location.path('/Templates/login.html');
                        $window.location = "/login";
                        //templateUrl: '/Templates/login.html'
                    }
                }
            },

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

        .when('/CrearBaseDato', {

            templateUrl: '/Templates/CrearBaseDato.html',
            controller: 'mainController'
        })

        

    	.otherwise({

    	    redirectTo: '/'
    	});
            
    
});

myApp.controller('mainController', function ($scope, $http, $location, $window, localStorageService, $rootScope) {

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


        $http.post('/login/logearse', { user: $scope.usuario, pass: $scope.password })
            .success(function (data) {

                if (data == "True") {

                    $rootScope.InicioSesion = true;

                    //Guardando informacion de la sesion
                    localStorageService.set('user', $scope.usuario);
                    localStorageService.set('pass', $scope.password);
                    localStorageService.set('Sesion', $rootScope.InicioSesion);

                    

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

    $scope.devolverUser = function () {

        alert(localStorageService.get('user') + " " + localStorageService.get('pass')+" "+localStorageService.get('Sesion'));
        
    }

    $scope.cerraSesion = function () {

        localStorageService.remove('user');
        localStorageService.remove('pass');
        localStorageService.remove('Sesion');

    }


    $scope.nombreBaseDatos;

    $scope.crearBaseDatos = function () {
            
        $http.post('/CrearBaseDato/crearBaseDatos', { user: localStorageService.get('user'), pass: localStorageService.get('pass'),nombre: $scope.nombreBaseDatos })
            .success(function (data) {
                console.log(data);
                if (data == "Ok") {

                    $.bootstrapGrowl("Base de datos Creada exitosamente", {
                        type: 'success'
                    });

                    $scope.nombreBaseDatos = ""

                } else {
                    $.bootstrapGrowl("Error al crear base de datos", {
                        type: 'danger'
                    });
                }
            })
            .error(function (error) {
                console.log(error);
            })

    }

});