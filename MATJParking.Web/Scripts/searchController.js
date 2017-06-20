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
            $scope.searchPath = getSearchPath();
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
            $scope.searchPath = getSearchPath();
        };

        function getVehicleTypeName(Id)
        {
            var vt = $scope.vehicleTypes.filter(function(el){
                return el.ID == parseInt(Id);
            });
            return vt[0].Name;
        }
        var getSearchPath = function () {
            switch ($scope.dropDown)
            {
                case "1": return "All parked vehicles";
                case "2": {
                    var price = parseFloat($scope.searchValue);
                    return "Price greater than/" + price;
                }
                case "3": return "Registration number/" + $scope.searchValue.toUpperCase() + "*";
                case "5": return "All Parking places";
                default: {
                    if ($scope.dropDown != "" && $scope.dropDown[0] == '4')
                    {
                        var vtId = $scope.dropDown.substring(2);
                        return "Vehicle type/" + getVehicleTypeName(vtId);
                    }
                };
            };
            
        };

        $scope.getSelectOptions = function () {
            var result = [{name: "All parked vehicles", value: "1"},
                {name: "Price greater than", value: "2"},
                {name: "Registration number", value: "3"},
                {name: "All parking places", value: "5"}
            ]
            for (vehicleType in $scope.vehicleTypes) {
                result.push({ name: vehicleType.Name, value: "4." + vehicleType.ID });
            };
            return result;
        };
        


        //initialise default values
        $scope.data = [];
        $scope.dropDown = "1";
        $scope.searchValue = "";
        $scope.errorMessage = "";
        $scope.sortOrder = "place_asc";
        $scope.vehicleTypeId = 0;
        $scope.searchPath = getSearchPath();
    }


    //Get our module. It is defined in a different file
    var module = angular.module("garage");
    module.controller("searchController", ["$scope", "$http", searchController])
}());