var myApp = angular.module('myApp', ['ngRoute', 'LocalStorageModule']);

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
        .when('/crearTabla', {

            templateUrl: '/Templates/crearTabla.html',
            controller: 'mainController'
        })
        .when('/crearRelacion', {

            templateUrl: '/Templates/CrearRelacion.html',
            controller: 'mainController'
        })
        .when('/borrarRelacion', {

            templateUrl: '/Templates/borrarRelacion.html',
            controller: 'mainController'
        })
        

    	.otherwise({

    	    redirectTo: '/'
    	});
            
    
});

myApp.controller('mainController', function ($scope, $http, $location, $window, localStorageService, $rootScope) {

    $scope.usuario;
    $scope.password;
    $scope.baseDatos = null;
    $scope.nombreTabla = "";

    $scope.init = function () {

        $scope.getDataBases();
        $scope.getDataTypes()
    }

    /*
    $scope.data = {
        repeatSelect: null,
        availableOptions: [
          { id: '1', name: 'Master' },
          { id: '2', name: 'TempDB' },
          { id: '3', name: 'Model' },
          { id: '3', name: 'msdb' }
        ],
    };
    */

    $scope.datatypes = ['varchar', 'int', 'double', 'char', 'datetime', 'time'];

    $scope.camposTabla = [
        [
            false,
            "",
            "varchar",
            20,
            true
        ]
    ]

    $scope.print = function () {
            
        console.log($scope.camposTabla);
        console.log($scope.baseDatos);
    }

    $scope.crearTabla = function () {
        var data = {
            baseDatos: $scope.baseDatos,
            nombreTabla: $scope.nombreTabla,
            campos: $scope.camposTabla
        };


        $http.post('/crearTabla/crearTabla', data)
        .success(function (d) {
            
            if (d == "ok") {

                $.bootstrapGrowl("tabla Creada exitosamente", {
                    type: 'success'
                });

               

            } else {
                sweetAlert("Error al crear tabla", d, "error");
            }
        })
    .error(function (error) {
        console.log(error);
    })

    }

    $scope.agregarFila = function () {
        $scope.camposTabla.push([
            false,
            "",
            "varchar",
            20,
            true
        ]);

    }

    $scope.eliminarCampo = function (key) {
        $scope.camposTabla.splice(key, 1);
    }

    $scope.getDataBases = function () {
        $http.get('/crearTabla/GetDataBases')
    .success(function (result) {
       
        $scope.data = result;
      

    })
    .error(function (data) {
        console.log(data);
    })
    }


    $scope.getDataTypes = function () {
        $http.get('/crearTabla/GetDataTypes')
    .success(function (result) {
        //console.log(result[0]);
        $scope.datatypes = result;
        //console.log($scope.data);

    })
    .error(function (data) {
        console.log(data);
    })
    }

    

    /*

    $http.get('/home/GetProducts')
        .success(function (result) {
            $scope.products = result;
            console.log(result);
        })
        .error(function (data) {
            console.log(data);
        })

   */

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

    $scope.listaTablas = [];

    $scope.databaseForRelation;
    $scope.fieldRight;
    $scope.fieldLeft;
    $scope.tableselectedRight;
    $scope.tableselectedLeft;
    $scope.nombreRelacion;

    $scope.crearForeignKey = function () {


        $http.post('/CrearRelacion/CrearForeignKey', { baseDatos: $scope.databaseForRelation, tablaDer: $scope.tableselectedRight, nombreRel: $scope.nombreRelacion, campoDer: $scope.fieldRight, tablaIzq: $scope.tableselectedLeft, campoIzq: $scope.fieldLeft })
           .success(function (result) {
               if (result == "true") {
                   swal("Buen Trabajo!", "relacion creada exitosamente", "success");
               } else {
                   sweetAlert("Error...", result, "error");
               }
           })
           .error(function (data) {
               $.bootstrapGrowl("Error al crear Foreign key", {
                   type: 'success'
               });
           })

     }

    //Funcion para cargar las tablas de una base de datos
    $scope.getTables = function () {

        $http.post('/CrearRelacion/GetTables', {baseDatos:$scope.databaseForRelation})
           .success(function (result) {
               
                $scope.listaTablas = result;
                
           })
           .error(function (data) {
            console.log(data);
        })
    }

    //CARGAR CAMPOS
    
    $scope.estructutaTableRight = [];
    $scope.estructutaTableLeft = []

    //Funcion
    $scope.cargarCamposRight = function () {

        $http.post('/CrearRelacion/GetCamposRight', { tableRight: $scope.tableselectedRight, baseDatos: $scope.databaseForRelation })
            .success(function (result) {
                
                $scope.estructutaTableRight = result;
               
            })
            .error(function (data) {
                console.log(data);
            })
    }


    $scope.cargarCamposLeft = function () {

        $http.post('/CrearRelacion/GetCamposLeft', { tableLeft: $scope.tableselectedLeft, baseDatos: $scope.databaseForRelation })
            .success(function (result) {
               
                $scope.estructutaTableLeft = result;
               
            })
            .error(function (data) {
                console.log(data);
            })
    }

   
    //Get Constraints
    $scope.listaConstraints = [];
    $scope.constraintSelected;

    $scope.idTabla;
    $scope.recuperarIdTable = function(){

        $http.post('/borrarRelacion/GetTablesID', { baseDatos: $scope.databaseForRelation, tabla: $scope.tableselectedLeft })
          .success(function (result) {
              $scope.idTabla = result[0];
              
          })
          .error(function (data) {
              console.log(data);
          })
        
       
    }


    

    $scope.getConstraints = function () {

        $http.post('/borrarRelacion/GetConstraints', { baseDatos: $scope.databaseForRelation,id:$scope.idTabla })
          .success(function (result) {
              $scope.listaConstraints = result;
          })
          .error(function (data) {
              console.log(data);
          })
    }

   

   

    //Borrar Constraints
    $scope.borrarConstraint = function () {

        $http.post('/borrarRelacion/borrarConstraints', { baseDatos: $scope.databaseForRelation, constraintSelected: $scope.constraintSelected,tabla:$scope.tableselectedLeft })
          .success(function (result) {
              if (result == "true") {
                  swal("Buen Trabajo!", "relacion borrada exitosamente", "success");
              }
          })
          .error(function (data) {
              console.log(data);
          })
    }


    //Ejecutar Queries
    $scope.resultadoQuery = [];
    $scope.QueryToExecute;

    $scope.ejecutarQuery = function () {

        $http.post('/Editor/execQueryRows', { baseDatos: $scope.baseDatos, query: $scope.QueryToExecute })
          .success(function (result) {

                
                var size = result.length;
                
                if (result[size - 1] == "True") {
                    $scope.resultadoQuery = result;
                    $scope.resultadoQuery.splice(size - 1, 1);

                    $.bootstrapGrowl("Consulta ejecutada exitosamente", {
                        type: 'success'
                    });

                } else {
                    sweetAlert("Error...", result, "error");
                }
              
          })
          .error(function (data) {
              console.log(data);
          })
        //alert($scope.baseDatos);
    }

    $scope.limpiarConsola = function () {
        $scope.resultadoQuery = [];
    }

    
});