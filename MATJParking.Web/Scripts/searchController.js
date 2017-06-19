(function () {

    var searchController = function($scope, $http)
    {
        var getData = function () {
            $http.get('/Vehicle/_Search', {
                dropDown: $scope.dropDown,
                searchValue: $scope.searchValue,
                searchOrder: $scope.searchOrder,
                vehicleTypeId: $scope.vehicleTypeId
            }).then(function (response) {
                //Success
                $scope.data = response.data;
                $scope.errorMessage = "";
            },
            function (response) {
                //Failure
                $scope.data = [];
                $scope.errorMessage = response.statusText;
            });
        };

        $scope.getData = getData;

        //initialise default values
        $scope.data = [];
        $scope.dropDown = "1";
        $scope.searchValue = "";
        $scope.errorMessage = "";
        $scope.sortOrder = "place_asc";
        $scope.vehicleTypeId = 0;
    }


    //Get our module. It is defined in a different file
    var module = angular.module("matjModule");
    modul.controller("searchController", ["$scope", "$http", searchController])
});