agileControllers.controller('menuCtrl', function ($scope, $rootScope, $mdSidenav) {

    $scope.menuItems = [
        {
            title: 'Board',
            link: '/#/board'
        },
        {
            title: 'Backlog',
            link: '/#/list'
        }];
    $scope.addNew = function() {
        $rootScope.$emit("addNew");
    }
});