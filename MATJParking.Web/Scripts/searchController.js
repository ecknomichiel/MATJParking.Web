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
            return result;
        };

        $scope.setSortOrder = function (fieldName) {
            if (fieldName == $scope.sortFieldName)
            {
                $scope.sortAsc = !$scope.sortAsc;
            }
            else
            {
                $scope.sortFieldName = fieldName;
                $scope.sortAsc = true;
            }
            var datafunction;
            switch (fieldName)
            {
                case "place": datafunction = function (el) {
                    return el.ID;
                }
                    break;
                case "registrationNumber": datafunction = function (el) {
                    return el.VehicleRegNumber;
                }
                    break;
                case "vehicleType": datafunction = function (el) {
                    return el.Vehicle.VehicleType.Name;
                }
                    break;
                case "parkingTime": datafunction = function (el) {
                    return el.Vehicle.ParkingTime;
                }
                    break;
                case "price": datafunction = function (el) {
                    return el.Vehicle.Price;
                }
                    break;
                default: datafunction = function (el) {
                    return el.ID;
                }
                    break;
            }
            var compareFunction = function (el1, el2) {
                var result = 0;
                if (datafunction(el1) > datafunction(el2))
                {
                    result = 1;
                }
                else if (datafunction(el1) < datafunction(el2))
                {
                    result = -1;
                }
                if (!$scope.sortAsc)
                    result = -result;
                return result;
            }
            $scope.data = $scope.data.sort(compareFunction);
        }
        


        //initialise default values
        $scope.data = [];
        $scope.dropDown = "1";
        $scope.searchValue = "";
        $scope.errorMessage = "";
        $scope.setSortOrder("place");
        $scope.vehicleTypeId = 0;
        $scope.searchPath = getSearchPath();
    }


    //Get our module. It is defined in a different file
    var module = angular.module("garage");
    module.controller("searchController", ["$scope", "$http", searchController])
}());