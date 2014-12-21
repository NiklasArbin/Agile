agileControllers.controller('menuCtrl', function ($scope, $mdSidenav) {

    $scope.menuItems = [
        {
            title: 'Board',
            link: '/#/board'
        },
        {
            title: 'Backlog',
            link: '/#/list'
        }];
});