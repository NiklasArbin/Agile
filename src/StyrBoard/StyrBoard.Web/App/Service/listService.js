agile.kanbanBoardApp.service('listService', function ($http, $q, $rootScope) {
    var proxy = null;

    var get = function () {
        return $http.get("/api/List").then(function (response) {
            return response.data;
        }, function (error) {
            return $q.reject(error.data.Message);
        });
    };

    return {
        get: get,
    };
});