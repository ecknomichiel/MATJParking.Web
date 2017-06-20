var myApp= angular
        .module("myapp",[])
        .controller("MainController", function($scope){
            var employees = [
                {firstName:"Tarek", dateOfBirht:"nov 15 , 1090", gender:"Male", salary:5500},
                {firstName:"Aomeone",dateOfBirht:"dec 20 , 1690", gender:"Female", salary:5100},
                {firstName:"Gecandone",dateOfBirht:"oct 10 , 1990", gender:"Male", salary:6500},
                {firstName:"Zherdone",dateOfBirht:"jun 09 , 1590", gender:"Female", salary:9500}
            ];
            $scope.employees = employees;
            $scope.sortColumn = "firstName";
            $scope.reverseSort = false;

            $scope.sortData = function(column) {
                $scope.reverseSort = ($scope.sortColumn == column) ? !$scope.reverseSort : false;
                $scope.sortColumn = column;
            }
            $scope.getSortClass = function(column){
                if($scope.sortColumn == column) {
                    return $scope.reverseSort ? 'arrow-down':'arrow-up'
                }
                return '';
            }
        });
