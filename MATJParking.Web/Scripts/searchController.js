(function () {

    var searchController = function($scope, $http)
    {
        $scope.getData = function () {
            $http.post('/Vehicle/_Search', {
                DropDown: $scope.dropDown,
                SearchValue: $scope.searchValue,
                SortOrder: $scope.sortOrder,
                VehicleTypeId: $scope.vehicleTypeId
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

        $scope.getVehicleTypes = function () {
            $http.get('/Vehicle/_getVehicleTypes', {}).then(function (response) {
                $scope.vehicleTypes = response.data;
            });
        };

     
        $scope.setSearchData = function (dropDown, searchValue, vehicleTypeId, parkingPlaces, vehicleTypes) {
            $scope.dropDown = dropDown;
            $scope.searchValue = searchValue;
            $scope.vehicleTypeId = vehicleTypeId;
            $scope.data = parkingPlaces;
            $scope.vehicleTypes = vehicleTypes;

        };


        //initialise default values
        $scope.data = [];
        $scope.dropDown = "1";
        $scope.searchValue = "";
        $scope.errorMessage = "";
        $scope.sortOrder = "place_asc";
        $scope.vehicleTypeId = 0;
    }


    //Get our module. It is defined in a different file
    var module = angular.module("garage");
    module.controller("searchController", ["$scope", "$http", searchController])
}());