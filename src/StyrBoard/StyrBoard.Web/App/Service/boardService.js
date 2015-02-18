agile.kanbanBoardApp.service('boardService', function ($http, $q, $rootScope, userStoryService) {
    var proxy = null;

    var getColumns = function () {
        return $http.get("/api/Board").then(function (response) {
            return response.data;
        }, function (error) {
            return $q.reject(error.data.Message);
        });
    };

    var canMoveTask = function (sourceColIdVal, targetColIdVal) {
        return $http.get("/api/Board/CanMove", { params: { sourceColId: sourceColIdVal, targetColId: targetColIdVal } })
            .then(function (response) {
                return response.data.canMove;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var moveTask = function (taskIdVal, targetColIdVal) {
        return $http.post("/api/Board/MoveTask", { taskId: taskIdVal, targetColId: targetColIdVal })
            .then(function (response) {
                return response.status === 200;
            }, function (error) {
                return $q.reject(error.data.Message);
            });
    };

    var initialize = function () {

        connection = jQuery.hubConnection();
        proxy = connection.createHubProxy('MainHub');

        // Listen to the 'BoardUpdated' event that will be pushed from SignalR server
        proxy.on('BoardUpdated', function () {
            $rootScope.$emit("refreshBoard");
        });

        proxy.on('CardUpdated', function (card) {
            $rootScope.$emit("cardUpdated", card);
        });

        proxy.on('CardDeleted', function (id) {
            $rootScope.$emit("cardDeleted", id);
        });

        proxy.on('CardAdded', function (location) {
            userStoryService.get(location).then(function(card) {
                $rootScope.$emit("cardAdded", card);
            });
        });

        proxy.on('CardPriorityChanged', function (list) {
            $rootScope.$emit("cardPriorityChanged", list);
        });

        // Connecting to SignalR server        
        return connection.start()
        .then(function (connectionObj) {
            return connectionObj;
        }, function (error) {
            return error.message;
        });
    };

    // Call 'NotifyBoardUpdated' on SignalR server, maybe move this serverside...
    var sendRequest = function () {
        proxy.invoke('NotifyBoardUpdated');
    };
    var notifyCardUpdated = function (id) {
        proxy.invoke('NotifyCardUpdated', id);
    };
    var notifyCardDeleted = function (id) {
        proxy.invoke('NotifyCardDeleted', id);
    };
    var notifyCardAdded = function (location) {
        proxy.invoke('NotifyCardAdded', location);
    };

    initialize();
    
    return {
        sendRequest: sendRequest,
        notifyCardUpdated: notifyCardUpdated,
        notifyCardDeleted: notifyCardDeleted,
        notifyCardAdded: notifyCardAdded,
        getColumns: getColumns,
        canMoveTask: canMoveTask,
        moveTask: moveTask
    };
});