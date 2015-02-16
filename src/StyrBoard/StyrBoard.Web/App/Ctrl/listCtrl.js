agileControllers.controller('listCtrl', function ($scope, $rootScope, $mdToast, $mdDialog, $filter, listService) {
    // Model
    $scope.cards = [];
    $scope.isLoading = false;


    function init() {
        $scope.isLoading = true;
        listService.get().then(function (list) {
            $scope.cards = list;
            $scope.isLoading = false;
        });
    };

    init();
});