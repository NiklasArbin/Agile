agile.kanbanBoardApp.service('taskService', function ($http, $q, $rootScope) {
    var getTask = function (id) {
        return $http.get("/api/Task/" + id).then(function (response) {
            return response.data;
        }, function (error) {
            return $q.reject(error.data.Message);
        });
    }
    return {
        getTask: getTask
    };
});