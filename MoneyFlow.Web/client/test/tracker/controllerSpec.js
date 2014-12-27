describe('controller: TrackerCtrl', function() {
    var $scope,
        ctrl, costStore, Cost,
        deferredCreate;

    beforeEach(function() {
        module('mf.tracker', function($provide) {
            $provide.value('costStore', new CostStoreMock());
        });
    });

    beforeEach(inject(function(costStore, $q) {
        function fakeCreate() {
            return (deferredCreate = $q.defer()).promise;
        }

        spyOn(costStore, 'create')
            .and.callFake(fakeCreate);
    }));

    beforeEach(inject(function($rootScope, $controller, _costStore_, _Cost_) {
        $scope = $rootScope.$new();
        costStore = _costStore_;
        Cost = _Cost_;

        // Instantiate controller
        ctrl = $controller('TrackerCtrl as vm', { $scope: $scope, costStore: costStore });

        // Go!
        $scope.$digest();
    }));

    it('should init right away after initialization', function() {

        // assert
        expect(ctrl._isInitialized).toBeTruthy();
    });

    it('should fetch all the periods on init', function() {
        var expected = [
            '2014-06-01T00:00:00',
            '2014-07-01T00:00:00'
        ];

        spyOn(costStore, 'getPeriods')
            .and.returnValue(expected);

        // act
        ctrl.init();

        // assert
        expect(ctrl.periods).toBe(expected);
    });
    
    it('should define the amount and category variables on init', function() {

        // act
        ctrl.init();

        // assert
        expect(ctrl.amount).toBeDefined();
        expect(ctrl.category).toEqual(jasmine.any(Object));
        expect(ctrl.category).not.toBeNull();
    });

    it('should indicate the beginning and the end of commit process', function() {
        var isProcessingBefore, isProcessingAfter;

        // act
        ctrl.commit();
        isProcessingBefore = ctrl.isProcessing;
        deferredCreate.resolve();
        $scope.$digest();
        isProcessingAfter = ctrl.isProcessing;

        // assert
        expect(isProcessingBefore).toBeTruthy();
        expect(isProcessingAfter).toBeFalsy();
    });
    
    it('should notify the descendants on commit completion', function() {
        var cost = new Cost();

        spyOn($scope, '$broadcast');

        // act
        ctrl.commit();
        deferredCreate.resolve(cost);
        $scope.$digest();

        // assert
        expect($scope.$broadcast).toHaveBeenCalledWith('costCreated', cost);
    }); 
});

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

    it('should fetch costs filtered by period', function() {
        var period = '2014-07-01T00:00:00';
        var expected = new CostStoreMock.GetByPeriodResult(period);

        // act
        ctrl.init(period);

        // assert
        expect(ctrl.costs).toEqual(expected);
    });
});