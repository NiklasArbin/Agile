agileControllers.controller('listCtrl', function ($scope, $rootScope, $mdToast, $mdDialog, $filter, listService, sortingService) {
    // Model
    $scope.cards = [];
    $scope.isLoading = false;

    $scope.dragControlListeners = {
        orderChanged: function (event) {
            sortingService.orderChanged(event, $scope.cards);
        },
    };

    $scope.getCardById = function (id) {
        for (var i = 0; i < $scope.cards.length; i++) {
            if ($scope.cards[i].Id === id) return $scope.cards[i];
        }
    };

    $scope.getCardIndexById = function (id) {
        for (var i = 0; i < $scope.cards.length; i++) {
            if ($scope.cards[i].Id === id) return i;
        }
    };

    $scope.deleteCard = function (id) {
        var index = $scope.getCardIndexById(id);
        $scope.cards.splice(index, 1);
    }

    $scope.updateCard = function(card) {
        $scope.deleteCard(card.Id);
        $scope.cards.push(card);
        $scope.cards.sort(function (a, b) {
            return a.Priority - b.Priority;
        });
    }

    $scope.addCard = function(card) {
        $scope.cards.push(card);
    };

    // Listen to the 'deleteCard' event and remove the card as a result
    $rootScope.$on("cardAdded", function (evt, location) {
        $scope.addCard(location);
    });

    // Listen to the 'updateCard' event and refresh the card as a result
    $rootScope.$on("cardUpdated", function (evt, card) {
        $scope.updateCard(card);
    });

    // Listen to the 'deleteCard' event and remove the card as a result
    $rootScope.$on("cardDeleted", function (evt, id) {
        $scope.deleteCard(id);
    });
    // Listen to the 'cardPriorityChanged' event and update the card as a result
    $rootScope.$on("cardPriorityChanged", function (evt, list) {
        for (var i = 0; i < list.length; i += 1) {
            var card = $scope.getCardById(list[i].Key);
            card.Priority = list[i].Value;
        };
    });

    function init() {
        $scope.isLoading = true;
        listService.get().then(function (list) {
            $scope.cards = list;
            $scope.isLoading = false;
        });
    };

    init();
});