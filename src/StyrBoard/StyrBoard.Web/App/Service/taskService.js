agile.kanbanBoardApp.service('taskService', function ($http, $q, $rootScope) {
    var getTask = function(id) {
        return $http.get("/api/Task/" + id).then(function(response) {
            return response.data;
        }, function(error) {
            return $q.reject(error.data.Message);
        });
    };
    var saveTask = function (task) {
        return $http.put("/api/Task/" + task.Id, task )
            .then(function (response) {
                return response.status == 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    var deleteTask = function (id) {
        return $http.delete("/api/Task/" + id)
            .then(function (response) {
                return response.status == 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    return {
        getTask: getTask,
        saveTask: saveTask,
        deleteTask: deleteTask
    };
});