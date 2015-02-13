agile.kanbanBoardApp.service('userStoryService', function ($http, $q) {
    var get = function (location) {
        return $http.get(location).then(function (response) {
            return response.data;
        }, function (error) {
            return $q.reject(error.data.Message);
        });
    };
    var put = function (task) {
        return $http.put("/api/UserStory/" + task.Id, task)
            .then(function (response) {
                return response.status === 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    var post = function (task) {
        return $http.post("/api/UserStory", task)
            .then(function (response) {
                return response.headers('location');
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var deleteUserStory = function (id) {
        return $http.delete("/api/UserStory/" + id)
            .then(function (response) {
                return response.status === 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var setPriority = function (id, priority) {
        return $http.put("/api/UserStory/Priority/" + id + "/" + priority)
            .then(function (response) {
                return response.status === 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    return {
        get: get,
        put: put,
        post: post,
        deleteUserStory: deleteUserStory,
        setPriority: setPriority
    };
});