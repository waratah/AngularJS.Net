interface IAppScope extends angular.IScope {
    IsAjax: () => boolean;
    IsLoaded: () => boolean;
    GetCities: () => void;

    model: {
        country: string,
        city: string,

        weather?: IWeatherResult,

        IsLoaded: boolean,
        Success: boolean,
        IsAjaxBusy: number,

        countries: string[],
        cities: string[],
        Errors: string[];
    }
}

angular.module('App', ['ngSanitize', 'ngAnimate', 'ui.bootstrap']).
    controller('AppController', function ($scope: IAppScope, $http: angular.IHttpService, dataService: IDataService) {

        $scope.IsAjax = () => {
            return $scope.model.IsAjaxBusy != 0;
        };

        $scope.IsLoaded = () => {
            return $scope.model.IsLoaded;
        };

        $scope.GetCities = () => {
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
                }).catch(function (reason: any) {
                    $scope.model.Errors = [reason];
                    $scope.model.IsAjaxBusy -= 1;
                });
        };

        $scope.GetWeather = () => {
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
                    $scope.model.weather= data.data;
                    console.log(data);
                    $scope.model.IsAjaxBusy -= 1;
                    $scope.model.Errors = [];
                }).catch(function (reason: any) {
                    $scope.model.Errors = [reason];
                    $scope.model.IsAjaxBusy -= 1;
                });
        };

        $scope.model = {
            country:"Australia",
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
                }).catch(function (reason: any) {
                    $scope.model.Success = false;
                    $scope.model.Errors = [reason];
                    $scope.model.IsLoaded = true;
                    $scope.model.IsAjaxBusy -= 1;
                });
        }

        GetCountries();
    });
