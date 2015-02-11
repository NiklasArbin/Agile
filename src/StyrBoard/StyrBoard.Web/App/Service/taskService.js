agile.kanbanBoardApp.service('taskService', function ($http, $q) {
    var get = function(location) {
        return $http.get(location).then(function(response) {
            return response.data;
        }, function(error) {
            return $q.reject(error.data.Message);
        });
    };
    var put = function (task) {
        return $http.put("/api/Task/" + task.Id, task )
            .then(function (response) {
                return response.status == 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    var post = function (task) {
        return $http.post("/api/Task", task)
            .then(function (response) {
            return response.headers('location');
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
        get: get,
        put: put,
        post: post,
        deleteTask: deleteTask
    };
});