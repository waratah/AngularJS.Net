angular.module('App', ['ngSanitize', 'ngAnimate', 'ui.bootstrap']).
    controller('AppController', function ($scope, $http, dataService) {
    $scope.IsAjax = function () {
        console.log($scope.model.isAjaxBusy);
        return $scope.model.isAjaxBusy != 0;
    };
    $scope.IsLoaded = function () {
        return $scope.model.isLoaded;
    };
    $scope.GetCities = function () {
        $scope.model.cities = [];
        if (!$scope.model.country) {
            $scope.model.errors = ["Please enter a country name"];
            return;
        }
        $scope.model.isAjaxBusy += 1;
        var svc = dataService.GetCities($scope.model.country);
        svc
            .then(function (data) {
            $scope.model.cities = data.data;
            $scope.model.isAjaxBusy -= 1;
            $scope.model.errors = [];
        }).catch(function (reason) {
            $scope.model.errors = [reason];
            $scope.model.isAjaxBusy -= 1;
        });
    };
    $scope.GetWeather = function () {
        $scope.model.weather = null;
        if (!$scope.model.country) {
            $scope.model.cities = [];
            $scope.model.errors = ["Please enter a country name"];
            return;
        }
        if (!$scope.model.city) {
            $scope.model.errors = ["Please enter a city"];
            return;
        }
        $scope.model.isAjaxBusy += 1;
        var svc = dataService.GetWeather($scope.model.country, $scope.model.city);
        svc
            .then(function (data) {
            $scope.model.weather = data.data;
            console.log(data);
            $scope.model.isAjaxBusy -= 1;
            $scope.model.errors = [];
        }).catch(function (reason) {
            $scope.model.errors = [reason];
            $scope.model.isAjaxBusy -= 1;
        });
    };
    $scope.model = {
        country: "Australia",
        city: null,
        isLoaded: false,
        success: false,
        isAjaxBusy: 0,
        errors: [],
        cities: [],
        countries: []
    };
    function GetCountries() {
        $scope.model.isAjaxBusy += 1;
        var svc = dataService.GetCountries();
        svc
            .then(function (data) {
            $scope.model.success = true;
            $scope.model.countries = data.data;
            $scope.model.errors = [];
            $scope.model.isLoaded = true;
            $scope.model.isAjaxBusy -= 1;
        }).catch(function (reason) {
            $scope.model.success = false;
            $scope.model.errors = [reason];
            $scope.model.isLoaded = true;
            $scope.model.isAjaxBusy -= 1;
        });
    }
    GetCountries();
});
//# sourceMappingURL=app.js.map