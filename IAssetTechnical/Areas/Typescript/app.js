angular.module('App', ['ngSanitize', 'ngAnimate', 'ui.bootstrap']).
    controller('AppController', function ($scope, $http, dataService) {
    $scope.IsAjax = function () {
        return $scope.model.IsAjaxBusy != 0;
    };
    $scope.IsLoaded = function () {
        return $scope.model.IsLoaded;
    };
    $scope.GetCities = function () {
        $scope.model.cities = [];
        if (!$scope.model.country) {
            $scope.model.Errors = ["Please enter a country name"];
            return;
        }
        $scope.model.IsAjaxBusy += 1;
        var svc = dataService.GetCities($scope.model.country);
        svc
            .then(function (data) {
            $scope.model.cities = data.data;
            $scope.model.IsAjaxBusy -= 1;
            $scope.model.Errors = [];
        }).catch(function (reason) {
            $scope.model.Errors = [reason];
            $scope.model.IsAjaxBusy -= 1;
        });
    };
    $scope.GetWeather = function () {
        $scope.model.weather = null;
        if (!$scope.model.country) {
            $scope.model.cities = [];
            $scope.model.Errors = ["Please enter a country name"];
            return;
        }
        if (!$scope.model.city) {
            $scope.model.Errors = ["Please enter a city"];
            return;
        }
        $scope.model.IsAjaxBusy += 1;
        var svc = dataService.GetWeather($scope.model.country, $scope.model.city);
        svc
            .then(function (data) {
            $scope.model.weather = data.data;
            console.log(data);
            $scope.model.IsAjaxBusy -= 1;
            $scope.model.Errors = [];
        }).catch(function (reason) {
            $scope.model.Errors = [reason];
            $scope.model.IsAjaxBusy -= 1;
        });
    };
    $scope.model = {
        country: "Australia",
        city: null,
        IsLoaded: false,
        Success: false,
        IsAjaxBusy: 0,
        Errors: [],
        cities: [],
        countries: []
    };
    function GetCountries() {
        $scope.model.IsAjaxBusy += 1;
        var svc = dataService.GetCountries();
        svc
            .then(function (data) {
            $scope.model.Success = true;
            $scope.model.countries = data.data;
            $scope.model.Errors = [];
            $scope.model.IsLoaded = true;
            $scope.model.IsAjaxBusy -= 1;
        }).catch(function (reason) {
            $scope.model.Success = false;
            $scope.model.Errors = [reason];
            $scope.model.IsLoaded = true;
            $scope.model.IsAjaxBusy -= 1;
        });
    }
    GetCountries();
});
//# sourceMappingURL=app.js.map