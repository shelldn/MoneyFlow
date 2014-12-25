describe('controller: PeriodCtrl', function() {
    var $scope, ctrl;

    beforeEach(function() {
        module('mf.tracker', function($provide) {
            $provide.value('costStore', new CostStoreMock());
        });
    });

    beforeEach(inject(function($rootScope, $controller, costStore) {
        $scope = $rootScope.$new();

        // Instantiate controller
        ctrl = $controller('PeriodCtrl as pd', { $scope: $scope, costStore: costStore });

        // Fire
        $scope.$digest();
    }));

    it('should load costs filtered by period', function() {
        var period = '2014-07-01T00:00:00';
        var expected = new CostStoreMock.GetByPeriodResult(period);

        // act
        ctrl.init(period);

        // assert
        expect(ctrl.costs).toEqual(expected);
    });
});