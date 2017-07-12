angular.module('App').
    service('dataService', (function () {
    function DataService($http) {
        this.$http = $http;
    }
    DataService.prototype.GetCountries = function () {
        var url = '/api/countries';
        var promise = this.$http({
            method: 'GET',
            responseType: "JSON",
            url: url
        });
        return promise;
    };
    DataService.prototype.GetCities = function (country) {
        var url = '/api/cities/' + country;
        var promise = this.$http({
            method: 'GET',
            responseType: "JSON",
            url: url
        });
        return promise;
    };
    DataService.prototype.GetWeather = function (country, city) {
        var url = '/api/weather?country=' + country + '&city=' + city;
        var promise = this.$http({
            method: 'GET',
            responseType: "JSON",
            url: url
        });
        return promise;
    };
    return DataService;
}()));
//# sourceMappingURL=service.js.map