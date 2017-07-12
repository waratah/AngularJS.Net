interface IWeatherResult {
    location: string;
    time: string;
    wind: string;
    visibility: string;
    skyConditions: string;
    temperature: string;
    dewPoint: string;
    relativeHumidity: string;
    pressure: string;
}

interface IDataService {
    GetCountries: () => angular.IHttpPromise<string[]>;
    GetCities: (country: string) => angular.IHttpPromise<string[]>;
    GetWeather: (country: string, city: string) => angular.IHttpPromise<IWeatherResult>;
}

angular.module('App').
    service('dataService',

    class DataService implements IDataService {

        constructor(private $http: angular.IHttpService) {
        }

        public GetCountries(): angular.IHttpPromise<string[]> {
            var url = '/api/countries';
            var promise = this.$http<string[]>({
                method: 'GET',
                responseType: "JSON",
                url: url
            });
            return promise;
        }

        public GetCities(country: string): angular.IHttpPromise<string[]> {
            var url = '/api/cities/' + country;
            var promise = this.$http<string[]>({
                method: 'GET',
                responseType: "JSON",
                url: url
            });
            return promise;
        }

        public GetWeather(country: string, city: string): angular.IHttpPromise<IWeatherResult> {
            var url = '/api/weather?country=' + country + '&city=' + city;
            var promise = this.$http<IWeatherResult>({
                method: 'GET',
                responseType: "JSON",
                url: url
            });
            return promise;
        }

    });