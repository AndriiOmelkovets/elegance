(function () {
    'use strict';

    angular
        .module('app')
        .factory('postFactory', postFactory);

    postFactory.$inject = ['$http', '$q'];

    function postFactory($http, $q) {
        var service = {
            getData: getData,
            createPost: createPost
        };

        return service;

        function getData() {
            return $http.get('/api/posts/getall')
            .then(sendData)
            .catch(errorData);

            function sendData(response) {
                return response.data;
            }

            function errorData(error) {
                console.log('error - ' + error);
            }
        }

        function createPost(data) {
            var q = $q.defer();
            $http.post('/api/posts/save', JSON.stringify(data), {
                method: "POST",
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            }).then(handleSuccess, handleError);
            function handleSuccess(response) {
                q.resolve(response.data);
            };
            function handleError(error) {
                console.log("Error");
                q.resolve(error);
            };
            return q.promise;
        }
    }
})();