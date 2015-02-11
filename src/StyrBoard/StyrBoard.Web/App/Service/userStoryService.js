agile.kanbanBoardApp.service('userStoryService', function ($http, $q) {
    var getTask = function(location) {
        return $http.get(location).then(function(response) {
            return response.data;
        }, function(error) {
            return $q.reject(error.data.Message);
        });
    };
    var saveTask = function (task) {
        return $http.put("/api/UserStory/" + task.Id, task )
            .then(function (response) {
                return response.status == 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    var createTask = function (task) {
        return $http.post("/api/UserStory", task)
            .then(function (response) {
            return response.headers('location');
        }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var deleteTask = function (id) {
        return $http.delete("/api/UserStory/" + id)
            .then(function (response) {
                return response.status == 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };
    return {
        getTask: getTask,
        saveTask: saveTask,
        createTask: createTask,
        deleteTask: deleteTask
    };
});