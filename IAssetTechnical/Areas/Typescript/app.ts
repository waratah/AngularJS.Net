interface IAppScope extends angular.IScope {
    IsAjax: () => boolean;
    IsLoaded: () => boolean;
    GetCities: () => void;

    model: {
        country: string,
        city: string,

        weather?: IWeatherResult,

        isLoaded: boolean,
        success: boolean,
        isAjaxBusy: number,

        countries: string[],
        cities: string[],
        errors: string[];
    }
}

angular.module('App', ['ngSanitize', 'ngAnimate', 'ui.bootstrap']).
    controller('AppController', function ($scope: IAppScope, $http: angular.IHttpService, dataService: IDataService) {

        $scope.IsAjax = () => {
            return $scope.model.isAjaxBusy != 0;
        };

        $scope.IsLoaded = () => {
            return $scope.model.isLoaded;
        };

        $scope.GetCities = () => {
            $scope.model.errors = [];
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
                }).catch(function (reason: any) {
                    $scope.model.errors = [reason];
                    $scope.model.isAjaxBusy -= 1;
                });
        };

        $scope.GetWeather = () => {
            $scope.model.errors = [];
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
                    $scope.model.weather= data.data;
                    $scope.model.isAjaxBusy -= 1;
                }).catch(function (reason: any) {
                    $scope.model.errors = [reason];
                    $scope.model.isAjaxBusy -= 1;
                });
        };

        $scope.model = {
            country:"Australia",
            city: null,
            isLoaded: false,
            success: false,
            isAjaxBusy: 0,
            errors: [],
            cities: [],
            countries: []
        };

        function GetCountries() {
            $scope.model.errors = [];
            $scope.model.isAjaxBusy += 1;
            var svc = dataService.GetCountries();
            svc
                .then(function (data) {
                    $scope.model.success = true;
                    $scope.model.countries = data.data;
                    $scope.model.isLoaded = true;
                    $scope.model.isAjaxBusy -= 1;
                }).catch(function (reason: any) {
                    $scope.model.success = false;
                    $scope.model.errors = [reason];
                    $scope.model.isLoaded = true;
                    $scope.model.isAjaxBusy -= 1;
                });
        }

        GetCountries();
    });
