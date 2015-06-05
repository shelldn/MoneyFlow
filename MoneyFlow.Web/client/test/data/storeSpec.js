describe('store-for: Cost (remote)', function() {
    var $httpBackend, costStore,
        Cost;

    var URL_COSTS           = '/api/costs',
        URL_COSTS_PERIODS   = '/api/costs/periods';

    // region suite-maintenance

    beforeEach(module('mf.data'));

    beforeEach(inject(function(_$httpBackend_, _remoteCostStore_, _Cost_) {
        $httpBackend = _$httpBackend_;
        costStore = _remoteCostStore_;
        Cost = _Cost_;

        $httpBackend.when('GET', '/api/costs/2014-07')
            .respond(['2014-07-13T14:00:00']);
    }));

    afterEach(function() {
        $httpBackend.verifyNoOutstandingExpectation();
        $httpBackend.verifyNoOutstandingRequest();
    });

    // endregion

    it('should fetch and return all the periods for costs', function() {
        var periods = [
            '2014-01-01T00:00:00',
            '2014-12-01T00:00:00'
        ];

        $httpBackend.whenGET(URL_COSTS_PERIODS).respond(periods);
        $httpBackend.expectGET(URL_COSTS_PERIODS);

        // act
        var ret = costStore
            .getPeriods();

        $httpBackend.flush();

        // assert
        expect(ret.length).toEqual(periods.length);
        expect(ret[0]).toEqual(periods[0]);
        expect(ret[1]).toEqual(periods[1]);
    });

    it('should translate period to year-month pair', function() {
        var pair = '2014-07',
            period = pair + '-01T00:00:00';

        $httpBackend.expectGET('/api/costs/' + pair);

        // act
        costStore.getByPeriod(period);
        $httpBackend.flush();
    });

    it('should fetch and return all the costs for specified period', function() {
        var period = '2014-07-01T00:00:00';

        // act
        var ret = costStore.getByPeriod(period);
        $httpBackend.flush();

        // assert
        expect(ret.length).toEqual(1);
        expect(ret[0]).toEqual('2014-07-13T14:00:00');
    });

    it('should create cost and return its server represented instance', function() {
        var cost = new Cost(25, {}, '2014-07-13T14:00:00'),
            serverCost = angular.copy(cost);

        serverCost.id = 1;

        var fakeSuccess = jasmine.createSpy('success');

        $httpBackend.whenPOST(URL_COSTS, cost).respond(200, serverCost);

        // act
        costStore.create(cost).then(fakeSuccess);
        $httpBackend.flush();

        // assert
        expect(fakeSuccess).toHaveBeenCalledWith(
            jasmine.objectContaining(serverCost)
        );
    });
});

describe('store-for: Cost (local)', function() {
    var ls, store,
        YES = true;

    // region suite-maintenance

    beforeEach(function() {
        jasmine.addMatchers(CustomMatchers);
    });

    beforeEach(module('mf.data'));

    beforeEach(inject(function(localStorageService, localCostStore) {
        ls      = localStorageService;
        store   = localCostStore;
    }));

    beforeEach(function() {
        ls.set('costs', costs({ /*

            YEAR    Jan  Feb  Mar  Apr  May  Jun  Jul  Aug  Sep  Oct  Nov  Dec
            ------------------------------------------------------------------ */
            2014: [ YES, 0,   0,   0,   0,   YES, YES, 0,   0,   0,   YES, 0,
                    0,   0,   0,   0,   0,   YES, 0,   0,   0,   0,   YES, 0,
                    0,   0,   0,   0,   0,   YES, 0,   0,   0,   0,   0,   0   ]
            //                               ^^^                      ^^^
            //                                 └----- duplicates ----┘

        }));
    });

    // endregion

    describe('getPeriods()', function() {

        it('should return ISO 8601 date string sequence', function() {
            function toBeDate(p) {
                expect(p).toBeDate();
            }

            _(store.getPeriods()).each(toBeDate);
        });

        it('should have no duplicated months in output sequence', function() {
            expect(store.getPeriods()).not.toHaveDuplicates();
        });

        it('should have no missed months in output sequence', function() {
            var periods = store.getPeriods();
            for (var i = 0; i < periods.length - 1; i++) {
                expect(periods[i]).toPrecede(periods[i + 1]);
            }
        });

        it('should match 1st and last months with min and max cost dates', function() {
            var periods = _(store.getPeriods()),
                Jan2014 = ISODate(2014, 1),
                Nov2014 = ISODate(2014, 11);

            expect(periods.first()) .toEqual(Jan2014);
            expect(periods.last())  .toEqual(Nov2014);
        });
    });

    describe('getByPeriod(String)', function() {
        it('should return all the costs spent within specified month');
    });

    describe('create(Cost)', function() {
        it('should persist Cost record in the local storage');
    });
});