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

    beforeEach(inject(function($controller, $rootScope, _costStore_, _Cost_) {
        $scope = $rootScope.$new();
        costStore = _costStore_;
        Cost = _Cost_;

        // Instantiate controller
        ctrl = $controller('TrackerCtrl as vm', { $scope: $scope, Cost: Cost, costStore: costStore });

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

    it('should create the Cost object with current amount, category and date', function() {
        ctrl.amount = 25;
        ctrl.category = { words: 'hello, world' };

        var date = new Date(),
            cost = new Cost(ctrl.amount, ctrl.category, date);

        // act
        ctrl.commit();

        // assert
        expect(costStore.create).toHaveBeenCalledWith(cost);
    });

    it('should notify the descendants on commit completion', function() {
        var cost = new Cost(25, {}, new Date());

        spyOn($scope, '$broadcast');

        // act
        ctrl.commit();
        deferredCreate.resolve(cost);
        $scope.$digest();

        // assert
        expect($scope.$broadcast).toHaveBeenCalledWith('costCreated', cost);
    }); 
    
    it('should reinitialize itself on commit completion', function() {
        spyOn(ctrl, 'init');

        // act
        ctrl.commit();
        deferredCreate.resolve(new Cost());
        $scope.$digest();

        // assert
        expect(ctrl.init).toHaveBeenCalled();
    }); 
});

describe('controller: PeriodCtrl', function() {
    var $rootScope, ctrl,
        link, costStore,
        Cost;

    beforeEach(function() {
        module('mf.tracker', function($provide) {
            $provide.value('costStore', new CostStoreMock());
        });
    });

    beforeEach(inject(function($controller, $filter, _$rootScope_, _costStore_, _Cost_) {
        var $scope;

        $rootScope      = _$rootScope_;
        costStore       = _costStore_;
        Cost            = _Cost_;

        $scope = $rootScope.$new();
        link = $filter('link');

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
    
    it('should append the newly created cost if periods match', function() {
        var costs = [
                new Cost(10, {}, '2014-03-10T14:00:00'),
                new Cost(20, {}, '2014-04-20T14:00:00')
            ],

            cost = new Cost(25, {}, '2014-07-13T14:00:00');

        spyOn(costStore, 'getByPeriod')
            .and.returnValue(costs);

        // act
        ctrl.init(cost.period);
        $rootScope.$broadcast('costCreated', cost);

        // assert
        expect(ctrl.costs).toContain(cost);
    });

    it('should ignore the newly created cost if periods not match', function() {
        var costs, cost,
            nextMonth;

        costs = [];
        cost = new Cost(25, {}, '2014-06-25T22:00:00');
        nextMonth = moment(cost.period).add(1, 'M').format();

        spyOn(costStore, 'getByPeriod')
            .and.returnValue(costs);

        // act
        ctrl.init(nextMonth);
        $rootScope.$broadcast('costCreated', cost);

        // assert
        expect(ctrl.costs).not.toContain(cost);
    });

    describe('isNewDay(Cost)', function() {
        it("should return 'true' if there is no previous cost", function() {
            var cost = new Cost(25, {}, '2014-07-10T14:00:00');

            // act
            var result = ctrl.isNewDay(cost);

            // assert
            expect(result).toBeTruthy();
        });

        it("should return 'true' if the previous cost was spent some days before current", function() {
            var costs = [
                new Cost(25, {}, '2014-06-10T14:00:00'),
                new Cost(25, {}, '2014-06-20T14:00:00')
            ];

            // act
            costs = link(costs);

            var result = ctrl.isNewDay(costs[1]);

            // assert
            expect(result).toBeTruthy();
        });

        it("should return 'false' if the previous cost was spent at the same day as current", function() {
            var costs = [
                new Cost(25, {}, '2014-06-10T16:00:00'),
                new Cost(25, {}, '2014-06-10T16:00:00')
            ];

            // act
            costs = link(costs);

            var result = ctrl.isNewDay(costs[1]);

            // assert
            expect(result).toBeFalsy();
        });
    });
});